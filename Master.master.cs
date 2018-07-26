using System;

public partial class Master : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        PageEvents.Trace(this);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        PageEvents.Trace(this);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        PageEvents.Trace(this);
    }

}