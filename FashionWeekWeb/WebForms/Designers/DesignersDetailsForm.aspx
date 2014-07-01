<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignersDetailsForm.aspx.cs" Inherits="WebForms_Designers_DesignersDetailsForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Register New Show</title>
    <link rel="stylesheet" type="text/css" href="../SharedStyles.css" />
    <link rel="stylesheet" type="text/css" href="DesignersDetailsForm.css" />
</head>
<body>
    <div class="Background">
        <img src="../../Resources/bg.jpg" alt="" />
    </div>
    <form id="showsDetailsForm" runat="server">
    <asp:Label ID="labelPageTitle" runat="server" Text="" />
    <div class="SmallFormDiv">
        <asp:Label ID="labelPN" CssClass="LeftSide" runat="server" Text="Personal Number:"></asp:Label>
        <asp:Label ID="labelPNVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelFirstName" CssClass="LeftSide" runat="server" Text="First Name:"></asp:Label>
        <asp:Label ID="labelFirstNameVal" CssClass="RightSide" runat="server" ></asp:Label>
        <asp:Label ID="labelLastName" CssClass="LeftSide" runat="server" Text="Last Name:"></asp:Label>
        <asp:Label ID="labelLastNameVal" CssClass="RightSide" runat="server" ></asp:Label>
        <asp:Label ID="labelDate" CssClass="LeftSide" runat="server" Text="Birth Date:"></asp:Label>
        <asp:Label ID="labelDateVal" CssClass="RightSide" runat="server" ></asp:Label>
        <asp:Label ID="labelGender" CssClass="LeftSide" runat="server" Text="Gender:"></asp:Label>
        <asp:Label ID="labelGenderVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelCountry" CssClass="LeftSide" runat="server" Text="Country:"></asp:Label>
        <asp:Label ID="labelCountryVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelFHouse" CssClass="LeftSide" runat="server" Text="Fashion House:"></asp:Label>
        <asp:Label ID="labelFHouseVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelFashionShows" CssClass="LeftSide" runat="server" Text="Fashion Shows:"></asp:Label>
        <asp:CheckBoxList ID="boxShows" CssClass="RightSide" Enabled="false" runat="server"></asp:CheckBoxList>
    </div>
        <asp:Button ID="buttonBack" runat="server" CssClass="Button" Text="Back" OnClick="buttonBack_Click"/>
    </form>
</body>
</html>
