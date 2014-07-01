<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignersForm.aspx.cs" Inherits="WebForms_Designers_DesignersForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="pageTitle">The Designers</title>
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
        <asp:Label ID="labelTitle" runat="server" Text="The Designers" />
        <asp:ListBox 
            ID="listBoxItems" 
            runat="server" 
            SelectionMode="Multiple"
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
        <asp:Button CssClass="Button" ID="buttonHistory" runat="server" Text="Check History" OnClick="buttonHistory_Click"
            style="position: absolute; left: 730px; bottom: 40px;"/>
        
        <asp:Label ID="labelHistory" runat="server" 
            style="position: absolute; bottom: 400px; font-size: 10pt"></asp:Label>
        <asp:Label ID="errorText" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
