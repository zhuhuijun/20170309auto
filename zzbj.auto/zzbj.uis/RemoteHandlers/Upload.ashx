<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    /// <summary>
    /// Upload 的摘要说明
    /// </summary>
    public class Upload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
             //接收上传后的文件
            HttpPostedFile file = context.Request.Files["Filedata"];
            string code = context.Request["code"];
            string Iconkey = string.Empty;
            switch(code){
                case "1":
                     Iconkey = System.Configuration.ConfigurationManager.AppSettings["IconPath"];
                    break;
            }
            //获取文件的保存路径
            string uploadPath =
                HttpContext.Current.Server.MapPath(Iconkey + "\\");
            string uploadminPath = uploadPath + "min\\";
            
            //判断上传的文件是否为空
            if (file != null)
            {
                if (!System.IO.Directory.Exists(uploadPath))
                {
                    System.IO.Directory.CreateDirectory(uploadPath);
                }
                if (!System.IO.Directory.Exists(uploadminPath))
                {
                    System.IO.Directory.CreateDirectory(uploadminPath);
                }
                string upfile = uploadPath + file.FileName;
                if (!System.IO.File.Exists(upfile))
                {
                    //保存文件
                    file.SaveAs(uploadPath + file.FileName);
                    file.SaveAs(uploadminPath + file.FileName);
                }
                string iconurl =Iconkey +"/"+ file.FileName;
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
