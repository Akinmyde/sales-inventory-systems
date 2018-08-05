using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

namespace Sales_Inventory_System
{
    public partial class Order : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            Handler.FillDropdowunList("Select * from tblCustomer", drpcustomer, "CustomerName");

            if (GridView1.Rows.Count > 0)
            {
                checkoutdiv.Visible = true;
            }
            else
            {
                checkoutdiv.Visible = false;
                txtsubtotal.Text = "#0";
                txtsubtotal1.Text = txtsubtotal.Text.Replace("#", "");
                txtvat.Text = "";
                txttotalpayment.Text = "";
            }

            if (!IsPostBack)
            {
               
                if (Session["datatable"] != null)
                {
                    DataTable dt = (DataTable)Session["datatable"];
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }

                if (Session["subtotal"] != null)
                {
                    txtsubtotal.Text = (string)Session["subtotal"];
                    txtsubtotal1.Text = txtsubtotal.Text.Replace("#", "");
                    txtvat.Text = "";
                    txttotalpayment.Text = "";
                }

               
               
                


                string query = "select * from tblProducts";
                string valuefield = "ProductName";
                string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
                SqlDataAdapter da = new SqlDataAdapter();
                using (SqlConnection con = new SqlConnection(CS))
                {
                    da = new SqlDataAdapter(query, con);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (da != null)
                    {
                        dropdownproducts.DataSource = ds;
                        dropdownproducts.DataValueField = valuefield;
                        dropdownproducts.DataBind();
                        
                    }

                }
                dropdownproducts.Items.Insert(0, new ListItem("---Select an option---", ""));
                
               // Handler.FillDropdowunList(query, dropdownproducts, valuefield);
                if (User.Identity.IsAuthenticated)
                {
                    LinkLogout.CausesValidation = false;
                    Lbluser.Text = User.Identity.Name;
                    // ...

                }
            }
            if (GridView1.Rows.Count > 0)
            {
                checkoutdiv.Visible = true;
            }
            else
            {
                checkoutdiv.Visible = false;
                txtsubtotal.Text = "#0";
                txtsubtotal1.Text = txtsubtotal.Text.Replace("#", "");
                txtvat.Text = "";
                txttotalpayment.Text = "";
            }

        }

        protected void LinkLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void dropdownproducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string sql = "select Quantity, SellingPrice from tblProducts where ProductName=@ProductName";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@ProductName", dropdownproducts.SelectedValue);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows && dropdownproducts.SelectedIndex!=0)
                {
                    if (dr.Read())
                    {
                        int quantity = (int)dr["Quantity"];
                        txtquantityavailabe.Text = Convert.ToString(quantity);
                        txtprice.Text = "#"+(string)dr["SellingPrice"];

                    }
                }
                
            }
        }

        protected void txtquantitytopurchase_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtquantitytopurchase.Text) > Convert.ToDouble(txtquantityavailabe.Text))
            {
                lblerror.Text = "Quantity to purchase cannot be greater than Quantity available";
            }
        }

        protected void btnaddtocart_Click(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();

            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    if (GridView1.Rows[i].Cells[2].Text == Convert.ToString(dropdownproducts.SelectedItem))
            //    {
            //        int currectquantity = Convert.ToInt32(GridView1.Rows[i].Cells[4].Text);
            //        int newquantity =Convert.ToInt32(txtquantitytopurchase.Text);
            //        int sumquantity = currectquantity + newquantity;
            //        GridView1.Rows[i].Cells[4].Text += sumquantity.ToString();

            //        Lbluser.Text = GridView1.Rows[i].Cells[4].Text;
            //       // dt = (DataTable)Session["datatable"];
            //    }
            //}

            if (Page.IsValid)
            {
               // DataTable dt = new DataTable();

                if (dropdownproducts.SelectedIndex == 0)
                {
                    lblerror.Text = "Please select a product";
                }
                else if (txtquantitytopurchase.Text == string.Empty || txtquantitytopurchase.Text.Contains("e"))
                {
                    lblerror.Text = "Quantity is required and must be a digit";
                }

                else if (Convert.ToDouble(txtquantitytopurchase.Text) > Convert.ToDouble(txtquantityavailabe.Text))
                {
                    lblerror.Text = "Quantity to purchase cannot be greater than Quantity available";
                }  
                
                else
                {
                    if (GridView1.Rows.Count == 0)
                    {
                        addtocart();
                    }
                    else
                    {
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {

                            if (GridView1.Rows[i].Cells[2].Text != Convert.ToString(dropdownproducts.SelectedItem))
                            {
                                addtocart();
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Added to Cart')", true);
                                //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                            }
                            else if (GridView1.Rows[i].Cells[2].Text == Convert.ToString(dropdownproducts.SelectedItem))
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "alert('Product Already added to Cart')", true);
                                //Page.Response.Redirect(Page.Request.Url.ToString(), true);
                                //RegisterStartupScript
                                //RegisterClientScriptBlock
                                //int currectquantity = Convert.ToInt32(GridView1.Rows[i].Cells[4].Text);
                                //int newquantity = Convert.ToInt32(txtquantitytopurchase.Text);
                                //int sumquantity = currectquantity + newquantity;
                                //int price = Convert.ToInt32(GridView1.Rows[i].Cells[3].Text.Replace("#",""));
                                //int totalprice = sumquantity * price;
                                //GridView1.Rows[i].Cells[4].Text = sumquantity.ToString();
                                //GridView1.Rows[i].Cells[5].Text = "#"+totalprice.ToString();

                                //total();
                                //DataTable dt = new DataTable(); 
                            }

                        }
                    }
                    
                    

                }
                if (GridView1.Rows.Count > 0)
                {
                    checkoutdiv.Visible = true;
                }
                else
                {
                    checkoutdiv.Visible = false;
                    txtsubtotal.Text = "#0";
                    txtsubtotal1.Text = txtsubtotal.Text.Replace("#", "");
                    txtvat.Text = "";
                    txttotalpayment.Text = "";
                }
                txtsubtotal1.Text = txtsubtotal.Text.Replace("#", "");
                txtvat.Text = "";
                txttotalpayment.Text = "";
            }
        }

        DataTable MakeTable()
        {
            DataTable mydt = new DataTable();

            DataColumn col1 = new DataColumn("INV-NO");
            col1.DataType = System.Type.GetType("System.String");
            mydt.Columns.Add(col1);

            DataColumn col2 = new DataColumn("Product Name");
            col2.DataType = System.Type.GetType("System.String");
            mydt.Columns.Add(col2);

            DataColumn col3 = new DataColumn("Price");
            col3.DataType = System.Type.GetType("System.String");
            mydt.Columns.Add(col3);

            DataColumn col4 = new DataColumn("Quantity");
            col4.DataType = System.Type.GetType("System.String");
            mydt.Columns.Add(col4);

            DataColumn col5 = new DataColumn("Total");
            col5.DataType = System.Type.GetType("System.String");
            mydt.Columns.Add(col5);

           

            return mydt;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            int index = Convert.ToInt32(e.CommandArgument);
            DataTable dt = (DataTable)Session["datatable"];
            dt.Rows[index].Delete();

            Session["datatable"] = dt;
            GridView1.DataSource = dt;
            GridView1.DataBind();

            if (GridView1.Rows.Count > 0)
            {
                checkoutdiv.Visible = true;
            }
            else
            {
                checkoutdiv.Visible = false;
                txtsubtotal.Text = "#0";
            }
            total();
            txtsubtotal1.Text = txtsubtotal.Text.Replace("#", "");
            txtvat.Text = "";
            txttotalpayment.Text = "";
        }

        private void total()
        {
            int sum = 0;

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                string index1 = GridView1.Rows[i].Cells[5].Text.Replace("#", "");
                sum += int.Parse(index1);
            }
            txtsubtotal.Text = "#" + sum.ToString();

            Session["subtotal"] = txtsubtotal.Text;

        }

        protected void btnupdatecart_Click(object sender, EventArgs e)
        {
            total();

            DataTable dt = (DataTable)Session["datatable"];
            GridView1.DataSource = dt;
            GridView1.DataBind();

            Session["datatable"] = dt;

            if (GridView1.Rows.Count > 0)
            {
                checkoutdiv.Visible = true;
            }
            else
            {
                checkoutdiv.Visible = false;
                txtsubtotal.Text = "#0";
            }

            txtsubtotal1.Text = txtsubtotal.Text.Replace("#","");
            txtvat.Text = "";
            txttotalpayment.Text = "";
        }

        protected void btnpurchase_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                Lbluser.Text = txtgrandtotal.Text;
                SqlCommand cmd = new SqlCommand("Insert into tblSales values(@invoiceid,@InvoiceDate,@CustomerId,@Subtotal,@VatPercentage,@VatAmount,@GrandTotal,@TotalPayment,@PaymentDue,@Change)", con);
                //int grandtotal = 0;
                double subtotal = Convert.ToDouble(txtsubtotal1.Text);
                double vatamout = (Convert.ToDouble(txtvat.Text) * subtotal) / 100;
                double grandtotal = vatamout + subtotal;

                double totalpay = Convert.ToDouble(txttotalpayment.Text);
                double paymentdue = 0;
                double change = 0;

                if (totalpay >= grandtotal)
                {
                    change = totalpay - grandtotal;
                    paymentdue = 0;
                }
                else if (totalpay < grandtotal)
                {
                    paymentdue = grandtotal - totalpay;
                    change = 0;
                }

                
                cmd.Parameters.AddWithValue("@invoiceid", Session["Inv-id"]);
                cmd.Parameters.AddWithValue("@InvoiceDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@CustomerId", drpcustomer.SelectedValue);
                cmd.Parameters.AddWithValue("@Subtotal", subtotal);
                cmd.Parameters.AddWithValue("@VatPercentage", txtvat.Text.Trim());
                cmd.Parameters.AddWithValue("@VatAmount", vatamout);
                cmd.Parameters.AddWithValue("@GrandTotal", grandtotal);
                cmd.Parameters.AddWithValue("@TotalPayment", txttotalpayment.Text.Trim());
                cmd.Parameters.AddWithValue("PaymentDue", paymentdue);
                cmd.Parameters.AddWithValue("@Change", change);

                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    // save products sold
                    //var table = "ProductSold";
                    //using (SqlConnection conn = new SqlConnection(CS))
                    //{
                    //    var bulkCopy = new SqlBulkCopy(conn);
                    //    bulkCopy.DestinationTableName = table;
                    //    DataTable dt = new DataTable();
                    //    if (Session["datatable"] != null)
                    //    {
                    //        dt = (DataTable)Session["datatable"];
                    //        conn.Open();
                    //        bulkCopy.WriteToServer(dt);
                    //    }
                    //    // update tblproducts

                    //}
                    for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        using (SqlConnection conn = new SqlConnection(CS))
                        {
                            string sql = "Insert into tblProductSold Values(@invoiceno,@productname,@quantity,@price,@totalamount)";
                            cmd = new SqlCommand(sql, conn);

                            cmd.Parameters.AddWithValue("@invoiceno", GridView1.Rows[i].Cells[1].Text);
                            cmd.Parameters.AddWithValue("@productname", GridView1.Rows[i].Cells[2].Text);
                            cmd.Parameters.AddWithValue("@quantity", GridView1.Rows[i].Cells[3].Text);
                            cmd.Parameters.AddWithValue("@price", GridView1.Rows[i].Cells[4].Text);
                            cmd.Parameters.AddWithValue("@totalamount", GridView1.Rows[i].Cells[5].Text);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }

                    }

                    for (int i = 0; i <= GridView1.Rows.Count - 1; i++)
                    {
                        using (SqlConnection con1 = new SqlConnection(CS))

                        {

                            string sql = "Update tblProducts set Quantity = Quantity - @Quantity where ProductName =  @ProductName";
                            cmd = new SqlCommand(sql, con1);

                            cmd.Parameters.AddWithValue("@Quantity", GridView1.Rows[i].Cells[4].Text);
                            cmd.Parameters.AddWithValue("@ProductName", GridView1.Rows[i].Cells[2].Text);

                            con1.Open();
                            cmd.ExecuteNonQuery();
                        }

                    }
                    //string olu = "Update tblProducts set Quantity = Quantity - " + GridView1.Rows[i].Cells[5].Text + " where ProductName= " + GridView1.Rows[i].Cells[2].Text + "";
                }

            }


        }

        private void addtocart()
        {
                lblerror.Text = "";
                double quantity = Convert.ToDouble(txtquantitytopurchase.Text);
                txtprice.Text.Replace("#", "");
                double price = Convert.ToDouble(txtprice.Text.Replace("#", ""));
                double totalproduct = quantity * price;

                DataTable dt = new DataTable();
                if (GridView1.Rows.Count == 0)
                {
                    dt = MakeTable();
                }
                else
                {
                    dt = (DataTable)Session["datatable"];
                    txtsubtotal.Text = (string)Session["subtotal"];
                    txtsubtotal1.Text = txtsubtotal.Text.Replace("#", "");
                    txtvat.Text = "";
                    txttotalpayment.Text = "";
                }


                if (Session["Inv-id"] == null)
                {
                    string invid = Handler.GenerateId("INV-");
                    Session["Inv-id"] = invid;
                }

                string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("Select InvoiceNo from tblSales where InvoiceNo=@InvoiceNo", con);

                    cmd.Parameters.AddWithValue("@InvoiceNo", Session["Inv-id"]);
                    con.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            string invid = (string)Session["Inv-id"];
                            string id = (string)reader["InvoiceNo"];
                            while (id == invid)
                            {
                                invid = Handler.GenerateId("INV-");
                                Session["Inv-id"] = invid;
                            }
                        }

                    }
                }


                DataRow dr = dt.NewRow();
                dr[0] = Session["Inv-id"];
                dr[1] = dropdownproducts.SelectedValue;
                dr[2] = txtprice.Text;
                dr[3] = txtquantitytopurchase.Text;
                dr[4] = "#" + Convert.ToDouble(totalproduct);


                dt.Rows.Add(dr);
                GridView1.DataSource = null;
                GridView1.DataSource = dt;
                GridView1.DataBind();
                //ViewState.Add("dt", dt);
                Session["datatable"] = dt;

                total();
            }
        }
    }
