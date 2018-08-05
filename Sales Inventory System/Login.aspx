<%@ Page EnableEventValidation="false" Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Sales_Inventory_System.Login" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta name="description" content="Creative - Bootstrap 3 Responsive Admin Template" />
    <meta name="author" content="GeeksLabs" />
    <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal" />
    <link rel="shortcut icon" href="Bootstrap/img/favicon.png" />
    <title>Login | Sales Inventory System</title>

    <link href="Bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet" />
    <link href="Bootstrap/css/elegant-icons-style.css" rel="stylesheet" />
    <link href="Bootstrap/css/font-awesome.css" rel="stylesheet" />
    <link href="Bootstrap/css/style.css" rel="stylesheet" />
    <link href="Bootstrap/css/style-responsive.css" rel="stylesheet" />
</head>
<body class="login-img3-body">

    <form id="form1" runat="server" class="login-form">
        <div class="container">
            <div class="login-wrap">
                <p class="login-img"><i class="icon_lock_alt"></i></p>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorUsername" ControlToValidate="txtUsername" runat="server" ForeColor="Red" ErrorMessage="Please Enter Username"></asp:RequiredFieldValidator>

                <div class="input-group">
                    <span class="input-group-addon"><i class="icon_profile"></i></span>
                    <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Username"></asp:TextBox>
                    <br />
                </div>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorPassword" ForeColor="Red" ControlToValidate="txtPassword" runat="server" ErrorMessage="Please Enter Password"></asp:RequiredFieldValidator>

                <div class="input-group">
                    <span class="input-group-addon"><i class="icon_key_alt"></i></span>
                    <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" placeholder="Password" TextMode="Password"></asp:TextBox>
                    <br />
                </div>
                <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidatorlistrole" ForeColor="Red" ControlToValidate="listrole" runat="server" ErrorMessage="Please select a role" InitialValue="-1"></asp:RequiredFieldValidator>

                <div class="col-lg-15">
                    <asp:DropDownList ID="listrole" class="form-control" runat="server">
                        <asp:ListItem Value="-1" Selected="True">-Select Category-</asp:ListItem>
                        <asp:ListItem Value="1">Administrator</asp:ListItem>
                        <asp:ListItem Value="2">Store Kepper</asp:ListItem>
                    </asp:DropDownList><br />
                </div>
                <label class="checkbox">
                    <asp:CheckBox runat="server" id="chkrememberme" text="Remember me" />
                    
                <span class="pull-right"><a href="#">Forgot Password?</a></span>
                </label>
                <div>
                    <asp:Button ID="btnlogin" runat="server" CssClass="btn btn-primary btn-lg btn-block" Text="Login" OnClick="btnlogin_Click" />
                    <br />
                    <asp:Label ID="lblmessage" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </form>

</body>
</html>

