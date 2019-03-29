<%@ Page Language="C#" AutoEventWireup="true" CodeFile="idnumber-creator.aspx.cs" Inherits="idnumber_creator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
  <style type="text/css">
    html, body { background: transparent; border: 0 none; height: 100%; margin: 0; padding: 0; width: 100%; }
    html { overflow-y: scroll; }
    body { color: #000; line-height: 1.5em; text-align: center; }
    form { background-color: #eee; margin: 0 auto; min-height: 100%; text-align: left; width: 1000px; }
  </style>
</head>
<body>
  <form id="_form" runat="server">
    <div style="padding: 50px;">
      <span>地区码：</span><asp:TextBox ID="TB_Area" runat="server"></asp:TextBox><br />
      <br />
      <span>日期码：</span><asp:TextBox ID="TB_Date" runat="server"></asp:TextBox><br />
      <br />
      <span>顺序码：</span><asp:TextBox ID="TB_Rank" runat="server"></asp:TextBox><br />
      <br />
      <asp:Button ID="B_Submit" runat="server" Text="生成" OnClick="B_Submit_Click" /><br />
      <br />
      <asp:Label ID="L_Result" runat="server"></asp:Label>
    </div>
  </form>
</body>
</html>
