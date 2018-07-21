using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;

public partial class Index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void Render(HtmlTextWriter writer)
    {
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

        base.Render(htmlWriter);
        htmlWriter.Flush();
        htmlWriter.Close();
        string pageContent = stringWriter.ToString().Replace("\t", "  ");
        //pageContent = pageContent.Replace("<head><", "<head>\r\n  <").Replace("></head>", ">\r\n</head>"); 
        pageContent = Regex.Replace(pageContent, @"<head>(?<head>(\S|\s)+)</head>", "<head>\r\n  ${head}\r\n</head>");
        pageContent = Regex.Replace(pageContent, @"<title>\r\n\s*(?<title>[^<]+)\r\n</title>", "<title>${title}</title>");
        pageContent = pageContent.Replace("/><", "/>\r\n  <").Replace("\r\n\r\n", "\r\n");
        pageContent = pageContent.Replace("></form>", ">\r\n</form>");
        pageContent = Regex.Replace(pageContent, "\r\n(?<tag><(/form|div|/div|input))", "\r\n  ${tag}");
        //pageContent = Regex.Replace(pageContent, @"<div class=\SaspNetHidden\S>(\r\n)+\s*(?<input><input(\S|\s)+))(\r\n)+</div>", "    <div class=\"aspNetHidden\">\r\n      ${input}\r\n    </div>");
        Response.Write(pageContent);
    }

}