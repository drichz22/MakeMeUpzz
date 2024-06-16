using MakeMeUpzz.Factories;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;

namespace MakeMeUpzz.Repositories
{
    public class TransactionDetailRepository
    {
        private static MakeMeUpzzDatabaseEntities1 db = DatabaseSingleton.getInstance();

        public static List<TransactionDetail> getAllTransactionDetails()
        {
            return db.TransactionDetails.ToList();
        }

        public static List<TransactionDetail> getTransactionDetailsByID(int TransactionID)
        {
            return (from x in db.TransactionDetails where x.TransactionID == TransactionID select x).ToList();
        }

        public static void insertTransactionDetails(int TransactionID, int UserID)
        {
            var carts = CartRepository.getAllCartsbyUserID(UserID);
            List<TransactionDetail> transactionDetails = new List<TransactionDetail>();
            
            foreach (var cart in carts)
            {
                var detail = new TransactionDetail
                {
                    TransactionID = TransactionID,
                    MakeupID = cart.MakeupID,
                    Quantity = cart.Quantity
                };
                transactionDetails.Add(detail); 
            }

            db.TransactionDetails.AddRange(transactionDetails);
            db.SaveChanges();
        }
    }
}