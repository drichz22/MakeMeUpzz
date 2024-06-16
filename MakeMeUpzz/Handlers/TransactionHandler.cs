using MakeMeUpzz.Factories;
using MakeMeUpzz.Models;
using MakeMeUpzz.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MakeMeUpzz.Handlers
{
    public class TransactionHandler
    {
        //Access Cart Repository
        public static List<Cart> getAllCarts()
        {
            return CartRepository.getAllCarts();
        }

        public static List<Cart> getAllCartsByUserID(int UserID)
        {
            return CartRepository.getAllCartsbyUserID(UserID);
        }

        public static int getLastCartID()
        {
            return CartRepository.getLastCartID();
        }

        public static void insertCart(int CartID, int UserID, int MakeupID, int Quantity)
        {
            CartRepository.insertCart(CartID, UserID, MakeupID, Quantity);
        }

        public static void removeAllCartItems(int UserID)
        {
            CartRepository.removeAllCartItems(UserID);
        }

        public static int generateCartID()
        {
            return CartRepository.generateCartID();
        }

        //Access TransactionHeader Repository
        public static List<TransactionHeader> getAllTransactionHeaders()
        {
            return TransactionHeaderRepository.getAllTransactionHeaders();
        }

        public static int getLastTransactionHeaderID()
        {
            return TransactionHeaderRepository.getLastTransactionHeaderID();
        }

        public static void insertTransactionHeader(int TransactionID, int UserID, DateTime TransactionDate,
            String status)
        {
            TransactionHeaderRepository.insertTransactionHeader(TransactionID, UserID, 
                TransactionDate, status);
        }

        public static List<TransactionHeader> getAllTransactionByUserID(int UserID)
        {
            return TransactionHeaderRepository.getAllTransactionByUserID(UserID);
        }
        public static TransactionHeader getTransactionByID(int TransactionID)
        {
            return TransactionHeaderRepository.getTransactionByID(TransactionID);
        }

        public static TransactionHeader FindTransactionbyID(int TransactionID)
        {
            return TransactionHeaderRepository.FindTransactionbyID(TransactionID);
        }

        public static void updateTransaction(int TransactionID, int UserID, DateTime TransactionDate,
            String Status)
        {
            TransactionHeaderRepository.updateTransaction(TransactionID, UserID, TransactionDate, Status);
        }

        public static int generateTransactionID()
        {
            return TransactionHeaderRepository.generateTransactionID();
        }

        //Access TransactionDetails Repository
        public static List<TransactionDetail> getAllTransactionDetails()
        {
            return TransactionDetailRepository.getAllTransactionDetails();
        }

        public static void insertTransactionDetails(int TransactionID, int UserID)
        {
            TransactionDetailRepository.insertTransactionDetails(TransactionID, UserID);
        }

        public static List<TransactionDetail> getTransactionDetailsByID(int TransactionID)
        {
            return TransactionDetailRepository.getTransactionDetailsByID(TransactionID);
        }
    }
}