using System;

public partial class ip : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button_Query_Click(object sender, EventArgs e)
    {
        EZGoal.CZIPDB db = new EZGoal.CZIPDB(Server.MapPath("~/App_Data/qqwry.dat"));


        EZGoal.IPRecord data = db.Query(TextBox_IP.Text.Trim());

        Label_Result.Text = string.Format("{0} {1} {2}", data.IP, data.Location1, data.Location2);

    }
}