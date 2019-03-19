using System;
using System.IO;
using System.Collections.Generic;

public partial class FileViewer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string dir = EZGoal.Common.GetParamValue("dir");
            if (dir == null)
                dir = "";
            var list = ListContents(dir);
            Repeater_Contents.DataSource = list;
            Repeater_Contents.DataBind();
        }
    }

    private List<Dircontent> ListContents(string path)
    {
        List<Dircontent> list = new List<Dircontent>();
        DirectoryInfo dInfo = null;
        FileInfo fInfo = null;
        string sPath = Server.MapPath("~/") + path;
        string[] dNames = Directory.GetDirectories(sPath);
        for (int i = 0; i < dNames.Length; i++)
        {
            dInfo = new DirectoryInfo(dNames[i]);
            Dircontent content = new Dircontent();
            content.Name = dInfo.Name;
            content.Path = dNames[i].Replace(Server.MapPath("~/"), "").Replace("\\", "/");
            content.Size = 0;
            content.Time = dInfo.LastWriteTime;
            content.Type = "d";
            content.Link = string.Format("href=\"file-viewer.aspx?dir={0}\" target=\"_self\"", content.Path);
            list.Add(content);
        }

        string[] fNames = Directory.GetFiles(sPath);
        for (int i = 0; i < fNames.Length; i++)
        {
            fInfo = new FileInfo(fNames[i]);
            Dircontent content = new Dircontent();
            content.Name = fInfo.Name;
            content.Path = fNames[i].Replace(Server.MapPath("~/"), "").Replace("\\", "/");
            content.Size = fInfo.Length;
            content.Time = fInfo.LastWriteTime;
            content.Type = "f";
            content.Link = string.Format("href=\"{0}\" target=\"_blank\"", content.Path);
            list.Add(content);
        }
        return list;
    }

    public string ParentDirectory()
    {
        string parent = "";
        string dir = EZGoal.Common.GetParamValue("dir");
        if (dir != null)
        {
            parent = Path.GetDirectoryName(dir).Replace("\\", "/");
        }
        return (parent != "") ? ("?dir=" + parent) : "";
    }

    public static string FileSizeToString(long size)
    {
        if ((size >> 10) == 0)
            return size + " B";
        else if ((size >> 20) == 0)
            return (size >> 10) + " KB";
        else if ((size >> 30) == 0)
            return (size >> 20) + " MB";
        else if ((size >> 40) == 0)
            return (size >> 30) + " GB";
        else if ((size >> 50) == 0)
            return (size >> 40) + " TB";
        else if ((size >> 60) == 0)
            return (size >> 50) + " PB";
        else
            return (size >> 60) + " ZB";
    }

}

public class Dircontent
{
    public string Link { get; set; }
    public string Name { get; set; }
    public string Path { get; set; }
    public long Size { get; set; }
    public DateTime Time { get; set; }
    public string Type { get; set; }
    public Dircontent() { }
}