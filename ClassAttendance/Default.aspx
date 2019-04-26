<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ClassAttendance._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

   <style>
        body {
            background-image: url('Content/image/wp1905615.jpg');
            background-repeat: no-repeat;
            background-attachment: fixed;
        }
    </style>

    <div>
        <div class="container" style="margin-top: 50px; background-color: rgba(255, 255, 255, 0.3); height:50vh; width:50vw;">

            <div class="row">
                <center><img src="Content/image/bb99ae87c3b7daa39aa0d655324ef114.jpg"  height="100" width="100" style="margin-top:20px; margin-bottom:10px"/></center>
                <div class="col-sm-12" style="font-size: 3rem; text-align: center;">Class Attendance</div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12" style="text-align: center;">
                    <input type="text" class="form-control" placeholder="Username" style="margin: 0 auto; max-width: none; width: 50%; font-size: 2rem;" />
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-sm-12" style="text-align: center;">
                    <input type="password" class="form-control" placeholder="Password" style="margin: 0 auto; max-width: none; width: 50%; font-size: 2rem;" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <center><button runat="server" onserverclick="Login_Click" class="btn" style="color:white; background-color:black; font-size:1.5rem; margin:10px 0;">Login</button></center>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
