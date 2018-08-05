using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Configuration;

namespace Sales_Inventory_System
{
    public partial class Categories : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Handler.Load("Select * from tblCategory", Gridview1);
                LinkLogout.CausesValidation = false;
                if (User.Identity.IsAuthenticated)
                {
                    Lbluser.Text = User.Identity.Name;
                }
            }
        }

        protected void LinkLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select CategoryName from tblCategory Where CategoryName =@categoryname", con);
                cmd.Parameters.AddWithValue("@categoryname", txtcategory.Text.Trim());

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string categoryname = dr["CategoryName"].ToString();

                        if (txtcategory.Text == categoryname)
                        {
                            Lblresponse.ForeColor = System.Drawing.Color.Red;
                            Lblresponse.Text = "There is already a category with this name.";
                        }

                    }

                }
                else
                {
                    Handler.Addcategory(txtcategory.Text, Lbluser);
                    Handler.Load("Select * from tblCategory", Gridview1);
                }
                
            }

                  
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {

        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            Handler.Search("Select * from tblCategory where CategoryName like @search", txtsearch.Text, Gridview1,message);
           
        }
    }
}