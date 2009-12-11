<%@ Control Language="C#" AutoEventWireup="true"  %>
<div style="text-align: center">
    <table>
        <tr>
            <td align="center" style="width: 100px">
            <asp:GridView ID="ListUser" runat="server" AllowPaging="True" DataKeyNames="userName"
                DataSourceID="UserData" AutoGenerateColumns="False" Width="934px">
                <RowStyle HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="Gray" />
                <PagerStyle HorizontalAlign="Center" />
                <HeaderStyle HorizontalAlign="Center" />
                <Columns> 
                <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName" />
                <asp:BoundField DataField="OpenId" HeaderText="OpenId" SortExpression="OpenId" />                   
                    
                    <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                    <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" SortExpression="CreationDate" />
                    <asp:BoundField DataField="LastLoginDate" HeaderText="Last Login Date" SortExpression="LastLoginDate" />
                    <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="Last Password Changed Date"
                        SortExpression="LastPasswordChangedDate" />
                    <asp:BoundField DataField="LastLockoutDate" HeaderText="Last Lockout Date" SortExpression="LastLockoutDate" />
                    <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Is Locked Out" SortExpression="IsLockedOut" />
                    <asp:CheckBoxField DataField="IsApproved" HeaderText="Is Approved" SortExpression="IsApproved" />
                    <asp:TemplateField ShowHeader="False" HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "UserName") %>'
                                Text='<img src="../Images/icon-delete.gif" border="0" />'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 100px">
    <asp:HyperLink ID="hlCreateNewUser" runat="server" NavigateUrl="~/OpenId/Pages/Create_User.aspx">Create new user</asp:HyperLink></td>
        </tr>
        <tr>
            <td style="width: 100px">
                <asp:ObjectDataSource ID="UserData" runat="server" SelectMethod="GetsAllOpenIdUsers" TypeName="OrbitOne.OpenId.TestProject.DataProvider" DeleteMethod="DeleteUser">
                    <SelectParameters>
                        <asp:Parameter Name="totalRecords" Type="Int32" />
                    </SelectParameters>
                    <DeleteParameters>
                        <asp:ControlParameter ControlID="ListUser" Name="userName" PropertyName="SelectedValue"
                            Type="String" />
                    </DeleteParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    &nbsp;</div>
