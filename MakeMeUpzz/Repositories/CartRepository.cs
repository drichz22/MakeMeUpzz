using MakeMeUpzz.Factories;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeMeUpzz.Repositories
{
    public class CartRepository
    {
        private static MakeMeUpzzDatabaseEntities1 db = DatabaseSingleton.getInstance();

        public static List<Cart> getAllCarts()
        {
            return db.Carts.ToList();
        }

        public static List<Cart> getAllCartsbyUserID(int UserID)
        {
            return (from x in db.Carts where x.UserID == UserID select x).ToList();
        }

        public static int getLastCartID()
        {
            return (from x in db.Carts select x.CartID).ToList().LastOrDefault();
        }

        public static void insertCart(int CartID, int UserID, int MakeupID, int Quantity)
        {
            var existingCartItem = (from x in db.Carts
                                    where x.MakeupID == MakeupID
                                    && x.UserID == UserID
                                    select x).FirstOrDefault();
            if (existingCartItem != null)
            {
                existingCartItem.Quantity += Quantity;
            }
            else
            {
                Cart cart = CartFactory.Create(CartID, UserID, MakeupID, Quantity);
                db.Carts.Add(cart);
            }
            db.SaveChanges();
        }

        public static void removeAllCartItems(int UserID)
        {
            var allCartItems = (from x in db.Carts where x.UserID == UserID select x);
            db.Carts.RemoveRange(allCartItems);
            db.SaveChanges();
        }

        public static int generateCartID()
        {
            int newID = 0;
            int lastID = getLastCartID();
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