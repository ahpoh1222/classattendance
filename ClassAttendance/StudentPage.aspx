<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentPage.aspx.cs" Inherits="ClassAttendance.StudentPage" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <button class="btn btn-danger" style="position:fixed; left:10px; top:10px;" runat="server" id="btnLogOut" onserverclick="btnLogOut_ServerClick">Log Out</button>
        <div class="col-sm-12" style="height: 90vh;">
            <table style="height: 100%; width: 100%;">
                <tr>
                    <td style="text-align: center;">
                        <h1 runat="server" id="lblName" style="color: darkcyan;">Student Name</h1>
                        <center><label runat="server" id="lblCurrentMac" type="text" class="form-control" style="font-size:2.5rem; max-height:none; max-width:none; width:50%; height:5rem;">00:0a:95:9d:68:16</label></center>
                        <br />
                        <button runat="server" id="btnSubmit" onserverclick="btnSubmit_ServerClick" class="btn btn-primary" style="font-size: 2rem;">Register Current Mac Address</button>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center;">
                        <div class="col-sm-3"></div>
                        <div class="col-sm-6">
                            <h1 style="text-align:left;">Registered Devices</h1>
                        <div class="col-sm-12" style="border: 3px solid #0094ff; border-radius: 0.5rem;">
                            <table style="height: 100%; width: 100%;">
                                <tr>
                                    <td style="text-align: center;">
                                        <h4><b><label runat="server" id="lblmac1"></label></b></h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-top: 3px solid #0094ff;"">
                                       <h4><b><label runat="server" id="lblmac2"></label></b></h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: center; border-top: 3px solid #0094ff;"">
                                        <h4><b><label runat="server" id="lblmac3"></label></b></h4>
                                    </td>
                                </tr>
                            </table>
                        </div>
                            </div>
                        <div class="col-sm-3"></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
