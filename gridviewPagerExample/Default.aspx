<%@ Page Title="首頁" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="gridviewPagerExample._Default" %>

<%@ Register src="UserControls/ucPager.ascx" tagname="ucPager" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>GridView分頁範例</title>
    <link href="Styles/base.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 5px; width: 70%;">
            <asp:GridView ID="GridView1" runat="server" CssClass="gridview-style">
            </asp:GridView>
            <uc1:ucPager ID="ucPager1" runat="server" OnPageClick="PageClick" />
        </div>
    </form>
</body>
</html>

