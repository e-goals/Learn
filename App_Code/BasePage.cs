using System;

namespace EZGoal
{
    public class BasePage : System.Web.UI.Page
    {
        public BasePage() { }

        // Occurs before page initialization.
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);
            PageEvents.Trace(this);
        }

        // Occurs when the server control is initialized, which is the first step in its lifecycle.
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PageEvents.Trace(this);
        }

        // Occurs when page initialization is complete.
        protected override void OnInitComplete(EventArgs e)
        {
            base.OnInitComplete(e);
            PageEvents.Trace(this);
        }

        // Occurs before the page Load event.
        protected override void OnPreLoad(EventArgs e)
        {
            base.OnPreLoad(e);
            PageEvents.Trace(this);
        }

        // Occurs when the server control is loaded into the Page object.
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PageEvents.Trace(this);
        }

        // Occurs at the end of the load stage of the page's life cycle.
        protected override void OnLoadComplete(EventArgs e)
        {
            base.OnLoadComplete(e);
            PageEvents.Trace(this);
        }

        // Occurs after the Control object is loaded but prior to rendering.
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            PageEvents.Trace(this);
        }

        // Occurs before the page content is rendered.
        protected override void OnPreRenderComplete(EventArgs e)
        {
            base.OnPreRenderComplete(e);
            PageEvents.Trace(this);
        }

        // Occurs after the page has completed saving all view state and control state information for the page and controls on the page.
        protected override void OnSaveStateComplete(EventArgs e)
        {
            base.OnSaveStateComplete(e);
            PageEvents.Trace(this);
        }
    }
}