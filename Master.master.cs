using System;
using System.Reflection;

public partial class Master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    private static string ClassName()
    {
        return MethodBase.GetCurrentMethod().DeclaringType.Name;
    }

    private static string Output(long counter, string method)
    {
        string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
        return string.Format("{0} - {1}.{2}", counter, className, method);
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(PreciseCounter.Counter, method));
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(PreciseCounter.Counter, method));
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(PreciseCounter.Counter, method));
    }

}