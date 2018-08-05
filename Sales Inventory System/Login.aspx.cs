using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;

namespace Sales_Inventory_System
{

    public partial class Login : System.Web.UI.Page
    {
        int retry;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (User.Identity.IsAuthenticated)
                {

                    Response.Redirect("~/Dashboard.aspx");
                    // ...
                }
                //    if (Request.Browser.Cookies)
                //    {
                //        if (Request.QueryString["CheckCookie"] == null)
                //        {
                //            HttpCookie cookie = new HttpCookie("TestCookie", "1");
                //            Response.Cookies.Add(cookie);
                //            Response.Redirect("login.aspx?CheckCookie=1");

                //        }
                //        else
                //        {
                //            HttpCookie cookie = Request.Cookies["TestCookie"];
                //            if (cookie == null)
                //            {
                //                lblmessage.Text = "This website needs cookie to run," + " <br/>" + "We have detected that cookies are disabled on your browser please enable it";
                //            }
                //        }

                //    }
                //    else
                //    {
                //        lblmessage.Text = ("Your browser doesn't support cookies. Please install one of the latest browsers");
                //    }
            }

        }


        protected void btnlogin_Click(object sender, EventArgs e)
        {
            //Check if is a post back to the server
            if (IsPostBack)
            {
                //Check is the page is valid
                if (Page.IsValid)
                {
                    //Check if the Account is locked or opened
                    string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(CS))
                    {
                        string query = "Select Locked from tblUsers where Username=@username";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);

                        con.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                if (Convert.ToBoolean(dr["Locked"]))
                                {
                                    lblmessage.ForeColor = System.Drawing.Color.Red;
                                    lblmessage.Text = "Account locked please contact the admin";
                                }
                                else
                                {
                                    //Login to the dashboard
                                    CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
                                    using (SqlConnection connection = new SqlConnection(CS))
                                    {
                                        string password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
                                        cmd = new SqlCommand("Select Username, Password from tblUsers Where Username =@username and Password =@password", connection);
                                        cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                                        cmd.Parameters.AddWithValue("@password", password.Trim());

                                        connection.Open();

                                        dr = cmd.ExecuteReader();


                                        if (dr.Read())
                                        {

                                            //byte locked = 1;
                                            DBNull lockeddate = DBNull.Value;
                                            FormsAuthentication.RedirectFromLoginPage(txtUsername.Text, chkrememberme.Checked);
                                            Handler.UpdateLogin(0, lockeddate, txtUsername.Text, password);
                                        }
                                        else
                                        {


                                            CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
                                            using (SqlConnection conn = new SqlConnection(CS))
                                            {
                                                cmd = new SqlCommand("Select RetryAttempts from tblUsers Where Username =@username", conn);
                                                cmd.Parameters.AddWithValue("@username", txtUsername.Text.Trim());

                                                conn.Open();

                                                dr = cmd.ExecuteReader();

                                                if (dr.HasRows)
                                                {
                                                    if (dr.Read())
                                                    {
                                                        retry = (int)dr["RetryAttempts"];
                                                        if ((4 - retry) == 0)
                                                        {
                                                            Handler.LockAccount(DateTime.Now.ToString(), txtUsername.Text.Trim(), lblmessage);
                                                        }

                                                        else if (retry < 5)
                                                        {
                                                            Handler.Retry(retry, txtUsername.Text);
                                                            lblmessage.ForeColor = System.Drawing.Color.Red;
                                                            lblmessage.Text = "Invalid Username or Password <br/> " + (4 - retry) + " Retry Attempt (s) Remaining";
                                                        }


                                                    }


                                                }

                                                
                                            }



                                        }

                                    }


                                }
                            }
                        }
                        else
                        {
                            lblmessage.ForeColor = System.Drawing.Color.Red;
                            lblmessage.Text = "Invalid Username or Password";
                        }
                    }
                    

                }
               
            }
        }




    }
}


    
