<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStudent.aspx.cs" Inherits="ClassAttendance.AddStudent" MasterPageFile="~/Site.Master" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        window.onunload = refreshParent;
        function refreshParent() {
            window.opener.location.reload();
        }
    </script>

    <div class="container">
        Student ID :
        <input runat="server" id="txtStudentID" type="text" class="form-control" placeholder="Student ID" />
        <br />
        Name :
        <input runat="server" id="txtName" type="text" class="form-control" placeholder="Name" />
        <br />
        Username :
        <input runat="server" id="txtUsername" type="text" class="form-control" placeholder="Username" />
        <br />
        Password :
        <input runat="server" id="txtPassword" type="password" class="form-control" placeholder="Password" />
        <br />  
        Class :
        <select class="form-control" runat="server" id="txtClassCode" style="width:280px;">
            
        </select>
        <br />
        <input runat="server" id="btnPerform" onserverclick="btnPerform_ServerClick" type="button" class="btn btn-success" value="Edit" />
        <asp:Button runat="server" ID="btnDeletee" OnClick="btnDelete_ServerClick" OnClientClick="return confirm('Do you really want to delete this account?');" Visible="false" class="btn btn-danger" text="Remove this student"  />
        <input type="button" class="btn" value="Cancel" onclick="window.close();" />
    </div>

</asp:Content>
