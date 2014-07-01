<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModelsAddForm.aspx.cs" Inherits="WebForms_Models_ModelsAddForm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Register New Model</title>
    <link rel="stylesheet" type="text/css" href="../SharedStyles.css" />
    <link rel="stylesheet" type="text/css" href="ModelsAddForm.css" />
</head>
<body>
    <div class="Background">
        <img src="../../Resources/bg.jpg" alt="" />
    </div>
    <form id="modelsEditForm" runat="server">
    <asp:Label ID="labelPageTitle" runat="server" Text="Register New Model" />
    <div class="SmallFormDiv">
        <asp:Label ID="labelPN" CssClass="LeftSide" runat="server" Text="Personal Number:"></asp:Label>
        <asp:TextBox ID="textBoxPN" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelFirstName" CssClass="LeftSide" runat="server" Text="First Name:"></asp:Label>
        <asp:TextBox ID="textBoxFirstName" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelLastName" CssClass="LeftSide" runat="server" Text="Last Name:"></asp:Label>
        <asp:TextBox ID="textBoxLastName" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelAgency" CssClass="LeftSide" runat="server" Text="Modeling Agency:"></asp:Label>
        <asp:DropDownList ID="dropDownListAgency" CssClass="RightSide" runat="server"></asp:DropDownList>
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
        <asp:Label ID="labelEyes" CssClass="LeftSide" runat="server" Text="Eye Color:"></asp:Label>
        <asp:DropDownList ID="dropDownListEyes" CssClass="RightSide" runat="server"></asp:DropDownList>
        <asp:Label ID="labelHair" CssClass="LeftSide" runat="server" Text="Hair Color:"></asp:Label>
        <asp:DropDownList ID="dropDownListHair" CssClass="RightSide" runat="server"></asp:DropDownList>
        <asp:Label ID="labelWeight" CssClass="LeftSide" runat="server" Text="Weight:"></asp:Label>
        <asp:TextBox ID="textBoxWeight" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelHeight" CssClass="LeftSide" runat="server" Text="Height:"></asp:Label>
        <asp:TextBox ID="textBoxHeight" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelCSize" CssClass="LeftSide" runat="server" Text="Clothing Size:"></asp:Label>
        <asp:TextBox ID="textBoxCSize" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:CheckBox ID="checkBoxSG" runat="server" AutoPostBack="true" OnCheckedChanged="checkBoxSG_CheckedChanged"/>
        <label id="labelSG" for="checkBoxSG">Special Guest</label>
        <asp:Label ID="labelOccupation" CssClass="LeftSide" runat="server" Text="Occupation:"></asp:Label>
        <asp:TextBox ID="textBoxOccupation" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelFashionShows" CssClass="RightSide" runat="server" Text="Fashion Shows:"></asp:Label>
        <asp:CheckBoxList ID="boxShows" CssClass="RightSide" runat="server"></asp:CheckBoxList>
        <asp:Label ID="labelMagazines" CssClass="LeftSide" runat="server" Text="Magazine Covers:"></asp:Label>
        <asp:ListBox ID="boxMagazines" CssClass="LeftSide" runat="server" ></asp:ListBox>
        <asp:TextBox ID="textBoxMagazines" CssClass="LeftSide" runat="server"></asp:TextBox>
        <asp:Button ID="buttonAdd" runat="server" Text="Add" OnClick="buttonAdd_Click"/>
        <asp:Button ID="buttonRemove" CssClass="LeftSide" runat="server" Text="Remove" OnClick="buttonRemove_Click"/>
    </div>

        <asp:Button ID="buttonOK" runat="server" CssClass="Button" Text="OK" OnClick="buttonOK_Click"/>
        <asp:Button ID="buttonCancel" runat="server" CssClass="Button" Text="Cancel" OnClick="buttonCancel_Click"/>

        <asp:Label ID="labelError" runat="server" ></asp:Label>
    </form>
</body>
</html>
