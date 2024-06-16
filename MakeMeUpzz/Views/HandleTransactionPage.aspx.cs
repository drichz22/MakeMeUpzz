using MakeMeUpzz.Handlers;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace MakeMeUpzz.Viewss
{
    public partial class HandleTransactionPage : System.Web.UI.Page
    {
        List<TransactionHeader> transactions = new List<TransactionHeader>();
        protected void Page_Load(object sender, EventArgs e)
        {
            HandleTransactionView.Visible = false;
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
                if (!IsPostBack && user.UserRole.Equals("Admin"))
                {
                    transactions = TransactionHandler.getAllTransactionHeaders();
                    TransactionGV.DataSource = transactions;
                    TransactionGV.DataBind();
                    HandleTransactionView.Visible = true;
                }               
            }
        }

        protected void HandleBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.CommandArgument);
            GridViewRow row = TransactionGV.Rows[index];

            int TransactionID = Convert.ToInt32(row.Cells[0].Text);
            int UserID = Convert.ToInt32(row.Cells[1].Text);
            DateTime TransactionDate = Convert.ToDateTime(row.Cells[2].Text);
            String TransactionStatus = row.Cells[3].Text;

            if (TransactionStatus.Equals("UNHANDLED"))
            {
                TransactionStatus = "HANDLED";
                TransactionHandler.updateTransaction(TransactionID, UserID, TransactionDate,
                    TransactionStatus);
            }

            transactions = TransactionHandler.getAllTransactionHeaders();
            TransactionGV.DataSource = transactions;
            TransactionGV.DataBind();
            HandleTransactionView.Visible = true;
        }
    }
}