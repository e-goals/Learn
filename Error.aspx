<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
  <style type="text/css">
    html, body { height: 100%; margin: 0; padding: 0; }
    form { color: #F00; font-family: Consolas, SimSun, sans-serif; font-size: 24px; line-height: 1.5em; margin: 15px 20px; }
    .code { font-size: 48px; font-weight: bold; line-height: 1.5em; }
    .text { font-weight: bold; }
    .prompt { color: #999; font-size: 18px; }
  </style>
</head>
<body>
  <form id="_form">
    <div>
      <span class="code"><%= statusCode %></span>&emsp;<span class="text"><%= statusText %></span>
      <br />
      <span class="prompt"><%= statusCode==200?"":"您要访问的页面出错了！网站已自动记录日志！" %></span>
    </div>
  </form>
</body>
</html>
