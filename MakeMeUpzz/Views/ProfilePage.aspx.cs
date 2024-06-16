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
    public partial class ProfilePage : System.Web.UI.Page
    {
        UserController userController = new UserController();
        List<User> users = new List<User>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null && Request.Cookies["user_cookie"] == null)
            {
                Response.Redirect("~/Views/LoginPage.aspx");
            }
            else
            {
                User currentUser = new User();
                if (Session["user"] == null)
                {
                    int userID = Convert.ToInt32(Request.Cookies["user_cookie"].Value);
                    currentUser = UserHandler.findUserbyID(userID);
                    Session["user"] = currentUser;
                }
                else
                {
                    currentUser = (User)Session["user"];
                }
                if (Application["user_count"] == null)
                {
                    Application["user_count"] = 1;
                }                
                if (!IsPostBack)
                {
                    int userID = currentUser.UserID;
                    User checkUser = UserHandler.getUserfromID(userID);

                    if (checkUser != null)
                    {
                        UsernameTB.Text = checkUser.Username;
                        UserEmailTB.Text = checkUser.UserEmail;
                        String userGender = checkUser.UserGender;
                        if (userGender.Equals("Male"))
                        {
                            RadioButtonMale.Checked = true;
                        }
                        else if (userGender.Equals("Female"))
                        {
                            RadioButtonFemale.Checked = true;
                        }
                        UserDOB.Text = checkUser.UserDOB.ToString("dd-MM-yyyy");
                        DOBCalendar.SelectedDate = checkUser.UserDOB;
                    }
                    else
                    {
                        Response.Redirect("~/Views/HomePage.aspx");
                    }
                }
            }
        }

        protected void ChangePasswordBtn_Click(object sender, EventArgs e)
        {
            User currentUser = (User)Session["user"];
            int userID = currentUser.UserID;
            User checkUser = UserHandler.getUserfromID(userID);
            String oldPassword = OldPasswordTB.Text;
            String newPassword = NewPasswordTB.Text;

            String validation = "Update Password Successful!";
            ErrorLblUpdatePassword.Text = userController.UpdatePassword(userID, oldPassword, newPassword);
            if (validation.Equals(ErrorLblUpdatePassword.Text))
            {
                ErrorLblUpdatePassword.ForeColor = System.Drawing.Color.Green;
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void UpdateProfileBtn_Click(object sender, EventArgs e)
        {
            User currentUser = (User)Session["user"];
            int userID = currentUser.UserID;
            User checkUser = UserHandler.getUserfromID(userID);
            String username = UsernameTB.Text;
            String userEmail = UserEmailTB.Text;
            String gender = "";
            if (RadioButtonMale.Checked)
            {
                gender = "Male";
            }
            else if (RadioButtonFemale.Checked)
            {
                gender = "Female";
            }
            DateTime UserDOB = DOBCalendar.SelectedDate;

            String validation = "Update Successful!";
            ErrorLblUpdateProfile.Text = userController.Update(userID, username, userEmail, gender, UserDOB);
            if (validation.Equals(ErrorLblUpdateProfile.Text))
            {
                ErrorLblUpdateProfile.ForeColor = System.Drawing.Color.Green;
                Response.Redirect(Request.RawUrl);
            }
        }
    }
}