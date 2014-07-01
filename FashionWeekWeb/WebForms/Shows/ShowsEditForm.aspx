﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowsEditForm.aspx.cs" Inherits="WebForms_Shows_ShowsEditForm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Show</title>
    <link rel="stylesheet" type="text/css" href="../SharedStyles.css" />
    <link rel="stylesheet" type="text/css" href="ShowsAddForm.css" />
</head>
<body>
    <div class="Background">
        <img src="../../Resources/bg.jpg" alt="" />
    </div>
    <form id="showsEditForm" runat="server">
    <asp:Label ID="labelPageTitle" runat="server" Text="Edit Show" />
    <div class="SmallFormDiv">
        <asp:Label ID="labelTitle" CssClass="LeftSide" runat="server" Text="Title:"></asp:Label>
        <asp:TextBox ID="textBoxTitle" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelDateTime" CssClass="LeftSide" runat="server" Text="Date & Time:"></asp:Label>
        <eo:DatePicker ID="datePicker" runat="server"  CssClass="RightSide" PickerFormat="dd/MM/yyyy hh:mm tt" TitleRightArrowImageUrl="00040206" TitleLeftArrowImageUrl="00040204"
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
        <asp:Label ID="labelCity" CssClass="LeftSide" runat="server" Text="City:"></asp:Label>
        <asp:TextBox ID="textBoxCity" CssClass="RightSide" runat="server"></asp:TextBox>
        <asp:Label ID="labelDesigners" CssClass="LeftSide" runat="server" Text="Designers:"></asp:Label>
        <asp:CheckBoxList ID="boxDesigners" CssClass="LeftSide" runat="server"></asp:CheckBoxList>
        <asp:Label ID="labelModels" CssClass="RightSide" runat="server" Text="Models:"></asp:Label>
        <asp:CheckBoxList ID="boxModels" CssClass="RightSide" runat="server"></asp:CheckBoxList>
    </div>

        <asp:Button ID="buttonOK" runat="server" CssClass="Button" Text="OK" OnClick="buttonOK_Click"/>
        <asp:Button ID="buttonCancel" runat="server" CssClass="Button" Text="Cancel" OnClick="buttonCancel_Click"/>

        <asp:Label ID="labelError" runat="server" ></asp:Label>
    </form>
</body>
</html>
