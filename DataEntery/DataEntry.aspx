<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataEntry.aspx.cs" Inherits="DataEntery.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="DataEntry" runat="server">
        <div>

            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Select Date:"></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="datePicker" runat="server" textmode="Date"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="labelDataNo" runat="server" Text="Data No."></asp:Label>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="textDataNo" runat="server"></asp:TextBox>

            <br />
            <br />
            <asp:Label ID="lableBank" runat="server" Text="Bank"></asp:Label>

        &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="dropDownBank" runat="server"></asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="labelRemark" runat="server" Text="Remark"></asp:Label>
        &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="inputRemark" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="buttonProceed1" runat="server" Text="Proceed" OnClick="buttonProceed1_Click" />
            <br />
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Select Account:"></asp:Label>

        &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="dropDownAcc" runat="server">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="lableCredit" runat="server" Text="Enter Credit Amount:"></asp:Label>

        &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="textCredit" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="lableDebit" runat="server" Text="Enter Debit Amount:"></asp:Label>

        &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="textDebit" runat="server"></asp:TextBox>

            <br />
            <br />
            <asp:Button ID="buttonProceed2" runat="server" Text="Proceed" OnClick="buttonProceed2_Click" />

        </div>
    </form>
</body>
</html>
