using MakeMeUpzz.Controllers;
using MakeMeUpzz.Handlers;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeMeUpzz.Viewss
{
    public partial class UpdateMakeupPage : System.Web.UI.Page
    {
        MakeupController makeupController = new MakeupController();
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateMakeupView.Visible = false;

            if (Session["user"] == null && Request.Cookies["user_cookie"] == null)
            {
                Response.Redirect("~/Views/LoginPage.aspx");
            }
            else
            {
                User user = new User();
                if (Session["user"] == null)
                {
                    int userID = Convert.ToInt32(Request.Cookies["user_cookie"].Value);
                    user = UserHandler.findUserbyID(userID);
                    Session["user"] = user;
                }
                else
                {
                    user = (User)Session["user"];
                }
                if (Application["user_count"] == null)
                {
                    Application["user_count"] = 1;
                }
                if (user.UserRole.Equals("Admin"))
                {
                    UpdateMakeupView.Visible = true;
                    if (!IsPostBack)
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        Makeup makeup = MakeupHandler.FindMakeupByID(id);

                        if (makeup != null)
                        {
                            MakeupNameTB.Text = makeup.MakeupName;
                            MakeupPriceTB.Text = makeup.MakeupPrice.ToString();
                            MakeupWeightTB.Text = makeup.MakeupWeight.ToString();
                            List<String> makeupTypeList = MakeupHandler.getAllMakeupTypeNames();
                            MakeupTypeList.DataSource = makeupTypeList;
                            MakeupTypeList.DataBind();
                            int makeupTypeID = makeup.MakeupTypeID;
                            MakeupTypeList.SelectedValue = MakeupHandler.getMakeupTypeNamebyID(makeupTypeID);

                            List<String> makeupBrandList = MakeupHandler.getAllMakeupBrandNames();
                            MakeupBrandList.DataSource = makeupBrandList;
                            MakeupBrandList.DataBind();
                            int makeupBrandID = makeup.MakeupBrandID;
                            MakeupBrandList.SelectedValue = MakeupHandler.getMakeupBrandNamebyID(makeupBrandID);
                        }
                        else
                        {
                            Response.Redirect("~/Views/ManageMakeupPage.aspx");
                        }
                    }                   
                }
            }
        }

        protected void UpdateMakeupBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            String MakeupName = MakeupNameTB.Text;
            int MakeupPrice = Convert.ToInt32(MakeupPriceTB.Text);
            int MakeupWeight = Convert.ToInt32(MakeupWeightTB.Text);
            int MakeupTypeID = MakeupHandler.getMakeupTypeIDbyName(MakeupTypeList.Text);
            int MakeupBrandID = MakeupHandler.getMakeupBrandIDbyName(MakeupBrandList.Text);

            String validation = "Operation Successful!";
            ErrorLbl.Text = makeupController.UpdateMakeup(id, MakeupName, MakeupPrice, MakeupWeight,
                MakeupTypeID, MakeupBrandID);
            if (validation.Equals(ErrorLbl.Text))
            {
                Response.Redirect("~/Views/ManageMakeupPage.aspx");
            }
        }

        protected void BackBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/ManageMakeupPage.aspx");
        }
    }
}