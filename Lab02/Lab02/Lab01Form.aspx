<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lab01Form.aspx.cs" Inherits="Lab02.Lab01Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" runat="server" media="screen" href="~/css/styles.css" />
    <title>Lab02 U16</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="body">
            <asp:Label ID="HeaderLabel" runat="server" Text="LAB02 U16"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Tax Info U16a.txt:"></asp:Label>
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Every Citizen Tax Data U16b.txt: "></asp:Label>
            <br />
            <asp:FileUpload ID="FileUpload2" runat="server" />
            <br />
            <asp:Button ID="DataButton" runat="server" Text="Submit New Data" OnClick="DataButton_Click" />
            <br />
            <br />
            <asp:Label ID="InitTaxLabel" runat="server" Text="U16a.txt Initial data:"></asp:Label>
            <asp:Table ID="InitTaxTable" runat="server">
            </asp:Table>
            <br />
            <asp:Label ID="InitCitizenLabel" runat="server" Text="U16b.txt Initial data:"></asp:Label>
            <asp:Table ID="InitCitizenTable" runat="server">
            </asp:Table>
            <br />
            <asp:Panel ID="CalculationsPanel" runat="server">
                <asp:Label ID="CitizenTaxLabel" runat="server" Text="All Citizen taxes over the months"></asp:Label>
                <asp:Table ID="CitizenTaxTable" runat="server">
                </asp:Table>
                <br />
                <asp:Label ID="AverageTax" runat="server"></asp:Label>
                <br />
                <asp:Label ID="TotalTaxSum" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label ID="CitizenTaxLabel0" runat="server" Text="Above Average Tax:"></asp:Label>
                <asp:Table ID="AboveAverageTable" runat="server">
                </asp:Table>
                <br />
                <asp:Label ID="FilterData" runat="server" Text="Filtered data:"></asp:Label>
                <asp:Table ID="FilterTable" runat="server">
                </asp:Table>
                <br />
                Tax Code:<br />
                <asp:TextBox ID="TaxCodeTextBox" runat="server"></asp:TextBox>
                <br />
                Month:<br />
                <asp:TextBox ID="TaxMonthTextBox" runat="server"></asp:TextBox>
                <br />
                <asp:Button ID="ButtonFilter" runat="server" Text="Submit" OnClick="ButtonFilter_Click" />
            </asp:Panel>
            <br />
        </div>
    </form>
</body>
</html>
