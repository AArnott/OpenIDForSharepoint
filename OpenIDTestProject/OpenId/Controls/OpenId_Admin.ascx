<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OpenId_Admin.ascx.cs"    Inherits="OpenId_Controls_OpenId_Admin" %>



<asp:Repeater ID="rpt_membership_user" runat="server"  OnItemDataBound="rpt_membership_user_DataBound" OnItemCommand="rpt_membership_user_ItemCommand">
    <HeaderTemplate>
        <table border ="1" cellpadding="0" style="border-collapse:collapse;" >
            <tr>
                <td>
                    <strong>User Name</strong> </td>
                <td>
                     <strong>Linked OpenId</strong></td>
            </tr>
    </HeaderTemplate>
   
    <ItemTemplate> 
    <tr>
        <td>
            <%# DataBinder.Eval(Container.DataItem,"UserName") %>
            <input id="hiddenInputUserId" runat="server" type="hidden" value='<%# DataBinder.Eval(Container.DataItem,"ProviderUserKey") %>' />
        </td>
        <td>
            <asp:Repeater Id="rpt_openId" runat="server" OnItemCommand="rpt_openId_ItemCommand">
            <HeaderTemplate><table border="0px"></HeaderTemplate>
            <ItemTemplate>
            <tr>
            <td><%#Container.DataItem.ToString()%>
            <input id="hiddenInputOpenId" runat="server" type="hidden" value="<%# Container.DataItem.ToString() %>" /> </td>
            <td><asp:Button ID="btnRemoveOpenId" runat="server" CommandName="RemoveOpenId" Text="Remove" /></td>
            </tr>
           
            </ItemTemplate>
            <FooterTemplate></table></FooterTemplate>
            </asp:Repeater>
            <br />
            <asp:TextBox ID="tbxNewOpenId" runat="server"></asp:TextBox>
           <br />
            <asp:Button ID="btnAddOpenId" runat="server" CommandName="AddNewOpenId" Text="Add" />
        </td>
    </tr>
    
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>
