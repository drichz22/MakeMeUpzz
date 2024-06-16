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
    public partial class InsertMakeupPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InsertMakeupView.Visible = false;

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
                    InsertMakeupView.Visible = true;
                    if (!IsPostBack)
                    {
                        List<String> makeupTypeList = MakeupHandler.getAllMakeupTypeNames();
                        MakeupTypeList.DataSource = makeupTypeList;
                        MakeupTypeList.DataBind();

                        List<String> makeupBrandList = MakeupHandler.getAllMakeupBrandNames();
                        MakeupBrandList.DataSource = makeupBrandList;
                        MakeupBrandList.DataBind();
                    }
                }
                else
                {
                    Response.Redirect("~/Views/HomePage.aspx");
                }
            }
        }

        protected void InsertMakeupBtn_Click(object sender, EventArgs e)
        {
            MakeupController makeupController = new MakeupController();
            String MakeupName = MakeupNameTB.Text;
            int MakeupPrice = Convert.ToInt32(MakeupPriceTB.Text);
            int MakeupWeight = Convert.ToInt32(MakeupWeightTB.Text);
            int MakeupTypeID = MakeupHandler.getMakeupTypeIDbyName(MakeupTypeList.Text);
            int MakeupBrandID = MakeupHandler.getMakeupBrandIDbyName(MakeupBrandList.Text);

            String validation = "Operation Successful!";
            ErrorLbl.Text = makeupController.InsertMakeup(MakeupName, MakeupPrice, MakeupWeight,
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