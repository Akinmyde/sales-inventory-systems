using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Sales_Inventory_System
{
    public partial class Profile : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)

            {

                Linklogout.CausesValidation = false;
                if (User.Identity.IsAuthenticated)
                {
                    Lbluser.Text = User.Identity.Name;
                    // ...
                    string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(CS))
                    {
                        SqlCommand cmd = new SqlCommand("Select * from tblProfile INNER JOIN tblUsers ON tblProfile.UserId = tblUsers.Id Where Username =@username ", conn);
                        cmd.Parameters.AddWithValue("@username", Lbluser.Text.Trim());

                        conn.Open();


                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                lblfirstname.Text = " " + dr["FirstName"].ToString();
                                lbllastname.Text = " " + dr["LastName"].ToString();
                                Lblduty.Text = " " + dr["Role"].ToString();
                                Lblphone.Text = " " + dr["Phone"].ToString();
                                Lblemail.Text = " " + dr["Email"].ToString();

                                fname.Text = dr["FirstName"].ToString();
                                lname.Text = dr["LastName"].ToString();
                                duty.Text = dr["Role"].ToString();
                                mobile.Text = dr["Phone"].ToString();
                                email.Text = dr["Email"].ToString();


                            }

                        }

                    }


                }
            }
        }

        protected void Linklogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
            //FormsAuthentication.RedirectToLoginPage();
        }




        protected void btnsave_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblProfile INNER JOIN tblUsers ON tblProfile.UserId = tblUsers.Id Where Username =@username ", con);
                cmd.Parameters.AddWithValue("@username", Lbluser.Text.Trim());

                con.Open();


                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        string UID = dr["UserId"].ToString();
                        Handler.UpdateProfile(fname.Text, lname.Text, mobile.Text, UID);

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(),"Message","alert('Changes was saved successfully')",true);
                        //Response.Write("<script>alert('Saved')<script>");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "openModal();", true);

  
                    }

                    

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Changes was not saved')", true);
                }
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "openModal();", true);
                //GetProfile();
                //Page.MaintainScrollPositionOnPostBack = true;
                //SetFocus("#profile");
                
                
            }
           
        }

        protected void changepassword_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                string password = FormsAuthentication.HashPasswordForStoringInConfigFile(txtcurrentpassword.Text, "SHA1");
                string CS = ConfigurationManager.ConnectionStrings["Connect"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    string sql = "Select Password From tblUsers where Username=@username and Password=@password";
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@Username", Lbluser.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", password);


                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            string pword = dr["Password"].ToString();
                            if (password == pword)
                            {
                                Handler.ChangePassword(txtnewpassword.Text.Trim(), Lbluser.Text.Trim(), txtcurrentpassword.Text.Trim());
                                //Lblchangepassword.ForeColor = System.Drawing.Color.Green;
                                //Lblchangepassword.Text = "Password Was Chnanged Successfully";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Password Was Changed Successfully')", true);
                            }
                            else
                            {
                                //Lblchangepassword.ForeColor = System.Drawing.Color.Green;
                                //Lblchangepassword.Text = "Current Password is wrong";
                                //Response.Redirect("profile.aspx");
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Current Password is wrong, nothing was saved')", true);
                            }
                        }
                        
                    }
                    else
                    {
                        //Lblchangepassword.ForeColor = System.Drawing.Color.Green;
                        //Lblchangepassword.Text = "Current Password is wrong";

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Message", "alert('Current Password is wrong, nothing was saved')", true);
                    }

                    //message.ForeColor = System.Drawing.Color.Green;
                    //message.Text = "Password Changed Successfully";
                    //sql = "Update tblUsers Set Password=@NewPassword Where Username=@Username and Password=@Password";
                }
            }
        }
    }
}