<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowsDetailsForm.aspx.cs" Inherits="WebForms_Shows_ShowsDetailsForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Register New Show</title>
    <link rel="stylesheet" type="text/css" href="../SharedStyles.css" />
    <link rel="stylesheet" type="text/css" href="ShowsDetailsForm.css" />
</head>
<body>
    <div class="Background">
        <img src="../../Resources/bg.jpg" alt="" />
    </div>
    <form id="showsDetailsForm" runat="server">
    <asp:Label ID="labelPageTitle" runat="server" Text="" />
    <div class="SmallFormDiv">
        <asp:Label ID="labelTitle" CssClass="LeftSide" runat="server" Text="Title:"></asp:Label>
        <asp:Label ID="labelTitleVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelDateTime" CssClass="LeftSide" runat="server" Text="Date & Time:"></asp:Label>
        <asp:Label ID="labelDateTimeVal" CssClass="RightSide" runat="server" ></asp:Label>
        <asp:Label ID="labelCity" CssClass="LeftSide" runat="server" Text="City:"></asp:Label>
        <asp:Label ID="labelCityVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelDesigners" CssClass="LeftSide" runat="server" Text="Designers:"></asp:Label>
        <asp:CheckBoxList ID="boxDesigners" CssClass="LeftSide" Enabled="false" runat="server"></asp:CheckBoxList>
        <asp:Label ID="labelModels" CssClass="RightSide" runat="server" Text="Models:"></asp:Label>
        <asp:CheckBoxList ID="boxModels" CssClass="RightSide" Enabled="false" runat="server"></asp:CheckBoxList>
    </div>
        <asp:Button ID="buttonBack" runat="server" CssClass="Button" Text="Back" OnClick="buttonBack_Click"/>
    </form>
</body>
</html>
