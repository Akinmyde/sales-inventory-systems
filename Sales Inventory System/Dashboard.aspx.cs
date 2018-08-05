using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security;
using System.Web.Security;

namespace Sales_Inventory_System
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
               
            {

                if (User.Identity.IsAuthenticated)
                {
                    Lbluser.Text = User.Identity.Name;
                    // ...
                    
                }
            }
        }

        

        protected void Linklogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
            //FormsAuthentication.RedirectToLoginPage();

        }
    }
}