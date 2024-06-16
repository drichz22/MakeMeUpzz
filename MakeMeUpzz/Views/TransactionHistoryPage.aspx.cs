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
    public partial class TransactionHistoryPage : System.Web.UI.Page
    {
        List<TransactionHeader> transactions = new List<TransactionHeader>();
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
                if (!IsPostBack)
                {
                    if (user.UserRole.Equals("Customer"))
                    {
                        transactions = TransactionHandler.getAllTransactionByUserID(user.UserID);
                    }
                    else if (user.UserRole.Equals("Admin"))
                    {
                        transactions = TransactionHandler.getAllTransactionHeaders();
                    }
                    TransactionGV.DataSource = transactions;
                    TransactionGV.DataBind();
                }
            }
        }

        protected void TransactionGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = TransactionGV.SelectedRow;
            String TransactionID = row.Cells[0].Text;
            Response.Redirect("~/Views/TransactionDetailPage.aspx?id=" + TransactionID);
        }
    }
}