<%@ Page Language="C#" AutoEventWireup="true" CodeFile="send-http.aspx.cs" Inherits="SendHttp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /><br />
      <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox><br />
      <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine" Width="100%"></asp:TextBox><br />
    </div>
  </form>
</body>
</html>
