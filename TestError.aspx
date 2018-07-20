<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestError.aspx.cs" Inherits="TestError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>测试错误</title>
</head>
<body>
  <form id="_form" runat="server">
    <div>
      <asp:Button ID="Button_Compute" runat="server" Text="100 / 0 = ???" OnClick="Button_Compute_Click" />
    </div>
    <div>
      <a href="page1234567890.aspx" target="_self">不存在的页面</a>
    </div>
  </form>
</body>
</html>
