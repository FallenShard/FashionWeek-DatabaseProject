<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AgenciesDetailsForm.aspx.cs" Inherits="WebForms_Agencies_AgenciesDetailsForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>--</title>
    <link rel="stylesheet" type="text/css" href="../SharedStyles.css" />
    <link rel="stylesheet" type="text/css" href="AgenciesDetailsForm.css" />
</head>
<body>
    <div class="Background">
        <img src="../../Resources/bg.jpg" alt="" />
    </div>
    <form id="designersAddForm" runat="server">
    <asp:Label ID="labelPageTitle" runat="server" Text="--" />
    <div class="SmallFormDiv">
        <asp:Label ID="labelRegNum" CssClass="LeftSide" runat="server" Text="Registration Number:"></asp:Label>
        <asp:Label ID="labelRegNumVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelName" CssClass="LeftSide" runat="server" Text="Name:"></asp:Label>
        <asp:Label ID="labelNameVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelDate" CssClass="LeftSide" runat="server" Text="Birth Date:"></asp:Label>
        <asp:Label ID="labelDateVal" CssClass="RightSide" runat="server" ></asp:Label>
        <asp:Label ID="labelHQ" CssClass="LeftSide" runat="server" Text="Headquarters:"></asp:Label>
        <asp:Label ID="labelHQVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelInt" CssClass="LeftSide" runat="server" Text="Agency Type:"/>
        <asp:Label ID="labelIntVal" CssClass="RightSide" runat="server" />
        <asp:Label ID="labelModels" CssClass="RightSide" runat="server" Text="Models:"></asp:Label>
        <asp:CheckBoxList ID="boxModels" CssClass="RightSide" Enabled="false" runat="server"></asp:CheckBoxList>
        <asp:Label ID="labelCountries" CssClass="LeftSide" runat="server" Enabled="false" Text="Countries:"></asp:Label>
        <asp:CheckBoxList ID="boxCountries" CssClass="LeftSide" runat="server" Enabled="false" ></asp:CheckBoxList>
    </div>
        <asp:Button ID="buttonBack" runat="server" CssClass="Button" Text="Back" OnClick="buttonBack_Click"/>
    </form>
</body>
</html>
