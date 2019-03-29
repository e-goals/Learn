<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ip.aspx.cs" Inherits="ip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
</head>
<body>
  <form id="_form" runat="server">
    <div>
      <asp:TextBox ID="TextBox_IP" runat="server"></asp:TextBox>
      <asp:Button ID="Button_Query" runat="server" Text="查询" OnClick="Button_Query_Click" />
    </div>
    <div>
      <asp:Label ID="Label_Result" runat="server"></asp:Label>
    </div>
  </form>
</body>
</html>
