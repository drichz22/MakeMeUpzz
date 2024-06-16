using MakeMeUpzz.Controllers;
using MakeMeUpzz.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeMeUpzz.Views
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        UserController userController = new UserController();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            ErrorLbl.Text = "";
            String gender = "";
            String username = UsernameTB.Text;
            String email = EmailTB.Text;
            if (RadioButtonMale.Checked)
            {
                gender = "Male";
            }
            else if (RadioButtonFemale.Checked)
            {
                gender = "Female";
            }
            String password = PasswordTB.Text;
            String confirmPassword = ConfirmPasswordTB.Text;
            DateTime DOB = DOBCalendar.SelectedDate;
            String validate = "Registration Successful!";
            ErrorLbl.Text = userController.Register(username, email, gender, password, confirmPassword,
                DOB);
            if (validate.Equals(ErrorLbl.Text))
            {
                Response.Redirect("~/Views/LoginPage.aspx");
            }
        }
    }
}