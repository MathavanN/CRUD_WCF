<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WcfDemoWebApp._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h4>Simple WCF Student Service</h4>
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    <table>
        <tr>
            <td>
                Student Id :
            </td>
            <td>
                <asp:TextBox ID="txtStudentId" runat="server" Enabled="false" />
            </td>
        </tr>
        <tr>
            <td>
                First Name :
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" style="width: 300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Last Name :
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server" style="width: 300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Register No. :
            </td>
            <td>
                <asp:TextBox ID="txtRegisterNo" runat="server" style="width: 300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Department :
            </td>
            <td>
                <asp:TextBox ID="txtDepartment" runat="server" style="width: 300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="ButtonInsert" runat="server" Text="Add" OnClick="InsertButton_Click"/>
                <asp:Button ID="ButtonUpdate" runat="server" visible="false" Text="Update" OnClick="InsertButton_Click"/>
                <asp:Button ID="ButtonDelete" runat="server" visible="false" Text="Delete" OnClick="DeleteButton_Click"/>
                <asp:Button ID="ButtonCancel" runat="server" visible="false" Text="Cancel" OnClick="CancelButton_Click"/>
            </td>
        </tr>
    </table>
    <asp:GridView ID="GridViewStudentDetails" DataKeyNames="StudentId" AutoGenerateColumns="false"
            runat="server" OnSelectedIndexChanged="GridViewStudentDetails_SelectedIndexChanged" Width="700">
            <HeaderStyle BackColor="#0A9A9A" ForeColor="White" Font-Bold="true" Height="30" />
            <AlternatingRowStyle BackColor="#f5f5f5" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnSelect" runat="server" CommandName="Select" Text="Select" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="First Name">
                    <ItemTemplate>
                        <asp:Label ID="lblFirstName" runat="server" Text='<%#Eval("FirstName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                        <asp:Label ID="lblLastName" runat="server" Text='<%#Eval("LastName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Register No.">
                    <ItemTemplate>
                        <asp:Label ID="lblRegisterNo" runat="server" Text='<%#Eval("RegisterNo") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate>
                        <asp:Label ID="lblDepartment" runat="server" Text='<%#Eval("Department") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

</asp:Content>
