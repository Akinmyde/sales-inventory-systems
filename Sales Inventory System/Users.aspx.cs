using System;
using System.Web.Security;
using System.Web.UI;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Sales_Inventory_System
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // get all user form the database
            
            if (!IsPostBack)
            {

                Handler.Load("Select Username, Email, Role, Locked, LastLoginDate from tblUsers", Gridview1);
                if (User.Identity.IsAuthenticated)
                {
                    
                    Linklogout.CausesValidation = false;
                    Lbluser.Text = User.Identity.Name;
                    // ...

                }
            }
                
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            // reset all field

            Txtusername.Text = "";
            txtpassword.Text = "";
            txtrole.SelectedIndex = -1;

        }

        protected void Linklogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void btnadduser_Click(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                if (Page.IsValid)
                {
                    // Add user to database
                    string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("Select Username, Email from tblUsers Where Username =@username or Email =@email", con);
                        cmd.Parameters.AddWithValue("@username", Txtusername.Text.Trim());
                        cmd.Parameters.AddWithValue("@email", Txtemail.Text.Trim());

                        con.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                string username = dr["Username"].ToString();
                                string email = dr["Email"].ToString();

                                if (Txtusername.Text == username)
                                {
                                    Lblresponse.ForeColor = System.Drawing.Color.Red;
                                    Lblresponse.Text = "Username already in use. Please choose a different one";
                                }

                                if (Txtemail.Text == email)
                                {
                                    Lblresponse.ForeColor = System.Drawing.Color.Red;
                                    Lblresponse.Text = "Email already in use. Please choose a different one";
                                }
                            }


                        }
                        else
                        {
                            Handler.SaveUser(Txtusername.Text, txtpassword.Text, Txtemail.Text, txtrole.SelectedItem.Text, Lblresponse);
                            Handler.Load("Select Username, Email, Role, Locked, LastLoginDate from tblUsers", Gridview1);

                            using (SqlConnection conn = new SqlConnection(CS))
                            {
                                cmd = new SqlCommand("Select Id from tblUsers Where Username =@username", conn);
                                cmd.Parameters.AddWithValue("@username", Txtusername.Text.Trim());

                                conn.Open();

                                dr = cmd.ExecuteReader();
                                string Uid;
                                if (dr.HasRows)
                                {
                                    while (dr.Read())
                                    {
                                         Uid = dr["Id"].ToString();

                                        Handler.AddProfile(Uid);

                                    }
                                    
                                }


                            }
                        }
                        
                       
                        
                    }
                    
                }
            }
        }

        protected void txtsearch_TextChanged(object sender, EventArgs e)
        {
            Handler.Search("Select Username, Email, Role, Locked, LastLoginDate from tblUsers where Username like @search", txtsearch.Text, Gridview1, message);
        }

        
    }
}