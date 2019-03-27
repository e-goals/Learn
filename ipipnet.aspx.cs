using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ipdb;

public partial class ipipnet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            // City类可用于IPDB格式的IPv4免费库，IPv4与IPv6的每周高级版、每日标准版、每日高级版、每日专业版、每日旗舰版
            City db = new City(Server.MapPath("~/APP_Data/ipipfree.ipdb"));

            // db.find(address, language) 返回索引数组
            EZGoal.Common.Message(this.Page,string.Join(",", db.find("115.24.14.124", "CN")));

            // db.findInfo(address, language) 返回 CityInfo 对象
            CityInfo info = db.findInfo("118.28.1.1", "CN");
            EZGoal.Common.Message(this.Page, info.ToString());
        }
        catch (Exception ex)
        {
            EZGoal.Common.Message(this, ex.StackTrace);
        }
    }
}