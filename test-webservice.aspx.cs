using System;

public partial class test_webservice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Network s = new Network();
            Label_HostAddress.Text = s.HostAddress();
        }
    }
}