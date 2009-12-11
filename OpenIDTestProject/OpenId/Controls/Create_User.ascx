<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Create_User.ascx.cs" Inherits="Open_Id_Controls_Create_User" %>
<fieldset>
    <!-- add H2 here and hide it with css since you can not put h2 inside a legend tag -->
    <h2 class="none">
        &nbsp;</h2>
    <legend><strong>User Detail</strong></legend>&nbsp;&nbsp;
    <asp:CreateUserWizard ID="CreateUserWizard" runat="server" OnCreatedUser="CreateUserWizard_CreatedUser">
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server">
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
        <StartNavigationTemplate>
            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Next" />
        </StartNavigationTemplate>
    </asp:CreateUserWizard>
</fieldset><fieldset>
    <!-- add H2 here and hide it with css since you can not put h2 inside a legend tag -->
    <h2 class="none">
        &nbsp;</h2>
    <legend><strong>OpenId User Detail</strong></legend>&nbsp;
    <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" MembershipProvider="OpenIDMembershipProvider" RequireEmail="False" AutoGeneratePassword="True" OnCreatedUser="CreateUserWizard1_CreatedUser">
        <WizardSteps>
            <asp:CreateUserWizardStep runat="server">
                <ContentTemplate>
                    <table border="0" class="textImag">
                        <tr>
                            <td align="center" colspan="2">
                                Sign Up for Your New Account</td>
                        </tr>
                        <tr>
                            <td align="right" style="height: 26px; width: 76px;">
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">OpenID:</asp:Label></td>
                            <td style="height: 26px">
                                <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                    ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2" style="color: red; height: 22px;">
                                <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:CreateUserWizardStep>
            <asp:CompleteWizardStep runat="server">
            </asp:CompleteWizardStep>
        </WizardSteps>
        <StartNavigationTemplate>
            <asp:Button ID="StartNextButton" runat="server" CommandName="MoveNext" Text="Next" />
        </StartNavigationTemplate>
    </asp:CreateUserWizard>
</fieldset>
