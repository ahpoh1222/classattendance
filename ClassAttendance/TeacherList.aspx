﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherList.aspx.cs" Inherits="ClassAttendance.TeacherList"  MasterPageFile="~/Site.Master"%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>
        function SearchName() {
            // Declare variables 
            var input, filter, table, tr, td, i, txtValue;
            input = document.getElementById("txtSearch");
            filter = input.value.toUpperCase();
            table = document.getElementById("myTable");
            tr = table.getElementsByTagName("tr");

            // Loop through all table rows, and hide those who don't match the search query
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[0];
                if (td) {
                    txtValue = td.textContent || td.innerText;
                    if (txtValue.toUpperCase().indexOf(filter) > -1) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }

        function lol(str) {
            window.open(str,'targetWindow','toolbar=no,location=no,status=no,menubar=no,scrollbars=yes,resizable=yes,width=600,height=600');
        }
    </script>

     <div class="container">
         <button class="btn" style="position:fixed; left:10px; top:10px;" onclick="history.go(-1); return false;">Back</button>
        <div class="col-sm-12" style="height: 80vh; border: 5px solid #0094ff; border-radius: 0.5rem; overflow: auto; padding-bottom: 15px; padding-top: 15px;">

            <b style="font-size: 3rem;"><u>Teacher List</u></b><a class="btn btn-success" style="float: right;" onclick="lol('AddTeacher.aspx?action=Add&id=0'); return false;">Add New Teacher</a>
            <br />
            <br />
            <input type="text" class="form-control" id="txtSearch" onkeyup="SearchName()" placeholder="Search for names / id">
            <table class="table" style="text-align: center;" id="myTable">
                <thead class="thead-dark">
                    <tr>
                        <th style="text-align: center;">Name</th>
                        <th style="text-align: center;">Username</th>
                        <th style="text-align: center;"></th>
                    </tr>
                </thead>
                <asp:PlaceHolder runat="server" ID="tblTeacher"></asp:PlaceHolder>
            </table>

        </div>
    </div>

    </asp:Content>