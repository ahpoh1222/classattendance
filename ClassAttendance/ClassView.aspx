﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassView.aspx.cs" Inherits="ClassAttendance.S_Connected" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>

        function btn_click(ClassID, Code, Room) {
            $('#divAll').show();
            $('#divResult').hide();
            $('#divLoading').show();
            $('#lblCode').html('Code : ' + Code);
            $('#lblRoom').html('Room : ' + Room);
            var Param = { 'id': ClassID };
            $.ajax({
                type: 'POST',
                url: 'ClassView.aspx/GetList',
                data: JSON.stringify(Param),
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (msg) {
                    //alert(msg.d);
                    var str = msg.d.split("|");
                    $('#lblStudent').html(str[0]);
                    $('#tblDetails').html(str[1]);
                    $('#divResult').show();
                    $('#divLoading').hide();
                },
                error: function (xhr, status, error) {
                    alert(error);
                    $('#divResult').show();
                    $('#divLoading').hide();
                }
            });
        }

    </script>

    <div class="container" style="max-width: none;">
        <div class="row">
            <button class="btn btn-danger" style="position:fixed; left:10px; top:10px;" runat="server" id="btnLogOut" onserverclick="btnLogOut_ServerClick">Log Out</button>
            <div class="col-sm-5" style="height: 80vh; border: 5px solid #0094ff; border-radius: 0.5rem; overflow: auto; padding-bottom: 15px; padding-top: 15px;">
                <b style="font-size: 2.5rem;"><u>Class List</u></b>
                <br />
                <br />
                <table class="table" style="text-align: center;">
                    <thead>
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
            <div class="col-sm-6" style="height: 80vh; border: 5px solid #0094ff; border-radius: 0.5rem; overflow: auto;">

                <div id="divAll" style="display:none;">
                    <div class="row" style="margin-top: 10px;">
                        <div class="col-sm-12" style="font-size: 2rem;">
                            <label id="lblCode"></label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12" style="font-size: 2rem;">
                            <label id="lblRoom"></label>
                        </div>
                    </div>
                    <div class="row" id="divResult" style="display: block;">
                        <div class="col-sm-12" style="font-size: 2rem;">
                            <label id="lblStudent"></label>
                        </div>
                        <br />
                        <table class="table" style="text-align: center;">
                            <thead>
                                <tr>
                                    <th style="text-align: center;">Student</th>
                                    <th style="text-align: center;">Device</th>
                                </tr>
                            </thead>  
                            <tbody id="tblDetails">

                            </tbody>
                        </table>
                    </div>
                    <div class="row" id="divLoading" style="display: none;">
                        <center>
                <img src="Content/image/ounq1mw5kdxy.gif" height="400" width="400" />
                </center>
                    </div>
                </div>



            </div>

        </div>
    </div>

</asp:Content>
