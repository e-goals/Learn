using System;
using System.Web;
using System.Web.UI;

public partial class TraceEvents :BasePage//, ICallbackEventHandler
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Trace.Warn("Page_Load");
    }

    public override void ApplyStyleSheetSkin(Page page)
    {
        base.ApplyStyleSheetSkin(page);
        Trace.Warn("ApplyStyleSheetSkin");
    }

    protected override HtmlTextWriter CreateHtmlTextWriter(System.IO.TextWriter tw)
    {
        Trace.Warn("CreateHtmlTextWiter");
        return base.CreateHtmlTextWriter(tw);
    }

    protected override ControlCollection CreateControlCollection()
    {
        Trace.Warn("CreateControlCollection");
        return base.CreateControlCollection();
    }

    protected override System.Collections.Specialized.NameValueCollection DeterminePostBackMode()
    {
        Trace.Warn("DeterminePostBackMode");
        return base.DeterminePostBackMode();
    }

    protected override void InitializeCulture()
    {
        Trace.Warn("InitializeCulture");
        base.InitializeCulture();
    }

    protected override void FrameworkInitialize()
    {
        Trace.Warn("FrameworkInitialize");
        base.FrameworkInitialize();
    }

    protected override void InitOutputCache(int duration, string varyByHeader, string varyByCustom, OutputCacheLocation location, string varyByParam)
    {
        Trace.Warn("InitOutputCache");
        base.InitOutputCache(duration, varyByHeader, varyByCustom, location, varyByParam);
    }

    protected override object LoadPageStateFromPersistenceMedium()
    {
        Trace.Warn("LoadPageStateFromPersistenceMedium");
        return base.LoadPageStateFromPersistenceMedium();
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Trace.Warn("OnInit");
    }

    protected override void OnInitComplete(EventArgs e)
    {
        Trace.Warn("OnInitComplete");
        base.OnInitComplete(e);   
    }

    protected override void OnLoadComplete(EventArgs e)
    {
        Trace.Warn("OnLoadComplete");
        base.OnLoadComplete(e);  
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
        Trace.Warn("OnPreInit");
    }

    protected override void OnPreLoad(EventArgs e)
    {
        base.OnPreLoad(e);
        Trace.Warn("OnPreLoad");
    }

    protected override void OnPreRenderComplete(EventArgs e)
    {
        base.OnPreRenderComplete(e);
        Trace.Warn("OnPreRenderComplete");
    }

    protected override void OnSaveStateComplete(EventArgs e)
    {
        base.OnSaveStateComplete(e);
        Trace.Warn("OnSaveStateComplete");
    }

    public override void ProcessRequest(HttpContext context)
    {
        base.ProcessRequest(context);
        Trace.Warn("ProcessRequest");
    }

    protected override void RaisePostBackEvent(IPostBackEventHandler sourceControl, string eventArgument)
    {
        Trace.Warn("RaisePostBackEvent");
        base.RaisePostBackEvent(sourceControl, eventArgument);
    }

    protected override void Render(HtmlTextWriter writer)
    {
        base.Render(writer);
        Trace.Warn("Render");
    }

    protected override void SavePageStateToPersistenceMedium(object state)
    {
        base.SavePageStateToPersistenceMedium(state);
        Trace.Warn("SavePageStateToPersistenceMedium");
    }

}