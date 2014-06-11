using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using Entities;

namespace Pages
{
    public partial class Pages_Shop : System.Web.UI.Page
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateShopControls();
        }

        protected void btnOrder_Click(object sender, EventArgs e)
        {
            Authenticate();
            ReviewOrder();
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            Authenticate();
            SendOrder();

            lblOrders.Text = "Your order has been placed. Thank you for shopping at our store";
            btnOk.Visible = false;
            btnCancel.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["orders"] = null;
            lblOrders.Visible = false;
            btnOk.Visible = false;
            btnCancel.Visible = false;
        }
        #endregion

        #region Methods

        //Fill page with dynamic controls showing products in database
        private void GenerateShopControls()
        {
            ArrayList coffeeList = ConnectionClass.GetCoffeeByType("%");

            foreach (Coffee coffee in coffeeList)
            {
                //Create Controls
                Panel coffeePanel = new Panel();
                Image image = new Image { ImageUrl = coffee.Image, CssClass = "ProductsImage" };
                Literal literal = new Literal { Text = "<br />" };
                Literal literal2 = new Literal { Text = "<br />" };
                Label lblName = new Label { Text = coffee.Name, CssClass = "ProductsName" };
                Label lblPrice = new Label { Text = String.Format("{0:0.00}",coffee.Price) + "<br />", CssClass = "ProductsPrice" };
                TextBox textBox = new TextBox {ID = coffee.Id.ToString(), CssClass = "ProductsTextBox", Text = "0", Width = 60};

                //Add validation so only numbers can be entered into the textfields
                var validator = new RegularExpressionValidator
                                    {
                                        ValidationExpression = "^[0-9]*",
                                        ControlToValidate = textBox.ID,
                                        ErrorMessage = "Please enter a number."
                                    };

                //Add controls to Panels
                coffeePanel.Controls.Add(image);
                coffeePanel.Controls.Add(literal);
                coffeePanel.Controls.Add(lblName);
                coffeePanel.Controls.Add(literal2);
                coffeePanel.Controls.Add(lblPrice);
                coffeePanel.Controls.Add(textBox);
                coffeePanel.Controls.Add(validator);

                pnlProducts.Controls.Add(coffeePanel);
            }
        }

        //Returns a list of all orders placed in textboxes
        private ArrayList GetOrders()
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");
            ControlFinder<TextBox> cF = new ControlFinder<TextBox>();
            cF.FindChildControlsRecursive(cph);
            IEnumerable<TextBox> textBoxList = cF.FoundControls;
            ArrayList orderList = new ArrayList();

            foreach (TextBox textBox in textBoxList)
            {
                if (textBox.Text != "") 
                {
                    int amountOfOrders = Convert.ToInt32(textBox.Text);
                    if (amountOfOrders > 0)
                    {
                        Coffee coffee = ConnectionClass.GetCoffeeByID(Convert.ToInt32(textBox.ID));
                        Order order = new Order(Session["login"].ToString(), coffee.Name, amountOfOrders, coffee.Price,
                                                DateTime.Now, false);
                        orderList.Add(order);
                    }
                }
            }
            return orderList;
        }

        //Generate string with orders to send to OrderControls
        private void ReviewOrder()
        {
            double totalAmount = 0;
            ArrayList orderList = GetOrders();
            Session["orders"] = orderList;

            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='reviewTable'>");
            sb.Append("<h3>Please review your order:</h3>");

            foreach (Order order in orderList)
            {
                double totalRow = order.Amount * order.Price;
                sb.Append(String.Format(@"<tr>
                                            <td width='50px'>{0} X </td>
                                            <td width='200px'>{1} ({2}) </td>
                                            <td>{3}</td><td>€</td>
                                      </tr>", order.Amount, order.ProductName, order.Price, String.Format("{0:0.00}", totalRow)));
                totalAmount += totalRow;
            }

            sb.Append(string.Format(@"<tr>
                                    <td><b>Total:</b></td>
                                    <td><b>{0} €</b></td>
                                </tr>", totalAmount));
            sb.Append("</table>");
            lblOrders.Text = sb.ToString();

            lblOrders.Visible = true;
            btnOk.Visible = true;
            btnCancel.Visible = true;
        }

        //Send order to database
        private void SendOrder()
        {
            ArrayList orderList = (ArrayList)Session["orders"];
            ConnectionClass.AddOrder(orderList);
            Session["orders"] = null;
        }

        //Check if user is logged in
        private void Authenticate()
        {
            if (Session["login"] == null)
            {
                Response.Redirect("~/Pages/Account/Login.aspx");
            }
        }
        #endregion
    }
}