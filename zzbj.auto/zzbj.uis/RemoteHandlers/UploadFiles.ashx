<%@ WebHandler Language="C#" Class="UploadFiles" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    /// <summary>
    /// UploadFiles 的摘要说明
    /// </summary>
public class UploadFiles : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        //接收上传后的文件
        HttpPostedFile file = context.Request.Files["Filedata"];
        string code = context.Request["code"];
        string Iconkey = string.Empty;
        Iconkey = System.Configuration.ConfigurationManager.AppSettings["TemporaryFilesPath"];
        //获取文件的保存路径
        string uploadPath =
            HttpContext.Current.Server.MapPath(Iconkey + "\\");
        //判断上传的文件是否为空
        if (file != null)
        {
            if (!System.IO.Directory.Exists(uploadPath))
            {
                System.IO.Directory.CreateDirectory(uploadPath);
            }
            else
            {
                string[] strFiles = System.IO.Directory.GetFiles(uploadPath);
                foreach (string strfile in strFiles)
                    System.IO.File.Delete(strfile);
            }
            string upfile = uploadPath + file.FileName;
            if (!System.IO.File.Exists(upfile))
            {
                //保存文件
                file.SaveAs(uploadPath + file.FileName);
            }
            string iconurl = Iconkey + "/" + file.FileName;
            context.Response.Write(iconurl);
        }
        else
        {
            context.Response.Write("0");
        }
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}