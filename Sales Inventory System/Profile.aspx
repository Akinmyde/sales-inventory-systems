<%@ Page Language="C#" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Sales_Inventory_System.Profile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <meta charset="utf-8"/>
    
  <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
  <meta name="description" content="Creative - Bootstrap 3 Responsive Admin Template"/>
  <meta name="author" content="GeeksLabs"/>
  <meta name="keyword" content="Creative, Dashboard, Admin, Template, Theme, Bootstrap, Responsive, Retina, Minimal"/>
  <link rel="shortcut icon" href="Bootstrap/img/favicon.png"/>

  <title>Sales Inventory System</title>

  <!-- Bootstrap CSS -->
  <link href="Bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
  <!-- bootstrap theme -->
  <link href="Bootstrap/css/bootstrap-theme.css" rel="stylesheet"/>
  <!--external css-->
  <!-- font icon -->
  <link href="Bootstrap/css/elegant-icons-style.css" rel="stylesheet" />
  <link href="Bootstrap/css/font-awesome.min.css" rel="stylesheet" />
  <!-- full calendar css-->
  <link href="Bootstrap/assets/fullcalendar/fullcalendar/bootstrap-fullcalendar.css" rel="stylesheet" />
  <link href="Bootstrap/assets/fullcalendar/fullcalendar/fullcalendar.css" rel="stylesheet" />
  <!-- easy pie chart-->
  <link href="Bootstrap/assets/jquery-easy-pie-chart/jquery.easy-pie-chart.css" rel="stylesheet" type="text/css" media="screen" />
  <!-- owl carousel -->
  <link rel="stylesheet" href="Bootstrap/css/owl.carousel.css" type="text/css"/>
  <link href="Bootstrap/css/jquery-jvectormap-1.2.2.css" rel="stylesheet"/>
  <!-- Custom styles -->
  <link rel="stylesheet" href="Bootstrap/css/fullcalendar.css"/>
  <link href="Bootstrap/css/widgets.css" rel="stylesheet"/>
  <link href="Bootstrap/css/style.css" rel="stylesheet"/>
  <link href="Bootstrap/css/style-responsive.css" rel="stylesheet" />
  <link href="Bootstrap/css/xcharts.min.css" rel=" stylesheet"/>
  <link href="Bootstrap/css/jquery-ui-1.10.4.min.css" rel="stylesheet"/>
  
</head>
<body>
    <form id="form1" runat="server">
   <section id="container" class="">
    <!--header start-->
    <header class="header dark-bg">
      <div class="toggle-nav">
        <div class="icon-reorder tooltips" data-original-title="Toggle Navigation" data-placement="bottom"><i class="icon_menu"></i></div>
      </div>

      <!--logo start-->
       <a href="Dashboard.aspx" class="logo">SALES <span class="lite">INVENTORY </span> <span class="logo"></span> SYSTEM</a>
      <!--logo end-->

         <div class="nav search-row" id="top_menu">
        <!--  search form start -->
        <ul class="nav top-menu">
          <li>
           <%-- <form class="navbar-form">--%>
              <%--<input class="form-control" placeholder="Search" type="text">--%>
           <%-- </form>--%>
          </li>
        </ul>
        <!--  search form end -->
      </div>

      <div class="top-nav notification-row">
        <!-- notificatoin dropdown start-->
        <ul class="nav pull-right top-menu">

          <li class="dropdown">
            <a data-toggle="dropdown" class="dropdown-toggle" href="#">
                            <span class="profile-ava">
                                <img alt="" src="Bootstrap/img/avatar1_small.jpg" />
                            </span>
                            <span class="username">
                                <asp:Label ID="Lbluser" runat="server"></asp:Label>

                            </span>
                            <b class="caret"></b>
                        </a>
            <ul class="dropdown-menu extended logout">
              <div class="log-arrow-up"></div>
              <li class="eborder-top">
                <a href="Profile.aspx"><i class="icon_profile"></i> My Profile</a>
              </li>
              
              <li>
                  <asp:LinkButton ID="Linklogout" runat="server" OnClick="Linklogout_Click"><i class="icon_key_alt"></i>Log out</asp:LinkButton>
              </li>
             
            </ul>
          </li>
          <!-- user login dropdown end -->
        </ul>
        <!-- notificatoin dropdown end-->
      </div>
    </header>
    <!--header end-->

    <!--sidebar start-->
 <aside>
      <div id="sidebar" class="nav-collapse ">
        <!-- sidebar menu start-->
        <ul class="sidebar-menu">
          <li class="active">
            <a class="" href="Dashboard.aspx">
                          <i class="icon_house_alt"></i>
                          <span>Dashboard</span>
                      </a>
          </li>
             <li class="active">
            <a class="" href="Profile.aspx"><i class="fa fa-briefcase"></i><span>Profile</span></a>
          </li>
          
            <li class="sub-menu">
            <a href="javascript:;" class="">
                          <i class="fa fa-envelope"></i>
                          <span>Manage</span>
                          <span class="menu-arrow arrow_carrot-right"></span>
                      </a>
            <ul class="sub">
                <li>
                                    <a class="" href="Users.aspx"><i class=""></i><span>Users</span></a>
                                </li>
                 <li>
            <a class="" href="Products.aspx"><i class=""></i><span>Products</span></a>
         </li>
                     <li>
            <a class="" href="Customers.aspx"><i class=""></i><span>Customers</span></a>
         </li>
                <li>
                                    <a class="" href="Categories.aspx"><i class=""></i><span>Category</span></a>
                                </li>
            </ul>
          </li>
            <li class="sub-menu">
            <a href="javascript:;" class="">
                          <i class="fa fa-book"></i>
                          <span>Report</span>
                          <span class="menu-arrow arrow_carrot-right"></span>
                      </a>
            <ul class="sub">
              <li>
            <a class="" href="#"><i class=""></i><span>Registration</span></a>
         </li>
         <li>
            <a class="" href="#"><i class=""></i><span>Sales</span></a>
         </li>
                 <li>
            <a class="" href="#"><i class=""></i><span>Orders</span></a>
         </li>
                 <li>
            <a class="" href="#"><i class=""></i><span>Invoice</span></a>
         </li>
            </ul>
          </li>
         </ul>
        <!-- sidebar menu end-->
      </div>
        
    </aside>
    <!--sidebar end-->

    <!--main content start-->
    <section id="main-content">
      <section class="wrapper">
        <div class="row">
          <div class="col-lg-12">
            <h3 class="page-header"><i class="fa fa-user-md"></i> Profile</h3>
            <ol class="breadcrumb">
              <li><i class="fa fa-home"></i><a href="Dashboard.aspx">Home</a></li>
              <li><i class="fa fa-user-md"></i>Profile</li>
            </ol>
          </div>
        </div>
        <div class="row">
          <!-- profile-widget -->
          <div class="col-lg-12">
            <div class="profile-widget profile-widget-info">
              <div class="panel-body">
                <div class="col-lg-2 col-sm-2">
                  <h4>Akinremi Olumide</h4>
                  <div class="follow-ava">
                    <img src="Bootstrap/img/profile-widget-avatar.jpg" alt=""/>
                  </div>
                  <h6>Administrator</h6>
                </div>
              
              </div>
            </div>
          </div>
        </div>
        <!-- page start-->
        <div class="row">
          <div class="col-lg-12">
            <section class="panel">
              <header class="panel-heading tab-bg-info">
                <ul class="nav nav-tabs">
                  <li class="active">
                    <a data-toggle="tab" href="#recent-activity">
                                          <i class="icon-home"></i>
                                          Daily Activity
                                      </a>
                  </li>
                  <li>
                    <a data-toggle="tab" href="#profile">
                                          <i class="icon-user"></i>
                                          Profile
                                      </a>
                  </li>
                  <li class="">
                    <a data-toggle="tab" href="#edit-profile">
                                          <i class="icon-envelope"></i>
                                          Edit Profile
                                      </a>
                  </li>
                    <li class="">
                    <a data-toggle="tab" href="#change-password">
                                          <i class="icon-envelope"></i>
                                          Change Password
                                      </a>
                  </li>
                </ul>
              </header>
              <div class="panel-body">
                <div class="tab-content">
                  <div id="recent-activity" class="tab-pane active">
                    <div class="profile-activity">
                      <div class="act-time">
                        <div class="activity-body act-in">
                          <span class="arrow"></span>
                          <div class="text">
                            <a href="#" class="activity-img"><img class="avatar" src="Bootstrap/img/chat-avatar.jpg" alt=""></a>
                            <p class="attribution"><a href="#">Jonatanh Doe</a> at 4:25pm, 30th Octmber 2014</p>
                            <p>It is a long established fact that a reader will be distracted layout</p>
                          </div>
                        </div>
                      </div>
                      <div class="act-time">
                        <div class="activity-body act-in">
                          <span class="arrow"></span>
                          <div class="text">
                            <a href="#" class="activity-img"><img class="avatar" src="Bootstrap/img/chat-avatar.jpg" alt=""></a>
                            <p class="attribution"><a href="#">Jhon Loves </a> at 5:25am, 30th Octmber 2014</p>
                            <p>Knowledge speaks, but wisdom listens.</p>
                          </div>
                        </div>
                      </div>
                      <div class="act-time">
                        <div class="activity-body act-in">
                          <span class="arrow"></span>
                          <div class="text">
                            <a href="#" class="activity-img"><img class="avatar" src="Bootstrap/img/chat-avatar.jpg" alt=""/></a>
                            <p class="attribution"><a href="#">Rose Crack</a> at 5:25am, 30th Octmber 2014</p>
                            <p>Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.</p>
                          </div>
                        </div>
                      </div>
                      
                    </div>
                  </div>
                  <!-- profile -->
                  <div id="profile" class="tab-pane">
                    <section class="panel">
                     
                      <div class="panel-body bio-graph-info">
                        <h1>Profile</h1>
                        <div class="row">
                          <div class="bio-row">
                            <p><span>First Name </span>: <asp:label runat="server" ID="lblfirstname" /> </p>
                          </div>
                          <div class="bio-row">
                            <p><span>Last Name </span>: <asp:label runat="server" ID="lbllastname" /></p>
                          </div>
                          <%--<div class="bio-row">
                            <p><span>Birthday</span>: 27 August 1987</p>
                          </div>
                          <div class="bio-row">
                            <p><span>Country </span>: United</p>
                          </div>--%>
                          <div class="bio-row">
                            <p><span>Duty </span>: <asp:label runat="server" ID="Lblduty" /></p>
                          </div>
                          <div class="bio-row">
                            <p><span>Email </span>:<asp:label runat="server" ID="Lblemail" /></p>
                          </div>
                          <%--<div class="bio-row">
                            <p><span>Mobile </span>: (+6283) 456 789</p>
                          </div>--%>
                          <div class="bio-row">
                            <p><span>Phone </span>: <asp:label runat="server" ID="Lblphone" Text="234" /></p>
                          </div>
                        </div>
                      </div>
                    </section>
                    <section>
                      <div class="row">
                      </div>
                    </section>
                  </div>
                  <!-- edit-profile -->
                  <div id="edit-profile" class="tab-pane">
                    <section runat="server" id="profile" class="panel">
                      <div class="panel-body bio-graph-info">
                        <h1> Profile Info</h1>
                        <%--<form class="form-horizontal" role="form">--%>
                          <div class="form-horizontal">
                          <div runat="server" id="userprofile" class="form-group">
                            <label class="col-lg-2 control-label">First Name</label>
                            <div class="col-lg-6">
                                <asp:TextBox ID="fname" CssClass="form-control" placeholder=" " runat="server" ></asp:TextBox>
                            </div>
                          </div>
                          <div class="form-group">
                            <label class="col-lg-2 control-label">Last Name</label>
                            <div class="col-lg-6">
                               <asp:TextBox ID="lname" CssClass="form-control" placeholder=" " runat="server" ></asp:TextBox><br />
                             </div>
                          </div>
                             <div class="form-group">
                            <label class="col-lg-2 control-label">Duty</label>
                            <div class="col-lg-6">
                              <asp:TextBox ID="duty" CssClass="form-control" placeholder=" " runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                          </div>
                           <div class="form-group">
                            <label class="col-lg-2 control-label">Email</label>
                            <div class="col-lg-6">
                             <asp:TextBox ID="email" CssClass="form-control" placeholder=" " Enabled="false" runat="server" TextMode="Email"></asp:TextBox>
                            </div>
                          </div>
                          <div class="form-group">
                            <label class="col-lg-2 control-label">Mobile</label>
                            <div class="col-lg-6">
                           <asp:TextBox ID="mobile" CssClass="form-control" placeholder=" " runat="server" TextMode="Phone"></asp:TextBox>
                            </div>
                          </div>
                         <div class="form-group">
                            <div class="col-lg-offset-2 col-lg-10">
                              <button type="submit" class="btn btn-primary" data-toggle="modal" data-target="#popup">Save</button>
                               <br />
                                <div class="modal fade" id="popup" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenter" aria-hidden="true">
                                  <div class="modal-dialog" role="document">
                                      <div class="modal-content">
                                          <div class="modal-header">
                                              <h5 class="modal-title" id="exampleModalLongTitle">Save Changes</h5>
                                              <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                  <span aria-hidden="true">&times;</span>
                                              </button>
                                          </div>
                                          <div class="modal-body">
                                              Are you sure you want to make this changes
                                          </div>
                                          <div class="modal-footer">
                                             <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                              <asp:button runat="server" ID="btnsave" type="button" class="btn btn-primary" Text="Save Changes" OnClick="btnsave_Click"></asp:button>
                                          </div>
                                      </div>
                                  </div>
                              </div>
                            </div>
                                                    
                          </div>
                  </div>
                             
                       <%-- </form>--%>
                      </div>
                    </section>
                  </div>
                    <div id="change-password" class="tab-pane">
                    <section class="panel">
                      <div class="panel-body bio-graph-info">
                        <h1> Change Password</h1>
                        <%--<form class="form-horizontal" role="form">--%>
                          <div class="form-horizontal">
                         
                              <asp:RequiredFieldValidator ValidationGroup="password" ID="RequiredFieldValidatorcurrentpassword" Display="Dynamic" runat="server" ErrorMessage="Enter Current Password" ControlToValidate="txtcurrentpassword" ForeColor="Red"></asp:RequiredFieldValidator>
                          <div class="form-group">
                            <div class="col-lg-6">
                              <asp:TextBox ID="txtcurrentpassword" TextMode="Password" CssClass="form-control" placeholder="Current Password" runat="server" ReadOnly="false"></asp:TextBox>
                            </div>
                          </div>
                            <asp:RequiredFieldValidator ValidationGroup="password" ID="RequiredFieldValidatornewpassword" Display="Dynamic" runat="server" ErrorMessage="Enter New Password" ControlToValidate="txtnewpassword" ForeColor="Red"></asp:RequiredFieldValidator>
                           <div class="form-group">
                            <div class="col-lg-6">
                             <asp:TextBox ValidationGroup="password" ID="txtnewpassword" CssClass="form-control" placeholder="New Password" Enabled="true" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                          </div>
                              <asp:CompareValidator ValidationGroup="password" ID="CompareValidatorcomfirmnewpassword" runat="server" ErrorMessage="Password are not the same" ForeColor="Red" ControlToCompare="txtnewpassword" ControlToValidate="comfirmnewpassword" Display="Dynamic"></asp:CompareValidator>
                              <asp:RequiredFieldValidator ValidationGroup="password" ID="RequiredFieldValidatorcomfirmnewpassword" Display="Dynamic" runat="server" ErrorMessage="Confirm New Password" ControlToValidate="comfirmnewpassword" ForeColor="Red"></asp:RequiredFieldValidator>
                            <div class="form-group">
                            <div class="col-lg-6">
                             <asp:TextBox ID="comfirmnewpassword" CssClass="form-control" placeholder="Comfirm Password" Enabled="true" runat="server" TextMode="Password"></asp:TextBox>
                            </div>
                          </div>
                            
                         <div class="form-group">
                            <div class="col-lg-offset-2 col-lg-10">
                                
                              <asp:Button ID="changepassword" ValidationGroup="password" runat="server" CssClass="btn btn-primary" Text="Change Password" OnClick="changepassword_Click"></asp:Button>
                                <br />
                                
                               <!-- <asp:Label runat="server" Font-Bold="true" ID="Lblchangepassword"></asp:Label>    -->
                            </div>
                          
                          </div>
                    </div>
                             
                       <%-- </form>--%>
                      </div>
                    </section>
                  </div>
                </div>
              </div>
            </section>
          </div>
        </div>

        <!-- page end-->
      </section>
    </section>
    <!--main content end-->
    <div class="text-right">
      <div class="credits">
          <!--
            All the links in the footer should remain intact.
            You can delete the links only if you purchased the pro version.
            Licensing information: https://bootstrapmade.com/license/
            Purchase the pro version form: https://bootstrapmade.com/buy/?theme=NiceAdmin
          -->
          
        </div>
    </div>
  </section>
  <script src="Bootstrap/js/jquery.js"></script>
  <script src="Bootstrap/js/jquery-ui-1.10.4.min.js"></script>
  <script src="Bootstrap/js/jquery-1.8.3.min.js"></script>
  <script type="text/javascript" src="Bootstrap/js/jquery-ui-1.9.2.custom.min.js"></script>
  <!-- bootstrap -->
  <script src="Bootstrap/js/bootstrap.min.js"></script>
  <!-- nice scroll -->
  <script src="Bootstrap/js/jquery.scrollTo.min.js"></script>
  <script src="Bootstrap/js/jquery.nicescroll.js" type="text/javascript"></script>
  <!-- charts scripts -->
  <script src="Bootstrap/assets/jquery-knob/js/jquery.knob.js"></script>
  <script src="Bootstrap/js/jquery.sparkline.js" type="text/javascript"></script>
  <script src="Bootstrap/assets/jquery-easy-pie-chart/jquery.easy-pie-chart.js"></script>
  <script src="Bootstrap/js/owl.carousel.js"></script>
  <!-- jQuery full calendar -->
  <<script src="Bootstrap/js/fullcalendar.min.js"></script>
    <!-- Full Google Calendar - Calendar -->
    <script src="Bootstrap/assets/fullcalendar/fullcalendar/fullcalendar.js"></script>
    <!--script for this page only-->
    <script src="Bootstrap/js/calendar-custom.js"></script>
    <script src="Bootstrap/js/jquery.rateit.min.js"></script>
    <!-- custom select -->
    <script src="Bootstrap/js/jquery.customSelect.min.js"></script>
    <script src="Bootstrap/assets/chart-master/Chart.js"></script>

    <!--custome script for all page-->
    <script src="Bootstrap/js/scripts.js"></script>
    <!-- custom script for this page-->
    <script src="Bootstrap/js/sparkline-chart.js"></script>
    <script src="Bootstrap/js/easy-pie-chart.js"></script>
    <script src="Bootstrap/js/jquery-jvectormap-1.2.2.min.js"></script>
    <script src="Bootstrap/js/jquery-jvectormap-world-mill-en.js"></script>
    <script src="Bootstrap/js/xcharts.min.js"></script>
    <script src="Bootstrap/js/jquery.autosize.min.js"></script>
    <script src="Bootstrap/js/jquery.placeholder.min.js"></script>
    <script src="Bootstrap/js/gdp-data.js"></script>
    <script src="Bootstrap/js/morris.min.js"></script>
    <script src="Bootstrap/js/sparklines.js"></script>
    <script src="Bootstrap/js/charts.js"></script>
    <script src="Bootstrap/js/jquery.slimscroll.min.js"></script>
    <script>
      //knob
      $(function() {
        $(".knob").knob({
          'draw': function() {
            $(this.i).val(this.cv + '%')
          }
        })
      });

      //carousel
      $(document).ready(function() {
        $("#owl-slider").owlCarousel({
          navigation: true,
          slideSpeed: 300,
          paginationSpeed: 400,
          singleItem: true

        });
      });

      //custom select box

      $(function() {
        $('select.styled').customSelect();
      });

      /* ---------- Map ---------- */
      $(function() {
        $('#map').vectorMap({
          map: 'world_mill_en',
          series: {
            regions: [{
              values: gdpData,
              scale: ['#000', '#000'],
              normalizeFunction: 'polynomial'
            }]
          },
          backgroundColor: '#eef3f7',
          onLabelShow: function(e, el, code) {
            el.html(el.html() + ' (GDP - ' + gdpData[code] + ')');
          }
        });
      });
    </script>
       </form>
    
</body>
</html>
