<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log-viewer.aspx.cs" Inherits="Log" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
</head>
<body>
  <form id="_form" runat="server">
    <div>
      <span style="display: block;"><%= "<a href='./log/" + System.DateTime.Now.ToString("yyyy-MM/yyyyMMdd") + ".log.xml'> >>>查看错误日志<<<</a>" %></span>
      <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" Width="100%">
        <Columns>
          <asp:TemplateField HeaderText="#" HeaderStyle-Width="30px">
            <ItemTemplate><%#(Container.DataItemIndex + 1) %></ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Timestamp" HeaderStyle-Width="100px">
            <ItemTemplate><%# ((DateTime)((EZGoal.Log)Container.DataItem).Timestamp).ToString("yyyy-MM-dd HH:mm:ss fffffff") %></ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Timetaken" HeaderStyle-Width="90px">
            <ItemTemplate><%# ((EZGoal.Log)Container.DataItem).Timetaken %>ms</ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="HTTP Request">
            <ItemTemplate>
              Method: <%# ((EZGoal.Log)Container.DataItem).Method %><br />
              URL: <%# ((EZGoal.Log)Container.DataItem).ExactURL %><br />
              File: <%# ((EZGoal.Log)Container.DataItem).FilePath %><br />
              Referrer: <%# ((EZGoal.Log)Container.DataItem).Referrer %>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Client">
            <ItemTemplate>
              <%# ((EZGoal.Log)Container.DataItem).UserHost %>, <%# ((EZGoal.Log)Container.DataItem).UserPort %><br />
              <%# ((EZGoal.Log)Container.DataItem).UserAgent %><br />
              HTTP_DNT: <%# ((EZGoal.Log)Container.DataItem).HTTP_DNT %><br />
              HTTP_Via: <%# ((EZGoal.Log)Container.DataItem).HTTP_Via %><br />
              HTTP_XFF: <%# ((EZGoal.Log)Container.DataItem).HTTP_X_Forwarded_For %>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Status" HeaderStyle-Width="200px">
            <ItemTemplate>
              <%# ((EZGoal.Log)Container.DataItem).StatusCode %><br />
              <%# ((EZGoal.Log)Container.DataItem).StatusText %>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
      </asp:GridView>

      <asp:DataList ID="DataList1" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <FooterTemplate>
        </FooterTemplate>
        <ItemTemplate>
        </ItemTemplate>
      </asp:DataList>
    </div>
  </form>
</body>
</html>
