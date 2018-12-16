<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="_default" EnableViewState="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title>Analysis of Proxy</title>
  <style type="text/css">
    table { border: 0 none; border-collapse: collapse; }
      table tr { font-family: Consolas, SimSun, sans-serif; font-size: 1.25rem; line-height: 2.5rem; }
        table tr th, table tr td { padding: 0; text-align: left; }
        table tr th { padding-right: 10px; }
        table tr td { color: red; }
  </style>
</head>
<body>
  <form id="_form">
    <table>
      <tr>
        <th>Request.ServerVariables["REMOTE_ADDR"]: </th>
        <td><%# Request.ServerVariables["REMOTE_ADDR"] %></td>
      </tr>
      <tr>
        <th>Request.ServerVariables["REMOTE_HOST"]: </th>
        <td><%# Request.ServerVariables["REMOTE_HOST"] %></td>
      </tr>
      <tr>
        <th>Request.ServerVariables["REMOTE_PORT"]: </th>
        <td><%# Request.ServerVariables["REMOTE_PORT"] %></td>
      </tr>
      <tr>
        <th>Request.ServerVariables["HTTP_USER_AGENT"]: </th>
        <td><%# Request.ServerVariables["HTTP_USER_AGENT"] %></td>
      </tr>
      <tr>
        <th>Request.ServerVariables["HTTP_Via"]: </th>
        <td><%# Request.ServerVariables["HTTP_Via"] %></td>
      </tr>
      <tr>
        <th>Request.ServerVariables["HTTP_X_Forwarded_For"]: </th>
        <td><%# Request.ServerVariables["HTTP_X_Forwarded_For"] %></td>
      </tr>
    </table>
    <a href="override-render-method.aspx" target="_blank">Override Render()</a>
  </form>
</body>
</html>
