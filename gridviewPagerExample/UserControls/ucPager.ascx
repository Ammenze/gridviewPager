<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPager.ascx.cs" Inherits="gridviewPagerExample.UserControls.ucPager" %>
<div class="wuc_pager">
    <div runat="server" id="divPager">
        <span>
        <asp:LinkButton ID="LnkPrev" runat="server" OnClick="LnkNumber_Click" CssClass="wuc_pager_span"></asp:LinkButton>
        </span>
        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" OnItemDataBound="Item_Bound">
            <ItemTemplate>
                <asp:LinkButton ID="LnkNumber" runat="server" CssClass="wuc_pager_span" OnClick="LnkNumber_Click"></asp:LinkButton>
                <asp:Label ID="LblNumber" runat="server" CssClass="wuc_pager_span"></asp:Label>
            </ItemTemplate>
        </asp:DataList>
        <span>
        <asp:LinkButton ID="LnkNxt" runat="server" OnClick="LnkNumber_Click" CssClass="wuc_pager_span"></asp:LinkButton>
        </span>
    </div>
    <div runat="server" id="divDdlPage" class="wuc_pager_block">
        跳到第<span>
            <asp:DropDownList ID="DdlPage" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlPage_SelectedIndexChanged">
            </asp:DropDownList>
        </span>頁/<asp:Label ID="LblTotalPage" runat="server"></asp:Label>頁
    </div>
    <div runat="server" id="divTbxPagePer" class="wuc_pager_block" visible="false">
        <asp:Label ID="LblWarning" runat="server" Text="每頁筆數過多會導致網頁讀取較慢,請耐心等候" class="wuc_pager_warn"
            ForeColor="Red" Style="display: none;"></asp:Label>
        <asp:TextBox ID="TbxPagePer" runat="server" AutoPostBack="True" OnTextChanged="TbxPagePer_TextChanged"
            Width="25px" MaxLength="3" Style="text-align: left;" onmouseover="$(this).parent().find('span').toggle();"
            onmouseout="$(this).parent().find('span').hide();"></asp:TextBox>筆/頁
    </div>
    <div class="wuc_pager_block">
        共
        <span>
            <asp:Label ID="LblTotalAmount" runat="server"></asp:Label>
        </span>
        筆
    </div>
    <p style="clear: both;">
    </p>
    <asp:HiddenField ID="hdfPageSize" runat="server" Visible="false" />
    <asp:HiddenField ID="hdfTotalData" runat="server" Visible="false" />
    <asp:HiddenField ID="hdfNowPage" runat="server" Visible="false" />
    <asp:HiddenField ID="hdfCapacity" runat="server" Visible="false" />
    <asp:HiddenField ID="hdfsPage" runat="server" Visible="false" />
    <asp:HiddenField ID="hdfePage" runat="server" Visible="false" />
    <asp:HiddenField ID="hdfMaxPageSize" runat="server" Visible="false" />
</div>
