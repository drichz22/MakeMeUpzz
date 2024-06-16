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
    public partial class UpdateMakeupBrandPage : System.Web.UI.Page
    {
        MakeupController makeupController = new MakeupController();
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateMakeupBrandView.Visible = false;

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
                    UpdateMakeupBrandView.Visible = true;
                    if (!IsPostBack)
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        MakeupBrand makeupBrand = MakeupHandler.FindMakeupBrandByID(id);

                        if (makeupBrand != null)
                        {
                            MakeupBrandNameTB.Text = makeupBrand.MakeupBrandName;
                            MakeupBrandRatingTB.Text = makeupBrand.MakeupBrandRating.ToString();
                        }
                        else
                        {
                            Response.Redirect("~/Views/ManageMakeupPage.aspx");
                        }
                    }
                }              
            }
        }

        protected void UpdateMakeupBrandBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            String MakeupBrandName = MakeupBrandNameTB.Text;
            int MakeupBrandRating = Convert.ToInt32(MakeupBrandRatingTB.Text);

            String validation = "Operation Successful!";
            ErrorLbl.Text = makeupController.UpdateMakeupBrand(id, MakeupBrandName, MakeupBrandRating);
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