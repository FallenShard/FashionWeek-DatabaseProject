<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DesignersAddForm.aspx.cs" Inherits="WebForms_Designers_DesignersAddForm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Register New Designer</title>
    <link rel="stylesheet" type="text/css" href="../SharedStyles.css" />
    <link rel="stylesheet" type="text/css" href="DesignersAddForm.css" />
</head>
<body>
    <div class="Background">
        <img src="../../Resources/bg.jpg" alt="" />
    </div>
    <form id="designersAddForm" runat="server">
    <asp:Label ID="labelPageTitle" runat="server" Text="Register New Designer" />
    <div class="SmallFormDiv">
        <asp:Label ID="labelPN" CssClass="LeftSide" runat="server" Text="Personal Number:"></asp:Label>
        <asp:TextBox ID="textBoxPN" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelFirstName" CssClass="LeftSide" runat="server" Text="First Name:"></asp:Label>
        <asp:TextBox ID="textBoxFirstName" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelLastName" CssClass="LeftSide" runat="server" Text="Last Name:"></asp:Label>
        <asp:TextBox ID="textBoxLastName" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelDate" CssClass="LeftSide" runat="server" Text="Birth Date:"></asp:Label>
        <eo:DatePicker ID="datePicker" runat="server" CssClass="RightSide" PickerFormat="dd/MM/yyyy" TitleRightArrowImageUrl="00040206" TitleLeftArrowImageUrl="00040204"
                DayCellWidth="24" DayCellHeight="16" TitleLeftArrowHoverImageUrl="00040205"
                DisableWeekendDays="True" TitleRightArrowHoverImageUrl="00040207" PopupImageUrl="00040201"
                PopupHoverImageUrl="00040203" PopupDownImageUrl="00040202"  Width="170px">
                <SelectedDayStyle CssText="background-image: url(00040208)"></SelectedDayStyle>
                <DisabledDayStyle CssText="color:Gray;"></DisabledDayStyle>
                <TitleStyle CssText="padding-right: 3px; padding-left: 3px; font-weight: bold; font-size: 11pt; padding-bottom: 3px; color: black; padding-top: 3px; font-family: Arial; background-color: transparent"></TitleStyle>
                <CalendarStyle CssText="background-color:#EBE9ED;border-bottom-color:black;border-bottom-style:solid;border-bottom-width:1px;border-left-color:black;border-left-style:solid;border-left-width:1px;border-right-color:black;border-right-style:solid;border-right-width:1px;border-top-color:black;border-top-style:solid;border-top-width:1px;"></CalendarStyle>
                <DayHoverStyle CssText="background-image:url('00040208');"></DayHoverStyle>
                <MonthStyle CssText="font-size: 11pt; cursor: hand; font-family: Arial"></MonthStyle>
                <PickerStyle CssText="font-size: 11pt; font-family: Arial"></PickerStyle>
                <DayHeaderStyle CssText="font-weight: bold; font-size: 11pt; color: black; border-bottom: black 1px solid; font-family: Arial"></DayHeaderStyle>
        </eo:DatePicker>
        <asp:Label ID="labelGender" CssClass="LeftSide" runat="server" Text="Gender:"></asp:Label>
        <asp:RadioButton ID="radioButtonMale" Checked="true" GroupName="gender" CssClass="RightSide" runat="server" />
        <label id="labelRBM" for="radioButtonMale">Male</label>
        <asp:RadioButton ID="radioButtonFemale" GroupName="gender" CssClass="RightSide" runat="server" />
        <label id="labelRBF" for="radioButtonFemale">Female</label>
        <asp:Label ID="labelCountry" CssClass="LeftSide" runat="server" Text="Country:"></asp:Label>
        <asp:TextBox ID="textBoxCountry" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelFashionHouse" CssClass="LeftSide" runat="server" Text="Fashion House:"></asp:Label>
        <asp:TextBox ID="textBoxFashionHouse" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelFashionShows" CssClass="LeftSide" runat="server" Text="Fashion Shows:"></asp:Label>
        <asp:CheckBoxList ID="boxShows" CssClass="RightSide" runat="server"></asp:CheckBoxList>
    </div>

        <asp:Button ID="buttonOK" runat="server" CssClass="Button" Text="OK" OnClick="buttonOK_Click"/>
        <asp:Button ID="buttonCancel" runat="server" CssClass="Button" Text="Cancel" OnClick="buttonCancel_Click"/>

        <asp:Label ID="labelError" runat="server" ></asp:Label>
    </form>
</body>
</html>
