using System;

public partial class TestError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_Compute_Click(object sender, EventArgs e)
    {
        int divisor = 0;
        int result = 100 / divisor;
        Label_Result.Text = result.ToString();
    }
}