<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestError.aspx.cs" Inherits="TestError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
</head>
<body>
  <form id="_form" runat="server">
    <div>
      <span>点击按钮将计算 100/0 </span>
      <asp:Button ID="Button_Compute" runat="server" Text="Button" OnClick="Button_Compute_Click" />
      <asp:Label ID="Label_Result" runat="server" Text="Label"></asp:Label>
    </div>
  </form>
</body>
</html>
