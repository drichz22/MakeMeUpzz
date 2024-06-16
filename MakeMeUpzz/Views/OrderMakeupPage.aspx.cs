using MakeMeUpzz.Controllers;
using MakeMeUpzz.Handlers;
using MakeMeUpzz.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MakeMeUpzz.Viewss
{
    public partial class OrderMakeupPage : System.Web.UI.Page
    {
        List<Makeup> makeups = new List<Makeup>();
        List<Cart> carts = new List<Cart>();
        TransactionController transactionController = new TransactionController();
        protected void Page_Load(object sender, EventArgs e)
        {
            OrderMakeupView.Visible = false;

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

                if (user.UserRole.Equals("Customer"))
                {
                    OrderMakeupView.Visible = true;
                    if (!IsPostBack)
                    {
                        makeups = MakeupHandler.getAllMakeups();
                        MakeupGV.DataSource = makeups;
                        MakeupGV.DataBind();

                        carts = TransactionHandler.getAllCarts();
                        CartGV.DataSource = carts;
                        CartGV.DataBind();
                    }                   
                }
                else
                {
                    Response.Redirect("~/Views/HomePage.aspx");
                }
            }
        }

        protected void OrderBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int index = Convert.ToInt32(btn.CommandArgument);
            GridViewRow row = MakeupGV.Rows[index];
            String MakeupName = row.Cells[0].Text;
            int MakeupPrice = Convert.ToInt32(row.Cells[1].Text);
            int MakeupWeight = Convert.ToInt32(row.Cells[2].Text);
            String MakeupTypeName = row.Cells[3].Text;
            int MakeupTypeID = MakeupHandler.getMakeupTypeIDbyName(MakeupTypeName);
            String MakeupBrandName = row.Cells[4].Text;
            int MakeupBrandID = MakeupHandler.getMakeupBrandIDbyName(MakeupBrandName);
            TextBox quantityTextBox = (TextBox)row.FindControl("OrderMakeupQuantityTB");
            Label errorLabel = (Label)row.FindControl("OrderLblError");

            int CartID = TransactionHandler.generateCartID();
            int MakeupID = MakeupHandler.getMakeupID(MakeupName, MakeupPrice, MakeupWeight,
                MakeupTypeID, MakeupBrandID);
            User user = (User)Session["user"];
            int userID = user.UserID;
            int Quantity = Convert.ToInt32(quantityTextBox.Text);

            String validation = "Order Makeup Successful!";
            errorLabel.Text = transactionController.Order(userID, MakeupID, Quantity);
            if (validation.Equals(errorLabel.Text))
            {
                makeups = MakeupHandler.getAllMakeups();
                MakeupGV.DataSource = makeups;
                MakeupGV.DataBind();

                carts = TransactionHandler.getAllCarts();
                CartGV.DataSource = carts;
                CartGV.DataBind();

                OrderMakeupView.Visible = true;
            }
        }

        protected void ClearCartBtn_Click(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            int userID = user.UserID;
            TransactionHandler.removeAllCartItems(userID);

            makeups = MakeupHandler.getAllMakeups();
            MakeupGV.DataSource = makeups;
            MakeupGV.DataBind();

            carts = TransactionHandler.getAllCarts();
            CartGV.DataSource = carts;
            CartGV.DataBind();

            OrderMakeupView.Visible = true;
        }

        protected void CheckoutBtn_Click(object sender, EventArgs e)
        {
            User user = (User)Session["user"];
            int userID = user.UserID;
            carts = TransactionHandler.getAllCartsByUserID(userID);

            String validate = "Order Successfully Created!";
            CartLblError.Text = transactionController.CheckOut(userID, carts);

            if (validate.Equals(CartLblError.Text))
            {
                CartLblError.ForeColor = System.Drawing.Color.Green;
                carts = TransactionHandler.getAllCartsByUserID(userID);
                CartGV.DataSource = carts;
                CartGV.DataBind();
            }
            OrderMakeupView.Visible = true;
        }
    }
}