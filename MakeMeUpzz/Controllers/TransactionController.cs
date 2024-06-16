using MakeMeUpzz.Handlers;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MakeMeUpzz.Controllers
{
    public class TransactionController
    {
        public String Order(int userID, int makeupID, int quantity)
        {
            String lbl;
            int cartID = TransactionHandler.generateCartID();

            if (quantity <= 0)
            {
                lbl = "Quantity must be greater than 0!";
                return lbl;
            }
            TransactionHandler.insertCart(cartID, userID, makeupID, quantity);
            lbl = "Order Makeup Successful!";
            return lbl;
        }

        public String CheckOut(int userID, List<Cart> carts)
        {
            String lbl;
            if (carts == null)
            {
                lbl = "No Items in Cart!";
                return lbl;
            }
            int transactionID = TransactionHandler.generateTransactionID();
            DateTime transactionDate = DateTime.Now;
            string status = "UNHANDLED";

            TransactionHandler.insertTransactionHeader(transactionID, userID, transactionDate, status);
            TransactionHandler.insertTransactionDetails(transactionID, userID);
            lbl = "Order Successfully Created!";
            TransactionHandler.removeAllCartItems(userID);
            return lbl;
        }
    }
}