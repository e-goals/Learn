using System;
using System.Collections.Generic;
using Brettle.Web.NeatUpload;
using System.Web.UI;

public partial class NeatUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button_UploadSingle_Click(object sender, EventArgs e)
    {
        if (InputFile1.HasFile)
        {
            string FileName = this.InputFile1.FileName;//获取上传文件的文件名,包括后缀
            string ExtenName = System.IO.Path.GetExtension(FileName);//获取扩展名
            string SaveFileName = System.IO.Path.Combine(
                    System.Web.HttpContext.Current.Request.MapPath("~/UpLoads/"),
                     DateTime.Now.ToString("yyyyMMddhhmm") + ExtenName);//合并两个路径为上传到服务器上的全路径
            InputFile1.MoveTo(SaveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
            string url = "UpLoads/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ExtenName;  //文件保存的路径
            float FileSize = (float)System.Math.Round((float)InputFile1.ContentLength / 1024000, 1); //获取文件大小并保留小数点后一位,单位是M
        }
        else
        {
            EZGoal.Common.Message(this, "您未选择任何文件!", true);
        }
    }

    protected void Btn_Upload_Click(object sender, EventArgs e)
    {
        List<string> files;
        bool uploaded = UploadFile(this, MultiFile1, "Uploads", out files);
        if (uploaded)
        {
            Lbl_Result.Text = "";
            foreach (string fname in files)
                Lbl_Result.Text += fname + ";<br />";
        }
    }

    public static bool UploadFile(Page page, MultiFile uploadControl, string relative, out List<string> uploadedFiles)
    {
        bool result = true;
        uploadedFiles = new List<string>();

        if (relative.StartsWith("/"))
            relative = relative.Substring(1);
        if (!relative.EndsWith("/"))
            relative += "/";
        string absolute = page.Server.MapPath("~/") + relative;
        Random random = new Random();
        foreach (UploadedFile file in uploadControl.Files)
        {
            string extension = System.IO.Path.GetExtension(file.FileName).ToUpper();
            string nFilename = EZGoal.Common.GenerateFilenameByGUID(extension);
            if (!System.IO.Directory.Exists(absolute))
                System.IO.Directory.CreateDirectory(absolute);

            if (file.ContentLength > 0)
            {
                try
                {
                    file.SaveAs(absolute + nFilename);
                }
                catch
                {
                    result = false;
                }
            }
            uploadedFiles.Add(relative + nFilename);
        }
        return result;
    }

    public static bool UploadFile(Page page, InputFile uploadControl, string relative, out string uploadedFile)
    {
        if (relative.StartsWith("/"))
            relative = relative.Substring(1);
        if (!relative.EndsWith("/"))
            relative += "/";
        string absolute = page.Server.MapPath("~/") + relative;

        string extension = System.IO.Path.GetExtension(uploadControl.FileName).ToUpper();
        string nFilename = EZGoal.Common.GenerateFilenameByTime(extension);

        if (!System.IO.Directory.Exists(absolute))
            System.IO.Directory.CreateDirectory(absolute);

        if (uploadControl.ContentLength > 0)
        {
            try
            {
                uploadControl.MoveTo(absolute + nFilename, MoveToOptions.Overwrite);
            }
            catch
            {
                uploadedFile = null;
                return false;
            }
        }
        uploadedFile = relative + nFilename;
        return true;
    }
}