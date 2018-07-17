using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;

public partial class index : System.Web.UI.Page
{
    protected string text = "<script type=\"text/javascript\">window.alert(\"hello\")</script>";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.DataBind();
        }
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

    private static string RunningTime(long counter, long frequency)
    {
        long time_s = counter / frequency;
        int d = (int)(counter / frequency / 3600.0 / 24);
        int h = (int)(counter / frequency / 3600.0) - d * 24;
        int m = (int)(counter / frequency / 60.0) - (d * 24 + h) * 60;
        int s = (int)(counter / frequency) - ((d * 24 + h) * 60 + m) * 60;
        return string.Format("{0} days {1} hours {2} minutes {3} seconds", d, h, m, s);
    }
}