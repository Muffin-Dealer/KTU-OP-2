<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="SavarankiskasPirmas.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Konkurso dalyvio registracija:"></asp:Label>
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red" />
        <br />
        <br />
        Vardas:
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Vardas yra privalomas" ForeColor="Red"></asp:RequiredFieldValidator>
        <br />
        <br />
        Amžius:
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DropDownList1" ErrorMessage="Neteisinga metų reikšmė" ForeColor="Red" MaximumValue="25" MinimumValue="14" Type="Integer"></asp:RangeValidator>
        <br />
        <br />
        Programavimo kalba:<br />
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" DataSourceID="XmlDataSource1" DataTextField="pavadinimas" DataValueField="pavadinimas">
        </asp:CheckBoxList>
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/App_Data/kalbos.xml"></asp:XmlDataSource>
        <br />
        <br />
        <asp:Button ID="RegisterButton" runat="server" OnClick="Button_Register_Click" Text="Registruotis" Width="99px" CausesValidation="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="DeleteInputButton" runat="server" CausesValidation="False" OnClick="DeleteInputButton_Click" Text="Trinti Įvestį" />
        <br />
        <br />
        <asp:Label ID="Counter" runat="server" Text="Label"></asp:Label>
        <asp:Table ID="DataTable" runat="server">
        </asp:Table>
    </form>
</body>
</html>
