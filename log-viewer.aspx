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
      <asp:GridView ID="GridView1" AutoGenerateColumns="false" runat="server" Width="100%">
        <Columns>
          <asp:TemplateField HeaderText="#" HeaderStyle-Width="30px">
            <ItemTemplate><%#(Container.DataItemIndex + 1) %></ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Timestamp" HeaderStyle-Width="100px">
            <ItemTemplate><%# ((DateTime)((EasyGoal.Log)Container.DataItem).Timestamp).ToString("yyyy-MM-dd HH:mm:ss fffffff") %></ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Timetaken" HeaderStyle-Width="90px">
            <ItemTemplate><%# ((EasyGoal.Log)Container.DataItem).Timetaken %>ms</ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="HTTP Request">
            <ItemTemplate>
              Method: <%# ((EasyGoal.Log)Container.DataItem).Method %><br />
              URL: <%# ((EasyGoal.Log)Container.DataItem).ExactURL %><br />
              File: <%# ((EasyGoal.Log)Container.DataItem).FilePath %><br />
              Referrer: <%# ((EasyGoal.Log)Container.DataItem).Referrer %>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Client">
            <ItemTemplate>
              <%# ((EasyGoal.Log)Container.DataItem).UserHost %>, <%# ((EasyGoal.Log)Container.DataItem).UserPort %><br />
              <%# ((EasyGoal.Log)Container.DataItem).UserAgent %><br />
              HTTP_DNT: <%# ((EasyGoal.Log)Container.DataItem).HTTP_DNT %><br />
              HTTP_Via: <%# ((EasyGoal.Log)Container.DataItem).HTTP_Via %><br />
              HTTP_XFF: <%# ((EasyGoal.Log)Container.DataItem).HTTP_X_Forwarded_For %>
            </ItemTemplate>
          </asp:TemplateField>
          <asp:TemplateField HeaderText="Status" HeaderStyle-Width="200px">
            <ItemTemplate>
              <%# ((EasyGoal.Log)Container.DataItem).StatusCode %><br />
              <%# ((EasyGoal.Log)Container.DataItem).StatusText %>
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
