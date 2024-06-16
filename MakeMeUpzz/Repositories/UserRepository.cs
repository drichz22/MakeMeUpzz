using MakeMeUpzz.Factories;
using MakeMeUpzz.Handlers;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeMeUpzz.Repositories
{
    public class UserRepository
    {
        private static MakeMeUpzzDatabaseEntities1 db = DatabaseSingleton.getInstance();

        public static List<User> getAllUsers()
        {
            return db.Users.ToList();
        }

        public static List<String> getAllUsernames()
        {
            return(from x in db.Users select x.Username).ToList();
        }

        public static List<String> getAllPasswords()
        {
            return(from x in db.Users select x.UserPassword).ToList();
        }

        public static User GetUserfromUsernamePassword(String username, String password)
        {
            return (from x in db.Users where x.Username.Equals(username) && x.UserPassword.Equals(password)
                    select x).FirstOrDefault();
        }

        public static int getLastUserID()
        {
            return(from x in db.Users select x.UserID).ToList().LastOrDefault();
        }

        public static void insertUser(int UserID, String Username, String UserEmail, DateTime UserDOB,
            String UserGender, String UserRole, String UserPassword)
        {
            User user = UserFactory.Create(UserID, Username, UserEmail, UserDOB, UserGender,
                UserRole, UserPassword);
            db.Users.Add(user);
            db.SaveChanges();
        }

        public static User FindUserbyID(int UserID)
        {
            User user = db.Users.Find(UserID);
            return user;
        }

        public static User getUserfromID(int UserID)
        {
            return (from x in db.Users where x.UserID.Equals(UserID) select x).FirstOrDefault();
        }

        public static String getPasswordfromID(int UserID)
        {
            return (from x in db.Users where x.UserID.Equals(UserID) select x.UserPassword).ToList().FirstOrDefault();
        }

        public static void updateUser(int UserID, String Username, String UserEmail, DateTime UserDOB,
            String UserGender)
        {
            User user = FindUserbyID(UserID);
            user.UserID = UserID;
            user.Username = Username;
            user.UserEmail = UserEmail;
            user.UserDOB = UserDOB;
            user.UserGender = UserGender;
            db.SaveChanges();
        }

        public static void updatePassword(int UserID, String UserPassword)
        {
            User user = FindUserbyID(UserID);
            user.UserPassword = UserPassword;
            db.SaveChanges();
        }

        public static int generateUserID()
        {
            int newID = 0;
            int lastID = getLastUserID();
            int? lastIDLogic = lastID;
            if (lastIDLogic == null)
            {
                newID = 1;
            }
            else
            {
                newID = lastID;
                newID++;
            }
            return newID;
        }
    }
}