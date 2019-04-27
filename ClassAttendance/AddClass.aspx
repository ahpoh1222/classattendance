<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClass.aspx.cs" Inherits="ClassAttendance.AddClass" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        window.onunload = refreshParent;
        function refreshParent() {
            window.opener.location.reload();
        }
    </script>

    <div class="container">
        Class :
        <input runat="server" id="txtClass" type="text" class="form-control" placeholder="Class" />
        <br />
        Room :
        <input runat="server" id="txtRoom" type="text" class="form-control" placeholder="Room" />
        <br />
        <input runat="server" id="btnPerform" onserverclick="btnPerform_ServerClick" type="button" class="btn btn-success" value="Edit" />
        <input type="button" class="btn" value="Cancel" onclick="window.close();" />
    </div>

</asp:Content>
