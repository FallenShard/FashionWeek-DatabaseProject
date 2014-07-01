<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModelsDetailsForm.aspx.cs" Inherits="WebForms_Models_ModelsDetailsForm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Model</title>
    <link rel="stylesheet" type="text/css" href="../SharedStyles.css" />
    <link rel="stylesheet" type="text/css" href="ModelsDetailsForm.css" />
</head>
<body>
    <div class="Background">
        <img src="../../Resources/bg.jpg" alt="" />
    </div>
    <form id="modelsDetailsForm" runat="server">
    <asp:Label ID="labelPageTitle" runat="server" Text="Edit Model" />
    <div class="SmallFormDiv">
        <asp:Label ID="labelPN" CssClass="LeftSide" runat="server" Text="Personal Number:"></asp:Label>
        <asp:Label ID="labelPNVal" CssClass="RightSide" runat="server" ></asp:Label>
        <asp:Label ID="labelFirstName" CssClass="LeftSide" runat="server" Text="First Name:"></asp:Label>
        <asp:Label ID="labelFirstNameVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelLastName" CssClass="LeftSide" runat="server" Text="Last Name:"></asp:Label>
        <asp:Label ID="labelLastNameVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelAgency" CssClass="LeftSide" runat="server" Text="Modeling Agency:"></asp:Label>
        <asp:Label ID="labelAgencyVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelDate" CssClass="LeftSide" runat="server" Text="Birth Date:"></asp:Label>
        <asp:Label ID="labelDateVal" CssClass="RightSide" runat="server" ></asp:Label>
        <asp:Label ID="labelGender" CssClass="LeftSide" runat="server" Text="Gender:"></asp:Label>
        <asp:Label ID="labelGenderVal" CssClass="RightSide" runat="server" Text="Gender:"></asp:Label>
        <asp:Label ID="labelEyes" CssClass="LeftSide" runat="server" Text="Eye Color:"></asp:Label>
        <asp:Label ID="labelEyesVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelHair" CssClass="LeftSide" runat="server" Text="Hair Color:"></asp:Label>
        <asp:Label ID="labelHairVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelWeight" CssClass="LeftSide" runat="server" Text="Weight:"></asp:Label>
        <asp:Label ID="labelWeightVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelHeight" CssClass="LeftSide" runat="server" Text="Height:"></asp:Label>
        <asp:Label ID="labelHeightVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelCSize" CssClass="LeftSide" runat="server" Text="Clothing Size:"></asp:Label>
        <asp:Label ID="labelCSizeVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelSG" CssClass="LeftSide" runat="server" Text="Special Guest Status:"></asp:Label>
        <asp:Label ID="labelSGVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelOccupation" CssClass="LeftSide" runat="server" Text="Occupation:"></asp:Label>
        <asp:Label ID="labelOccupationVal" CssClass="RightSide" runat="server"></asp:Label>
        <asp:Label ID="labelFashionShows" CssClass="RightSide" runat="server" Text="Fashion Shows:"></asp:Label>
        <asp:CheckBoxList ID="boxShows" CssClass="RightSide" Enabled="false" runat="server"></asp:CheckBoxList>
        <asp:Label ID="labelMagazines" CssClass="LeftSide" runat="server" Text="Magazine Covers:"></asp:Label>
        <asp:CheckBoxList ID="boxMagazines" CssClass="LeftSide" Enabled="false" runat="server"></asp:CheckBoxList>
    </div>
        <asp:Button ID="buttonBack" runat="server" CssClass="Button" Text="Back" OnClick="buttonBack_Click"/>
    </form>
</body>
</html>
