using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;

public partial class Content : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private static string ClassName()
    {
        return MethodBase.GetCurrentMethod().DeclaringType.Name;
    }

    private string Output(long counter, string method)
    {
        string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
        return string.Format("{0} - {1}.{2}", counter, className, method);
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    protected override void OnSaveStateComplete(EventArgs e)
    {
        base.OnSaveStateComplete(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    protected override void Render(HtmlTextWriter writer)
    {
        StringWriter stringWriter = new StringWriter();
        HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

        base.Render(htmlWriter);
        htmlWriter.Flush();
        htmlWriter.Close();
        string pageContent = stringWriter.ToString().Replace("\t", "  ");
        pageContent = Regex.Replace(pageContent, "(\r\n){2,}", "\r\n");
        pageContent = Regex.Replace(pageContent, "(\r\n\\s+)+(?<Follow>\r\n\\s+</)", "${Follow}");
        pageContent = Regex.Replace(pageContent, "\r\n(?<Indent>\\s+)(\r\n\\s+)+", "\r\n${Indent}");
        Match m = Regex.Match(pageContent, "\r\n(?<Indent>\\s+)Page Events:");
        if (m.Success)
        {
            string indent = m.Groups["Indent"].Value;
            pageContent = Regex.Replace(pageContent, "<br />(?<counter>[0-9]{12})", "<br />\r\n" + indent + "${counter}");
        }
        Response.Write(pageContent);
    }

}