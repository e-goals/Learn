<%@ Page Title="" Language="C#" MasterPageFile="Master.master" AutoEventWireup="true" CodeFile="content.aspx.cs" Inherits="Content" Trace="true" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="cph_head" runat="server">
  <style type="text/css">
    body { font-family: Consolas; }
  </style>
</asp:Content>
<asp:Content ID="ContentBody" ContentPlaceHolderID="cph_body" runat="server">
  <%= PageEvents.Output %>
</asp:Content>
