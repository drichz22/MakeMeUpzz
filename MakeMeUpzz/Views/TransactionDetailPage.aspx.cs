using MakeMeUpzz.Handlers;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace MakeMeUpzz.Viewss
{
    public partial class TransactionDetailPage : System.Web.UI.Page
    {
        protected List<TransactionDetail> details = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null && Request.Cookies["user_cookie"] == null)
            {
                Response.Redirect("~/Views/LoginPage.aspx");
            }
            else
            {
                User user = new User();
                if (Session["user"] == null)
                {
                    int userID = Convert.ToInt32(Request.Cookies["user_cookie"].Value);
                    user = UserHandler.findUserbyID(userID);
                    Session["user"] = user;
                }
                else
                {
                    user = (User)Session["user"];
                }
                if (Application["user_count"] == null)
                {
                    Application["user_count"] = 1;
                }
                string transactionID = Request.QueryString["id"];
                details = TransactionHandler.getTransactionDetailsByID(Convert.ToInt32(transactionID));
                TransactionHeader transaction = TransactionHandler.getTransactionByID(Convert.ToInt32(transactionID));

                if (transactionID == null || details == null)
                {
                    Response.Redirect("~/Views/TransactionHistoryPage.aspx");
                }
                pageID.InnerText = "Transaction " + transactionID;
                pageDate.InnerText = "Transaction Date: " + transaction.TransactionDate.ToString("yyyy-MM-dd");
            }
        }
    }
}