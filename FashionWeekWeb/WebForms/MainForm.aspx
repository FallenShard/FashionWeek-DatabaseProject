<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="MainForm.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fashion Week</title>
    <link rel="stylesheet" type="text/css" href="SharedStyles.css" /> 
    <link rel="stylesheet" type="text/css" href="MainForm.css" /> 
</head>
<body>
    <div class="Background">
        <img src="../Resources/bg.jpg" alt="" />
    </div>
    <form id="mainForm" runat="server">
        <asp:Label ID="labelTitle" runat="server" Text="Fashion Week" />
        <asp:Label ID="labelSubTitle" runat="server" Text="Redefining what it means to be fashionable" />
        <div id="shade"></div>
        <asp:Button CssClass="Button" ID="buttonOverview" runat="server" Text="Overview" OnClick="buttonOverview_Click" />
        <asp:Button CssClass="Button" ID="buttonShows" runat="server" Text="The Shows" OnClick="buttonShows_Click"/>
        <asp:Button CssClass="Button" ID="buttonDesigners" runat="server" Text="The Designers" OnClick="buttonDesigners_Click"/>
        <asp:Button CssClass="Button" ID="buttonModels" runat="server" Text="The Models" OnClick="buttonModels_Click"/>
        <asp:Button CssClass="Button" ID="buttonAgencies" runat="server" Text="The Agencies" OnClick="buttonAgencies_Click"/>
        <asp:Image ID="devLogo" ImageUrl="~/Resources/logo.png" runat="server" />
    </form>
</body>
</html>
