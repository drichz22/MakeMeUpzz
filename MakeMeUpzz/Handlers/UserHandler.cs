using MakeMeUpzz.Models;
using MakeMeUpzz.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeMeUpzz.Handlers
{
    public class UserHandler
    {
        public static List<User> getAllUsers()
        {
            return UserRepository.getAllUsers();
        }

        public static List<String> getAllUsernames()
        {
            return UserRepository.getAllUsernames();
        }

        public static int getLastUserID()
        {
            return UserRepository.getLastUserID();
        }

        public static User findUserbyID(int userID)
        {
            return UserRepository.FindUserbyID(userID);
        }

        public static User GetUserfromUsernamePassword(String username, String password) {
            return UserRepository.GetUserfromUsernamePassword(username, password);
        }

        public static void insertUser(int UserID, String Username, String UserEmail, DateTime UserDOB,
            String UserGender, String UserRole, String UserPassword)
        {
            UserRepository.insertUser(UserID, Username, UserEmail, UserDOB, UserGender,
                UserRole, UserPassword);
        }

        public static User getUserfromID(int UserID)
        {
            return UserRepository.getUserfromID(UserID);
        }

        public static String getPasswordfromID(int UserID)
        {
            return UserRepository.getPasswordfromID(UserID);
        }

        public static void updateUser(int UserID, String Username, String UserEmail, DateTime UserDOB,
            String UserGender)
        {
            UserRepository.updateUser(UserID, Username, UserEmail, UserDOB, UserGender);
        }

        public static void updatePassword(int UserID, String Password)
        {
            UserRepository.updatePassword(UserID, Password);
        }

        public static int generateUserID()
        {
            return UserRepository.generateUserID();
        }
    }
}