using System;
using System.Web.UI;

public partial class Log : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridView1.DataSource = EasyGoal.Log.GetTop(50);
            Page.DataBind();
        }
    }
}