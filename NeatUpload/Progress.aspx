<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
        "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Page Language="c#" AutoEventWireup="false" Inherits="Brettle.Web.NeatUpload.ProgressPage" %>

<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%--
NeatUpload - an HttpModule and User Controls for uploading large files
Copyright (C) 2005  Dean Brettle

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
--%>
<html>
<head runat="server">
  <title>Upload Progress</title>
  <link rel="stylesheet" type="text/css" title="default" href="default.css" />
</head>
<body>
  <form id="dummyForm" runat="server">
    <table id="progressDisplay" class="ProgressDisplay">
      <tr>
        <td>
          <span id="label" runat="server" class="Label">Upload&#160;Status:&#160;</span>
        </td>
        <td id="barTd">
          <div id="statusDiv" runat="server">
            <Upload:DetailsSpan ID="normalInProgress" runat="server" WhenStatus="NormalInProgress" CssClass="DetailsSpan"><%# FormatCount(BytesRead) %>/<%# FormatCount(BytesTotal) %> <%# CountUnits %>(<%# String.Format("{0:0%}", FractionComplete) %>) at <%# FormatRate(BytesPerSec) %> - <%# FormatTimeSpan(TimeRemaining) %> left</Upload:DetailsSpan>
            <Upload:DetailsSpan ID="chunkedInProgress" runat="server" WhenStatus="ChunkedInProgress" CssClass="DetailsSpan"><%# FormatCount(BytesRead) %> <%# CountUnits %> at <%# FormatRate(BytesPerSec) %>	- <%# FormatTimeSpan(TimeElapsed) %> elapsed</Upload:DetailsSpan>
            <Upload:DetailsSpan ID="processing" runat="server" WhenStatus="ProcessingInProgress ProcessingCompleted" CssClass="DetailsSpan"><%# ProcessingHtml %></Upload:DetailsSpan>
            <Upload:DetailsSpan ID="completed" runat="server" WhenStatus="Completed">Complete: <%# FormatCount(BytesRead) %> <%# CountUnits %> at <%# FormatRate(BytesPerSec) %> took <%# FormatTimeSpan(TimeElapsed) %></Upload:DetailsSpan>
            <Upload:DetailsSpan ID="cancelled" runat="server" WhenStatus="Cancelled">Cancelled!</Upload:DetailsSpan>
            <Upload:DetailsSpan ID="rejected" runat="server" WhenStatus="Rejected">Rejected: <%# Rejection != null ? Rejection.Message : "" %></Upload:DetailsSpan>
            <Upload:DetailsSpan ID="error" runat="server" WhenStatus="Failed">Error: <%# Failure != null ? Failure.Message : "" %></Upload:DetailsSpan>
            <Upload:DetailsDiv ID="barDetailsDiv" runat="server" UseHtml4="true" Width='<%# Unit.Percentage(Math.Floor(100*FractionComplete)) %>' CssClass="ProgressBar">
            </Upload:DetailsDiv>
          </div>
        </td>
        <td>
          <asp:HyperLink ID="cancel" runat="server" Visible='<%# CancelVisible %>' NavigateUrl='<%# CancelUrl %>' ToolTip="Cancel Upload" CssClass="ImageButton"><img id="cancelImage" src="cancel.png" alt="Cancel Upload" /></asp:HyperLink>
          <asp:HyperLink ID="refresh" runat="server" Visible='<%# StartRefreshVisible %>' NavigateUrl='<%# StartRefreshUrl %>' ToolTip="Refresh" CssClass="ImageButton"><img id="refreshImage" src="refresh.png" alt="Refresh" /></asp:HyperLink>
          <asp:HyperLink ID="stopRefresh" runat="server" Visible='<%# StopRefreshVisible %>' NavigateUrl='<%# StopRefreshUrl %>' ToolTip="Stop Refreshing" CssClass="ImageButton"><img id="stopRefreshImage" src="stop_refresh.png" alt="Stop Refreshing" /></asp:HyperLink>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>
