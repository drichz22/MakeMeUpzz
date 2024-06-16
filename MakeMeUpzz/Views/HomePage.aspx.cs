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
    public partial class HomePage : System.Web.UI.Page
    {
        List<User> users = new List<User>();
        protected void Page_Load(object sender, EventArgs e)
        {
            UserGV.Visible = false;

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
                UserRoleLbl.InnerText = user.UserRole;
                UserNameLbl.InnerText = user.Username;

                if (user.UserRole.Equals("Admin") && !IsPostBack)
                {
                    users = UserHandler.getAllUsers();
                    UserGV.DataSource = users;
                    UserGV.DataBind();
                    UserGV.Visible = true;
                }
            }
        }
    }
}