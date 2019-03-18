using System;
using System.Web.UI;

using Regex = System.Text.RegularExpressions.Regex;

public partial class Content : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        PageEvents.Trace(this);
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        PageEvents.Trace(this);
    }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);
        PageEvents.Trace(this);
    }

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);
        PageEvents.Trace(this);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        PageEvents.Trace(this);
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
        PageEvents.Trace(this);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        PageEvents.Trace(this);
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);
        PageEvents.Trace(this);
    }

    protected override void OnSaveStateComplete(EventArgs e)
    {
        base.OnSaveStateComplete(e);
        PageEvents.Trace(this);
    }

    protected override void Render(HtmlTextWriter writer)
    {
        var stringWriter = new System.IO.StringWriter();
        var htmlWriter = new System.Web.UI.HtmlTextWriter(stringWriter);

        base.Render(htmlWriter);

        htmlWriter.Flush();
        htmlWriter.Close();
        string html = stringWriter.ToString().Replace("\t", "  ");
        html = Regex.Replace(html, "(\r\n){2,}", "\r\n");
        html = Regex.Replace(html, "(\r\n\\s+)+(?<Follow>\r\n\\s+</)", "${Follow}");
        html = Regex.Replace(html, "\r\n(?<Indent>\\s+)(\r\n\\s+)+", "\r\n${Indent}");
        var match = Regex.Match(html, "\r\n(?<Indent>\\s+)<span>Page Events:");
        if (match.Success)
        {
            string indent = match.Groups["Indent"].Value;
            html = Regex.Replace(html, "<br />(?<counter><span>[0-9]{12})", "<br />\r\n" + indent + "${counter}");
        }
        Response.Write(html);
    }

}