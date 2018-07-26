using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class FileViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ListFiles();
        }
    }

    private void ListFiles()
    {
        string path = Server.MapPath("~/");
        var ds = System.IO.Directory.GetDirectories(path);
        DirectoryInfo di = new DirectoryInfo(path);

        DirectoryInfo[] subDirs = di.GetDirectories();
        foreach (DirectoryInfo subd in subDirs)
        {
            
            Response.Write(subd.Name);
            Response.Write("<br />");
        }
        Response.Write("<br />");
        FileInfo[] fis = di.GetFiles();
        foreach (FileInfo fi in fis)
        {
            Response.Write(fi.Name);
            Response.Write("<br />");
        }
    }
}