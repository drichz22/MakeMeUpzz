using MakeMeUpzz.Factories;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeMeUpzz.Repositories
{
    public class TransactionHeaderRepository
    {
        private static MakeMeUpzzDatabaseEntities1 db = DatabaseSingleton.getInstance();

        public static List<TransactionHeader> getAllTransactionHeaders()
        {
            return db.TransactionHeaders.ToList();
        }

        public static int getLastTransactionHeaderID()
        {
            return (from x in db.TransactionHeaders select x.TransactionID).ToList().LastOrDefault();
        }

        public static List<TransactionHeader> getAllTransactionByUserID(int userID)
        {
            return (from x in db.TransactionHeaders where userID == x.UserID select x).ToList();
        }

        public static TransactionHeader getTransactionByID(int transactionID)
        {
            return (from x in db.TransactionHeaders where transactionID == x.TransactionID select x).FirstOrDefault();
        }

        public static void insertTransactionHeader(int TransactionID, int UserID, DateTime TransactionDate,
            String status)
        {
            TransactionHeader transactionHeader = TransactionHeaderFactory.Create(TransactionID,
                UserID, TransactionDate, status);
            db.TransactionHeaders.Add(transactionHeader);
            db.SaveChanges();
        }

        public static TransactionHeader FindTransactionbyID(int TransactionID)
        {
            TransactionHeader transactionHeader = db.TransactionHeaders.Find(TransactionID);
            return transactionHeader;
        }

        public static void updateTransaction(int TransactionID, int UserID, DateTime TransactionDate,
            String Status)
        {
            TransactionHeader updateTransactionHeader = FindTransactionbyID(TransactionID);
            updateTransactionHeader.TransactionID = TransactionID;
            updateTransactionHeader.UserID = UserID;
            updateTransactionHeader.TransactionDate = TransactionDate;
            updateTransactionHeader.Status = Status;
            db.SaveChanges();
        }

        public static int generateTransactionID()
        {
            int newID = 0;
            int lastID = getLastTransactionHeaderID();
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