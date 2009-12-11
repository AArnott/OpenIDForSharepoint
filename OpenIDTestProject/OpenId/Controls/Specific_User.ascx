<%@ Control Language="C#" AutoEventWireup="true" %>
<fieldset>
    <!-- add H2 here and hide it with css since you can not put h2 inside a legend tag -->
    <h2 class="none">
        &nbsp;</h2>
    <legend><strong>User Detail</strong></legend>
    <br />
<asp:ObjectDataSource ID="UserData" runat="server" SelectMethod="GetUser" TypeName="OrbitOne.OpenId.TestProject.DataProvider">
    <SelectParameters>
        <asp:ControlParameter ControlID="LoginName" DefaultValue="" Name="UserName" PropertyName="Page.User.Identity.Name"
            Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:DetailsView ID="DetailsView" runat="server" Height="50px"
    Width="344px" DataSourceID="UserData" AutoGenerateRows="False">
    <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
    <Fields>
        <asp:BoundField DataField="UserName" HeaderText="User Name" />
        <asp:BoundField DataField="Email" HeaderText="e-mail" />
        <asp:BoundField DataField="Comment" HeaderText="Comment" />
        <asp:BoundField DataField="CreationDate" HeaderText="Creation Date" />
        <asp:BoundField DataField="LastLoginDate" HeaderText="Last Login Date" />
        <asp:BoundField DataField="LastActivityDate" HeaderText="Last Activity Date" />
        <asp:BoundField DataField="LastPasswordChangedDate" HeaderText="Last Password Changed Date" />
        <asp:BoundField DataField="LastLockOutDate" HeaderText="Last Lock Out Date" />
        <asp:CheckBoxField DataField="IsLockedOut" HeaderText="Is Locked Out" />
        <asp:CheckBoxField DataField="IsApproved" HeaderText="Is Approved" />
    </Fields>
</asp:DetailsView>
    <asp:LoginName ID="LoginName" runat="server" Visible="False" />
</fieldset>
