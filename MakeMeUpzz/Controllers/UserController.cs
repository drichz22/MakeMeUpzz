using MakeMeUpzz.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace MakeMeUpzz.Controllers
{
    public class UserController
    {
        protected Boolean isUsernameUnique(String username)
        {
            List<String> usernameList = UserHandler.getAllUsernames();
            for (int i = 0; i < usernameList.Count; i++)
            {
                if (username.Equals(usernameList[i])) return false;
            }
            return true;
        }
        protected Boolean isAlphaNumeric(String password)
        {
            Boolean isAlphanumeric = Regex.IsMatch(password, "^[a-zA-Z0-9]+$");
            Boolean hasLetter = Regex.IsMatch(password, "[a-zA-Z]");
            Boolean hasDigit = Regex.IsMatch(password, "[0-9]");
            return  isAlphanumeric && hasLetter && hasDigit;
        }
        
        public String Register(String username, String email, String gender, 
            String password, String confirmPassword, DateTime DOB)
        {
            String lbl;
            //Validasi Username
            if (username == null || username.Length == 0)
            {
                lbl = "Username cannot be empty!";
                return lbl;
            }
            else if (username.Length < 5 || username.Length > 15)
            {
                lbl = "Username length must be between 5 and 15 characters!";
                return lbl;
            }
            else if (!isUsernameUnique(username))
            {
                lbl = "Username must be unique!";
                return lbl;
            }

            //Validasi Email
            if (email == null || email.Length == 0)
            {
                lbl = "Email cannot be empty!";
                return lbl;
            }
            else if (!email.EndsWith(".com"))
            {
                lbl = "Email must end with '.com'!";
                return lbl;
            }

            //Validasi Gender
            if (gender != "Male" && gender != "Female")
            {
                lbl = "Gender must be chosen!";
                return lbl;
            }

            //Validasi Password
            if (password == null || password.Length == 0)
            {
                lbl = "Password cannot be empty!";
                return lbl;
            }
            else if (!isAlphaNumeric(password))
            {
                lbl = "Password must be alphanumeric!";
                return lbl;
            }
            else if (!password.Equals(confirmPassword))
            {
                lbl = "Password must be equal with Confirm Password!";
                return lbl;
            }

            //Validasi ConfirmPassword
            if (confirmPassword == null || confirmPassword.Length == 0)
            {
                lbl = "Confirm Password cannot be empty!";
                return lbl;
            }
            else if (!confirmPassword.Equals(password))
            {
                lbl = "Confirm Password must be equal with Password!";
                return lbl;
            }           

            //Validasi DOB
            if (DOB == DateTime.MinValue)
            {
                lbl = "Date of Birth cannot be empty!";
                return lbl;
            }

            int id = UserHandler.generateUserID();
            String role = "Customer";
            UserHandler.insertUser(id, username, email, DOB, gender, role, password);
            lbl = "Registration Successful!";
            return lbl;
        }

        public String Login(String username, String password)
        {
            String lbl;
            if (UserHandler.GetUserfromUsernamePassword(username, password) == null)
            {
                lbl = "User not found";
                return lbl;
            };
            lbl = "Login Successful!";
            return lbl;
        }

        public String Update(int id, String username, String email, String gender, DateTime DOB)
        {
            String lbl;

            //Validasi Username
            if (username == null || username.Length == 0)
            {
                lbl = "Username cannot be empty!";
                return lbl;
            }
            else if (username.Length < 5 || username.Length > 15)
            {
                lbl = "Username length must be between 5 and 15!";
                return lbl;
            }
            else if (!isUsernameUnique(username))
            {
                lbl = "Username must be unique!";
                return lbl;
            }

            //Validasi Email
            if (email == null || email.Length == 0)
            {
                lbl = "Email cannot be empty!";
                return lbl;
            }
            else if (!email.EndsWith(".com"))
            {
                lbl = "Email must end with '.com'!";
                return lbl;
            }

            //Validasi Gender
            if (gender != "Male" && gender != "Female")
            {
                lbl = "Gender must be chosen!";
                return lbl;
            }

            //Validasi DOB
            if (DOB == null)
            {
                lbl = "Date of Birth cannot be empty!";
                return lbl;
            }

            UserHandler.updateUser(id, username, email, DOB, gender);
            lbl = "Update Successful!";
            return lbl;
        }

        public String UpdatePassword(int ID, String oldPassword, String newPassword)
        {
            String lbl;

            //Validasi Old Password
            if (!oldPassword.Equals(UserHandler.getPasswordfromID(ID)))
            {
                lbl = "Password does not match!";
                return lbl;
            }
            else if (oldPassword == null || oldPassword.Length == 0)
            {
                lbl = "Old Password cannot be empty!";
                return lbl;
            }

            //Validasi New Password
            if (newPassword == null || newPassword.Length == 0)
            {
                lbl = "New Password cannot be empty!";
                return lbl;
            }
            else if (!isAlphaNumeric(newPassword))
            {
                lbl = "New Password must be alphanumeric!";
                return lbl;
            }
            else if (newPassword.Equals(oldPassword))
            {
                lbl = "New Password cannot be equal to old password!";
                return lbl;
            }

            UserHandler.updatePassword(ID, newPassword);
            lbl = "Update Password Successful!";
            return lbl;
        }
    }
}