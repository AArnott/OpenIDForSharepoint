<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Permissions_And_Roles.ascx.cs" Inherits="Open_Id_Controls_Permissions_And_Roles" %>
<%@ Register Src="All_Users.ascx" TagName="All_Users" TagPrefix="uc1" %>
&nbsp;<div style="text-align: center">
    <table>
        <tr>
            <td colspan="2" style="text-align: center">
    <legend>
        <asp:GridView ID="ListUser" runat="server" AllowPaging="True" DataKeyNames="userName"
            DataSourceID="UserData" AutoGenerateColumns="False">
            <Columns>
                <asp:CommandField SelectText="&lt;img src=&quot;../Images/select.gif&quot;&gt;" ShowSelectButton="True" />
                <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" SortExpression="CreationDate" />
                <asp:BoundField DataField="LastLoginDate" HeaderText="Last Login Date" SortExpression="LastLoginDate" />
                <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="Last Password Changed Date"
                    SortExpression="LastPasswordChangedDate" />
                <asp:BoundField DataField="LastLockoutDate" HeaderText="Last Lockout Date" SortExpression="LastLockoutDate" />
                <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Is Locked Out" SortExpression="IsLockedOut" />
                <asp:CheckBoxField DataField="IsApproved" HeaderText="Is Approved" SortExpression="IsApproved" />
            </Columns>
            <RowStyle HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="Gray" />
            <PagerStyle HorizontalAlign="Center" />
            <HeaderStyle HorizontalAlign="Center" />
        </asp:GridView>
        &nbsp;
        <asp:ObjectDataSource ID="UserData" runat="server" SelectMethod="GetsAllUsers" TypeName="OrbitOne.OpenId.TestProject.DataProvider">
            <SelectParameters>
                <asp:ControlParameter ControlID="ListUser" Name="totalRecords" PropertyName="Rows.Count"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
            </td>
        </tr>
        <tr>
            <td style="width: 100px; text-align: center;">
<fieldset style="text-align: center; width: 424px; height: 355px;">
    <!-- add H2 here and hide it with css since you can not put h2 inside a legend tag -->
    <h2 class="none" align="left">
        &nbsp;</h2>
    <legend style="font-family: Times New Roman"><strong>Roles</strong></legend>
    <asp:ObjectDataSource ID="RolesData" runat="server" SelectMethod="GetsAllRoles" TypeName="OrbitOne.OpenId.TestProject.DataProvider">
    </asp:ObjectDataSource>
    <asp:GridView ID="ListAllRoles" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False"
        DataSourceID="RolesData" Height="1px" OnPageIndexChanging="ListAllRoles_PageIndexChanging" PageSize="5" OnRowCommand="ListAllRoles_RowCommand" Font-Strikeout="False" Font-Underline="False" Width="230px">
        <Columns>
            <asp:BoundField DataField="RoleName" HeaderText="Role Name" >
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Add to Role" ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleName") %>'
                        Text='<img src="../Images/icon-save.gif" border="0" />'></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
        </Columns>
        <RowStyle HorizontalAlign="Left" />
        <HeaderStyle CssClass="grid-header" HorizontalAlign="Left" />
        <PagerStyle HorizontalAlign="Center" />
    </asp:GridView>
    <table border="0">
        <tr>
            <td colspan="3" style="height: 21px; text-align: center">
                Add Role</td>
        </tr>
        <tr>
            <td style="width: 100px; height: 17px">
                Role Name</td>
            <td style="width: 120px; height: 17px">
                <asp:TextBox ID="txtRoleName" runat="server"></asp:TextBox></td>
            <td style="width: 43px; height: 17px">
                <asp:Button ID="btnAddRole" runat="server" Text="Add" OnClick="btnAddRole_Click" Width="65px" /></td>
        </tr>
    </table>
</fieldset>
            </td>
            <td style="width: 100px; text-align: center;">
                <fieldset style="text-align: center; width: 424px; height: 355px;">
                    <!-- add H2 here and hide it with css since you can not put h2 inside a legend tag -->
                    <h2 class="none">
                        &nbsp;</h2>
                    <legend style="font-family: Times New Roman"><strong>User Roles</strong></legend>
    <asp:ObjectDataSource ID="UserInRoleData" runat="server" DeleteMethod="DeleteUsersFromRoles"
        SelectMethod="GetRolesForUser" TypeName="OrbitOne.OpenId.TestProject.DataProvider">
        <DeleteParameters>
            <asp:ControlParameter ControlID="ListUser" Name="userName" PropertyName="SelectedValue"
                Type="String" />
            <asp:Parameter Name="roleName" Type="String" />
        </DeleteParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="ListUser" Name="userName" PropertyName="SelectedValue"
                Type="String" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:GridView ID="ListUserInRole" runat="server" AllowPaging="True"
        DataSourceID="UserInRoleData" OnPageIndexChanging="ListUserInRole_PageIndexChanging" OnRowCommand="ListUserInRole_RowCommand" style="vertical-align: middle; line-height: normal; letter-spacing: normal; text-align: center" PageSize="5" Width="230px" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="Role Name" DataField="RoleName" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Delete from Role">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "RoleName") %>'
                       Text='<img src="../Images/icon-delete.gif" border="0" />'></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
        </Columns>
        <RowStyle HorizontalAlign="Left" />
        <HeaderStyle CssClass="grid-header" HorizontalAlign="Center" />
    </asp:GridView>
                    <br />
<asp:LoginName ID="LoginName" runat="server" Visible="False" />
                </fieldset>
            </td>
        </tr>
    </table>
</div>
<br />
<br />
&nbsp;<br />
&nbsp;
<br />
<br />
