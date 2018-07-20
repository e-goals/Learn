﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="content-type" content="text/html; charset=utf-8" />
  <meta name="author" content="EASYGOAL.NET" />
  <meta name="description" content="Test Web Site" />
  <meta name="keywords" content="Web, CSS, Javascript" />
  <title>abc</title>
  <link href="Image/favicon.ico" rel="shortcut icon" />
  <link href="Image/favicon.ico" rel="shortcut icon" />
  <script src="script/common.js" type="text/javascript"></script>
  <script src="script/common.js" type="text/javascript"></script>
</head>
<body>
  <form id="_form" runat="server">
    <div>
      <asp:TextBox ID="tbox_test" runat="server" Text="<%# text %>"></asp:TextBox>
    </div>
  </form>
</body>
</html>