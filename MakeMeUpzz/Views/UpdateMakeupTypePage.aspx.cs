using MakeMeUpzz.Controllers;
using MakeMeUpzz.Handlers;
using MakeMeUpzz.Models;
using System;

namespace MakeMeUpzz.Viewss
{
    public partial class UpdateMakeupTypePage : System.Web.UI.Page
    {
        MakeupController makeupController = new MakeupController();
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateMakeupTypeView.Visible = false;

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
                    UpdateMakeupTypeView.Visible = true;
                    if (!IsPostBack) 
                    {
                        int id = Convert.ToInt32(Request.QueryString["id"]);
                        MakeupType makeupType = MakeupHandler.FindMakeupTypeByID(id);

                        if (makeupType != null)
                        {
                            MakeupTypeNameTB.Text = makeupType.MakeupTypeName;
                        }
                        else
                        {
                            Response.Redirect("~/Views/ManageMakeupPage.aspx");
                        }
                    }                  
                }                
            }
        }

        protected void UpdateMakeupTypeBtn_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(Request.QueryString["id"]);
            String MakeupTypeName = MakeupTypeNameTB.Text;
            
            String validation = "Operation Successful!";
            ErrorLbl.Text = makeupController.UpdateMakeupType(id, MakeupTypeName);
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