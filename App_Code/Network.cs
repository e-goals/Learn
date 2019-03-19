using System.Web.Services;

/// <summary>
/// Network 的摘要说明
/// </summary>
[WebService(Namespace = "http://ezgoals.net/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
[System.Web.Script.Services.ScriptService]
public class Network : System.Web.Services.WebService
{

    public Network() { }

    [WebMethod]
    public string HostAddress()
    {
        return EZGoal.Common.Current.Request.UserHostAddress;
    }

}