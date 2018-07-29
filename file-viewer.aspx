<%@ Page Language="C#" AutoEventWireup="true" CodeFile="file-viewer.aspx.cs" Inherits="FileViewer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
  <style type="text/css">
    html, body { height: 100%; margin: 0; padding: 0; width: 100%; }
    form { padding: 30px; }
    ul { margin: 0; padding: 0; list-style-position: inside; list-style-type: none; width: 100%; }
      ul li { margin: 0; padding: 0; }
        ul li > span { display: block; font-size: 18px; font-weight: bold; line-height: 2.25em; }
        ul li a { border-bottom: 1px solid #ddd; color: #000; display: block; height: 2.25em; line-height: 2.25em; text-decoration: none; }
          ul li a:hover { box-shadow: 0px 1px 10px #000; }
          ul li a span { display: block; }
    span.name { float: left; padding-left: 10px; text-align: left; }
    span.time { float: right; text-align: center; width: 240px; }
    span.size { float: right; padding-right: 20px; text-align: right; width: 100px; }
    .clearfix { zoom: 1; }
      .clearfix:after { display: table; clear: both; content: ""; }
  </style>
</head>
<body>
  <form id="_form" runat="server">
    <div>
      <a href="file-viewer.aspx<%= GetParentDirectory() %>" target="_self">返回上层</a><br />
      <asp:Repeater ID="Repeater_Contents" runat="server">
        <HeaderTemplate>
        <ul>
          <li class="clearfix">
            <span class="name">Name</span>
            <span class="size">Size</span>
            <span class="time">Last Modified</span>
          </li>
        </HeaderTemplate>
        <ItemTemplate>
          <li>
            <a href="<%# ((Dircontent)Container.DataItem).Type == "d" ? "file-viewer.aspx?dir=" + ((Dircontent)Container.DataItem).Path : ((Dircontent)Container.DataItem).Path %>" target="<%# ((Dircontent)Container.DataItem).Type == "d" ? "_self" : "_blank" %>">
              <span class="name"><%# ((Dircontent)Container.DataItem).Name %></span>
              <span class="size"><%# ((Dircontent)Container.DataItem).Type == "d" ? " --- " : FileSizeToString(((Dircontent)Container.DataItem).Size) %></span>
              <span class="time"><%# ((Dircontent)Container.DataItem).Time.ToString("yyyy-MM-dd HH:mm:ss") %></span>
            </a>
          </li>
        </ItemTemplate>
        <FooterTemplate>
        </ul>
        </FooterTemplate>
      </asp:Repeater>
    </div>
  </form>
</body>
</html>
