﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sales_Inventory_System
{
    public partial class Products : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtprofit.Text = "0";
                txtcostprice.Text = "0";
                txtsellingprice.Text = "0";

                GridviewProducts.EnableSortingAndPagingCallbacks = false;

                Handler.Load("Select ProductName, Quantity, CostPrice, SellingPrice from tblProducts", GridviewProducts);

                //Handler.FillDropdowunList("Select ProductName from tblProducts", dropdowncategory, "ProductName");
                Handler.FillDropdowunList("Select CategoryName from tblCategory", dropdowncategory, "CategoryName");

                if (User.Identity.IsAuthenticated)
                {
                    LinkLogout.CausesValidation = false;
                    Lbluser.Text = User.Identity.Name;
                    // ...

                }
            }
        }
        protected void LinkLogout_Click(object sender, EventArgs e)
        {

            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");

        }

        protected void search_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtcostprice_TextChanged(object sender, EventArgs e)
        {
            //double cp = Convert.ToDouble(txtcostprice.Text);
            //double sp = Convert.ToDouble(txtsellingprice.Text);
            //double profit = sp - cp;
            //txtprofit.Text = Convert.ToDouble(profit) + " Per One";

            //txtprofit.Text = string.Format("{O:C}", profit).ToString();
        }
        protected void txtsellingprice_TextChanged(object sender, EventArgs e)
        {
            //double cp = Convert.ToDouble(txtcostprice.Text);
            //double sp = Convert.ToDouble(txtsellingprice.Text);
            //double profit = sp - cp;
            //txtprofit.Text = "#" + Convert.ToDouble(profit) + " Per One";

            //txtprofit.Text = string.Format("{0:C}", profit).ToString();



        }

        protected void btnupload_Click(object sender, EventArgs e)
        {

            if (btnupload.Text == "Upload")
            {
                GridviewProducts.Visible = false;

                if (FileUpload1.HasFile)
                {
                    string fileextention = System.IO.Path.GetExtension(FileUpload1.FileName);
                    //FileInfo fileInfo = new FileInfo(FileUpload1.PostedFile.FileName);
                    //if (fileInfo.Name.Contains(".csv"))

                    if (fileextention.ToLower() == ".csv")
                    {
                        string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
                        FileUpload1.SaveAs(csvPath);

                        //Create a DataTable.  
                        DataTable dt = new DataTable();
                        dt.Columns.AddRange(new DataColumn[6] {
                        new DataColumn("Id", typeof(string)),
                        new DataColumn("Product Name", typeof(string)),
                        new DataColumn("Quantity", typeof(string)),
                        new DataColumn("Cost Price", typeof(string)),
                        new DataColumn("Selling Price",typeof(string)),
                        new DataColumn("Category", typeof(string)) });

                    //Read the contents of CSV file.  
                    string csvData = File.ReadAllText(csvPath);

                        foreach (string row in csvData.Split('\n'))
                        {

                            if (!string.IsNullOrEmpty(row))
                            {
                                dt.Rows.Add();
                                int i = 1;

                                //Execute a loop over the columns.  
                                foreach (string cell in row.Split(','))
                                {
                                    for (int j = 1; j <= dt.Rows.Count; j++)
                                    {
                                        dt.Rows[dt.Rows.Count - 1][0] = j;
                                    }
                                    //string id = Handler.GenerateId();
                                    //
                                    dt.Rows[dt.Rows.Count - 1][i] = cell;
                                    i++;
                                }


                            }
                        }


                        //Bind the DataTable.
                        Session["Dt"] = dt;
                        GridviewUpload.Visible = true;
                        GridviewUpload.DataSource = dt;
                        GridviewUpload.DataBind();
                        if (GridviewUpload.Rows.Count > 0)
                        {
                            btnupload.Text = "Save this upload";
                            btncancel.Visible = true;
                            lblupload.ForeColor = System.Drawing.Color.Green;
                            lblupload.Text = "Upload was successfull <br/><b>Please Preview before saving</b";

                        }


                    }
                    else
                    {
                        lblupload.ForeColor = System.Drawing.Color.Red;
                        lblupload.Text = "Only .csv files are allowed";
                    }
                }
                else
                {
                    lblupload.ForeColor = System.Drawing.Color.Red;
                    lblupload.Text = "Upload a file(.csv)";
                }


            }
            else if (btnupload.Text == "Save this upload")
            {
                string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;

                //using (SqlConnection con = new SqlConnection(CS))
                //{
                //    SqlCommand cmd = new SqlCommand("Select * From tblCategory", con);
                //    con.Open();
                //    SqlDataReader dr = cmd.ExecuteReader();
                //    for (int i = 0; i <= GridviewProducts.Rows.Count - 1; i++)
                //    {
                //        if (GridviewProducts.Rows[i].Cells[6].Text == (string)dr["CategoryName"])
                //        {
                //            Lbluser.Text = "THERE is an error";
                //        }
                //        //GridviewProducts.Rows[i].Cells[6].Text 
                //    }
                //}



                var table = "tblProducts";
                using (SqlConnection con = new SqlConnection(CS))
                {
                    var bulkCopy = new SqlBulkCopy(con);
                    bulkCopy.DestinationTableName = table;
                    DataTable dt = new DataTable();
                    dt = (DataTable)Session["Dt"];
                    con.Open();
                    bulkCopy.WriteToServer(dt);
                    lblupload.ForeColor = System.Drawing.Color.Green;
                    lblupload.Text = "File was saved successfully";
                    btncancel.Enabled = false;
                    btnupload.Text = "Upload";
                }
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            GridviewUpload.DataSource = null;
            GridviewUpload.DataBind();
            GridviewUpload.Visible = false;
            GridviewProducts.Visible = true;
            btnupload.Text = "Upload";
            lblupload.ForeColor = System.Drawing.Color.Blue;
            lblupload.Text = "Upload Multiple Product";
            btncancel.Visible = false;
            Handler.Load("Select ProductName, Quantity, CostPrice, SellingPrice from tblProducts", GridviewProducts);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            SqlConnection con = new SqlConnection(CS);
            string query = "Insert Into tblProducts Values(@productname,@quantity,@cp,@sp,@category)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("productname", TxtProductName.Text.Trim());
            cmd.Parameters.AddWithValue("quantity", txtQuantity.Text.Trim());
            cmd.Parameters.AddWithValue("cp", txtcostprice.Text.Trim());
            cmd.Parameters.AddWithValue("sp", txtsellingprice.Text.Trim());

            cmd.Parameters.AddWithValue("category", txtQuantity.Text.Trim());
            bool olu;
            con.Open();
            olu = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (olu)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Product Was Added Successfully')", true);
                Handler.Load("Select ProductName, Quantity, CostPrice, SellingPrice from tblProducts", GridviewProducts);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('There was an error, nothing was saved')", true);
                Handler.Load("Select ProductName, Quantity, CostPrice, SellingPrice from tblProducts", GridviewProducts);
            }

        }
    }
}