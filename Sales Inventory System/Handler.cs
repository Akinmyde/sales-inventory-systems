using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sales_Inventory_System
{
    public class Handler
    {


        public static string GenerateId(string slug)
        {
            Random rnd = new Random();
            int id = rnd.Next(10000);
            string PID = slug + id;
            return PID;
        }
        public static void Load(string query, GridView gridview)
        {

            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection con = new SqlConnection(CS))
            {
                da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (da != null)
                {
                    gridview.DataSource = ds;
                    gridview.DataBind();
                }

            }
        }
             public static void FillDropdowunList(string query, DropDownList dropdownlist, string valuefield)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection con = new SqlConnection(CS))
            {
                da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (da != null)
                {
                    dropdownlist.DataSource = ds;
                    dropdownlist.DataTextField = valuefield;
                    //dropdownlist.DataValueField = valuefield;
                    dropdownlist.DataBind();
                   

                }

            }

        }
    
   

        public static void Retry(int retry, string username)
        {
          string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string sql = "Update tblUsers Set RetryAttempts=@retry where Username=@username";
                SqlCommand cmd = new SqlCommand(sql, con);
                //int retry = 0;
                cmd.Parameters.AddWithValue("@retry", retry + 1);
                cmd.Parameters.AddWithValue("@username", username.Trim());
                

                con.Open();

                cmd.ExecuteNonQuery();

            }
        }

        public static void LockAccount(string lockeddate, string username, Label response)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string sql = "Update tblUsers Set Locked=@locked, LockedDate=@lockeddate where Username=@username";
                SqlCommand cmd = new SqlCommand(sql, con);
                byte locked = 1;
                cmd.Parameters.AddWithValue("@locked", locked);
                cmd.Parameters.AddWithValue("@lockeddate", lockeddate);
                cmd.Parameters.AddWithValue("@username", username);



                con.Open();

                cmd.ExecuteNonQuery();

                response.ForeColor = System.Drawing.Color.Red;
                response.Text = "Account locked please contact the admin";

            }
        }
        public static void Search(string query, string parameter, GridView gridview, Label message)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                
                
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@search", parameter + "%");
                con.Open();
                gridview.DataSource = cmd.ExecuteReader();
                gridview.DataBind();
                if (gridview.Rows.Count == 0)
                {
                    message.ForeColor = System.Drawing.Color.Red;
                    message.Text = "Nothing Found";
                }
                else if (gridview.Rows.Count > 0)
                {
                    message.Visible = false;
                }


            }
        }

        public static void UpdateLogin(byte locked, DBNull lockdate, string username, string password)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string sql = "Update tblUsers Set LastLoginDate=@lastlogin, RetryAttempts=@retry, Locked=@locked, LockedDate=@lockeddate where Username=@username and Password=@password";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@lastlogin", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@retry", 0);
                cmd.Parameters.AddWithValue("@locked", locked);
                cmd.Parameters.AddWithValue("@lockeddate", lockdate);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                con.Open();

                cmd.ExecuteNonQuery();
                
            }
        }
        public static void SaveUser(string username, string password, string email, string role, Label response)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                
                password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
                byte userlock = 0;
                SqlCommand cmd = new SqlCommand("Insert into tblUsers values (@username, @password, @email, @role, @createddate, @lastlogindate, @retry, @locked, @lockeddate)", con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@role", role);
                cmd.Parameters.AddWithValue("@createddate", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@lastlogindate", DBNull.Value);
                cmd.Parameters.AddWithValue("@retry", DBNull.Value);
                cmd.Parameters.AddWithValue("@locked", userlock);
                cmd.Parameters.AddWithValue("@lockeddate", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();
                response.ForeColor = System.Drawing.Color.Green;
                response.Text = "User data was saved Successfull";

            }

        }

        public static void Addcategory(string categoryname, Label response)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insert into tblCategory values (@categoryname)", con);
                cmd.Parameters.AddWithValue("@categoryname", categoryname);
                
                con.Open();

                cmd.ExecuteNonQuery();
                response.ForeColor = System.Drawing.Color.Green;
                response.Text = "saved Successfull";

            }
        }

        public static void AddProduct(string Id,string Txtproductname, string txtabtproduct, string Drpcategory, Label response)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insert into tblProducts values (@Id,@ProductName,@AboutProduct,@Category,@CostPrice,@SellinPrice,@Profit,@QuantityAvailable)", con);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@ProductName", Txtproductname);
                cmd.Parameters.AddWithValue("@AboutProduct", txtabtproduct);
                cmd.Parameters.AddWithValue("@Category", Drpcategory);
                cmd.Parameters.AddWithValue("@CostPrice", DBNull.Value);
                cmd.Parameters.AddWithValue("@SellinPrice", DBNull.Value);
                cmd.Parameters.AddWithValue("@Profit", DBNull.Value);
                cmd.Parameters.AddWithValue("@QuantityAvailable", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();
                response.ForeColor = System.Drawing.Color.Green;
                response.Text = "Saved Successfull";

            }
        }

        public static void AddProfile(string Uid)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Insert into tblProfile values (@UserId,@FirstName,@LastName,@Phone)", con);
                cmd.Parameters.AddWithValue("@UserId", Uid);
                cmd.Parameters.AddWithValue("@FirstName", DBNull.Value);
                cmd.Parameters.AddWithValue("@LastName", DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", DBNull.Value);

                con.Open();

                cmd.ExecuteNonQuery();

            }
            
        }

        public static void UpdateProfile(string fname, string lname, string phone, string UserId)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string sql = "Update tblProfile Set FirstName=@FirstName,LastName=@LastName,Phone=@Phone Where UserId=@UserId";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@FirstName", fname);
                cmd.Parameters.AddWithValue("@LastName", lname);
                cmd.Parameters.AddWithValue("@Phone", phone);
                cmd.Parameters.AddWithValue("@UserId", UserId);

                con.Open();

                cmd.ExecuteNonQuery();
                //message.ForeColor = System.Drawing.Color.Green;
                //message.Text = "Profile Update Successfully";

            }
        }

        public static void ChangePassword(string newpassword, string username, string currentpassword)
        {
            newpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(newpassword, "SHA1");
            currentpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(currentpassword, "SHA1");
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                string sql = "Update tblUsers Set Password=@NewPassword Where Username=@Username and Password=@Password";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@NewPassword", newpassword);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", currentpassword);
                
                con.Open();

                cmd.ExecuteNonQuery();

            }
        }

        public static void LoadProduct(string query, DropDownList dropdownlist)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            SqlDataAdapter da = new SqlDataAdapter();
            using (SqlConnection con = new SqlConnection(CS))
            {
                da = new SqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (da != null)
                {
                    dropdownlist.DataSource = ds;
                    dropdownlist.DataValueField = "CategoryName";
                    dropdownlist.DataBind();
                }

            }

        }


        public static bool Save(string query, List<SqlParameter> Parameters)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                
                foreach(SqlParameter parameter in Parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                con.Open();
                return Convert.ToBoolean(cmd.ExecuteScalar());
                //cmd.ExecuteNonQuery();
               

            }
        }

    }
}