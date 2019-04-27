<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="ClassAttendance.AdminPage" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function lol(str) {
            window.open(str,'targetWindow','toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=600,height=300');
        }
        
    </script>

    <div class="container" style="max-width: none;">
        <div class="row">
            <div class="col-sm-6" style="height: 80vh; border: 5px solid #0094ff; border-radius: 0.5rem; overflow:auto; padding-bottom:15px; padding-top:15px;">
                   <b style="font-size:3rem;"><u>Class List</u></b><a class="btn btn-success" style="float:right;" onclick="lol('AddClass.aspx?action=Add&id=0'); return false;">Add New Class</a>
                    <br />
                <br />
                    <table class="table" style="text-align: center;">
                        <thead class="thead-dark">
                            <tr>
                                <th style="text-align: center;">Code</th>
                                <th style="text-align: center;">Room</th>
                                <th style="text-align: center;">No. Of Student</th>
                                <th style="text-align: center;"></th>
                            </tr>
                        </thead>
                        <asp:PlaceHolder runat="server" ID="tblClass"></asp:PlaceHolder>
                    </table>
            </div>
            <div class="col-sm-1"></div>
            <div class="col-sm-5" style="height: 80vh;  border: 5px solid #0094ff; border-radius: 0.5rem;">
                <table style="height:100%; width:100%;">
                    <tr>
                        <td style="text-align:center;"><b style="font-size:2.5rem;">Student List</b><br /><label runat="server" id="lblStudent">0 Students</label><br /><a class="btn btn-success">View</a></td>
                    </tr>
                    <tr>
                        <td style="text-align:center; border-top:5px solid #0094ff;"><b style="font-size:2.5rem;">Teacher List</b><br /><label runat="server" id="lblTeacher">0 Teacher</label><br /><a class="btn btn-success">View</a></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>


</asp:Content>
