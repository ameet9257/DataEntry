<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="DataEntery.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="loginpage" runat="server">
        <div>
            <asp:TextBox ID="inputUserName" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:TextBox ID="inputUserPwd" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="login_button" runat="server" Text="Login" OnClick="login_button_Click" />
        </div>
    </form>
</body>
</html>
