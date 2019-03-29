<%@ Page Language="C#" AutoEventWireup="true" CodeFile="neatupload.aspx.cs" Inherits="NeatUpload" %>

<%@ Register Assembly="Brettle.Web.NeatUpload" Namespace="Brettle.Web.NeatUpload" TagPrefix="Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
  <title></title>
  <script type="text/javascript">
    function ToggleVisibility(id, type)   //进度条的隐藏和显示     
    {
      el = document.getElementById(id);
      if (el.style) {
        if (type == 'on') {
          el.style.display = 'block';
        }
        else {
          el.style.display = 'none';
        }
      }
      else {
        if (type == 'on') {
          el.display = 'block';
        }
        else {
          el.display = 'none';
        }
      }
    }
  </script>
</head>
<body>
  <form id="form1" runat="server">
    <div>
      <div>
        <Upload:InputFile ID="InputFile1" runat="server" />
        <asp:Button ID="Button_UploadSingle" runat="server" Text="上传" OnClick="Button_UploadSingle_Click" OnClientClick="ToggleVisibility('progressbar','on')" />
        <div style="border: 1px solid #FCC; margin: 0; padding: 0;">
          <div id="progressbar">
            <Upload:ProgressBar ID="ProgressBar1" runat='server' Inline="false" Triggers="Button_UploadSingle" Height="100px" Width="550px"></Upload:ProgressBar>
            <Upload:ProgressBar ID="ProgressBar2" runat='server' Inline="true" Triggers="Btn_Upload" Height="60px" Width="550px"></Upload:ProgressBar>
          </div>
        </div>
      </div>
      <Upload:MultiFile ID="MultiFile1" runat="server"></Upload:MultiFile>
      <asp:Button ID="Btn_Upload" runat="server" Text="上传" OnClick="Btn_Upload_Click" />
      <asp:Label ID="Lbl_Result" runat="server"></asp:Label>

    </div>
  </form>
</body>
</html>
