using System;
using System.Diagnostics;
using System.Reflection;

public class BasePage : System.Web.UI.Page
{
    public BasePage() { }

    private static string ClassName()
    {
        return MethodBase.GetCurrentMethod().DeclaringType.Name;
    }

    private string Output(long counter, string eventName)
    {
        string className = MethodBase.GetCurrentMethod().DeclaringType.Name;
        return string.Format("{0} - {1}.{2}", counter, className, eventName);
    }

    // Occurs before page initialization.
    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    // Occurs when the server control is initialized, which is the first step in its lifecycle.
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    // Occurs when page initialization is complete.
    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    // Occurs before the page Load event.
    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    // Occurs when the server control is loaded into the Page object.
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    // Occurs at the end of the load stage of the page's life cycle.
    protected override void OnLoadComplete(EventArgs e)
    {
        base.OnLoadComplete(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    // Occurs after the Control object is loaded but prior to rendering.
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    // Occurs before the page content is rendered.
    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }

    // Occurs after the page has completed saving all view state and control state information for the page and controls on the page.
    protected override void OnSaveStateComplete(EventArgs e)
    {
        base.OnSaveStateComplete(e);
        string method = MethodBase.GetCurrentMethod().Name;
        PageEvents.Append(EasyGoal.PreciseCounter.Counter, ClassName(), method);
        Trace.Warn(Output(EasyGoal.PreciseCounter.Counter, method));
    }
}