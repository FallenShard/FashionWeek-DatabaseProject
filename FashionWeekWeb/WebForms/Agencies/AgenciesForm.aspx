<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgenciesForm.aspx.cs" Inherits="WebForms_Agencies_AgenciesForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="pageTitle">The Agencies</title>
    <link rel="stylesheet" type="text/css" href="../SharedStyles.css" /> 
    <link rel="stylesheet" type="text/css" href="../SharedViewPage.css" />
    <link rel="stylesheet" type="text/css" href="../Table.css" /> 
</head>
<body>
    <div class="Background">
        <img src="../../Resources/bg.jpg" alt="" />
    </div>
    <form id="designersForm" runat="server">
    <div>
        <asp:Label ID="labelTitle" runat="server" Text="The Agencies" />
        <asp:ListBox 
            ID="listBoxItems" 
            runat="server" 
            AutoPostBack="true">
        </asp:ListBox>

        <div id="tableWrapper" class="TableWrapper">
            <div id="tableScroll" class="TableScroll">
                <asp:Table ID="tableOverview" runat="server" ViewStateMode="Enabled"></asp:Table>
            </div>
        </div>

        <asp:Button CssClass="Button" ID="buttonAddNew" runat="server" Text="Add New" OnClick="buttonAddNew_Click"/>
        <asp:Button CssClass="Button" ID="buttonEdit" runat="server" Text="Edit" OnClick="buttonEdit_Click"/>
        <asp:Button CssClass="Button" ID="buttonDelete" runat="server" Text="Delete" OnClick="buttonDelete_Click"/>
        <asp:Button CssClass="Button" ID="buttonDetails" runat="server" Text="Details" OnClick="buttonDetails_Click"/>
        <asp:Button CssClass="Button" ID="buttonBack" runat="server" Text="Back" OnClick="buttonBack_Click"/>

        <asp:Label ID="errorText" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
