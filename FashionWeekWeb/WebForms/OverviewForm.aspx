<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OverviewForm.aspx.cs" Inherits="OverviewForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fashion Week - Overview</title>
    <link rel="stylesheet" type="text/css" href="SharedStyles.css" /> 
    <link rel="stylesheet" type="text/css" href="Table.css" /> 
    <link rel="stylesheet" type="text/css" href="OverviewForm.css" /> 
</head>
<body>
    <div class="Background">
        <img src="../Resources/bg.jpg" alt="" />
    </div>

    <form id="overviewForm" runat="server">
        <asp:Label ID="labelDesc" runat="server" Text="Select a table to display:" />

        <asp:Label ID="labelTable" runat="server" Text="" />

        <asp:ListBox 
            ID="listBoxTables" 
            runat="server" 
            AutoPostBack="true"
            OnSelectedIndexChanged="listBoxTables_SelectedIndexChanged">
        </asp:ListBox>

        <div id="tableWrapper" class="TableWrapper">
            <div id="tableScroll" class="TableScroll">
                <asp:Table ID="tableOverview" runat="server" ></asp:Table>
            </div>
        </div>

        <asp:Button ID="buttonBack" CssClass="Button" runat="server" Text="Back" OnClick="buttonBack_Click" />

    </form>
</body>
</html>
