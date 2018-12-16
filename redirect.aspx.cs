using System;
using System.Web.Configuration;

public partial class redirect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string redirectUrl = WebConfigurationManager.AppSettings["redirect-url"];

            if (redirectUrl == null)
            {
                Label_Error.Text = "ERROR: Missing the configuration parameter \"redirect-url\".";
                Label_Error.Visible = true;
            }
            else if (redirectUrl.Trim() == String.Empty)
            {
                Label_Error.Text = "ERROR: The value of the parameter \"redirect-url\" is blank.";
                Label_Error.Visible = true;
            }
            else
            {
                Response.Redirect(redirectUrl);
            }
        }
    }
}