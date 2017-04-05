<%@ WebHandler Language="C#" Class="ValidationCode" %>

using System;
using System.Web;
using System.Drawing;
using zzbj.commons;

public class ValidationCode : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        BaseHelper.NoCache();
        string code = GetRandCode(4);
        BaseHelper.CreateCookie("ValidationCode", code, 0);
        CreateImage(code,context);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    private string GetRandCode(int num)
    {
        string[] code = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        string vNum = "";
        Random ran = new Random();
        int iNum = 0;
        for (int i = 1; i <= num; i++)
        {
            iNum = ran.Next(0, code.Length);//这句效率更高
            vNum += code[iNum];
        }
        return vNum;
    }
    private void CreateImage(string checkCode, HttpContext context)
    {
        int iwidth = (int)(checkCode.Length * 15);
        System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 23);
        Graphics g = Graphics.FromImage(image);
        g.Clear(Color.White);
        //定义颜色
        Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        //定义字体            
        string[] font = { "Verdana", "Helvetica", "Comic Sans MS", "Arial", "sans-serif" };
        Random rand = new Random();
        //随机输出噪点
        for (int i = 0; i < 50; i++)
        {
            int x = rand.Next(image.Width);
            int y = rand.Next(image.Height);
            g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
        }

        //for (int i = 0; i < 3; i++)
        //{
        //    int x1 = rand.Next(image.Width);
        //    int y1 = rand.Next(image.Height);
        //    int x2 = rand.Next(image.Width);
        //    int y2 = rand.Next(image.Height);
        //    g.DrawLine(new Pen(Color.FromArgb(5, 32, 44), 1), x1, y1, x2, y2);
        //}

        //输出不同字体和颜色的验证码字符
        for (int i = 0; i < checkCode.Length; i++)
        {
            int cindex = rand.Next(7);
            int findex = rand.Next(5);

            Font f = new System.Drawing.Font(font[findex], 12, System.Drawing.FontStyle.Regular);
            Brush b = new System.Drawing.SolidBrush(Color.FromArgb(5, 32, 44));
            int ii = 4;
            if ((i + 1) % 1 == 0)
            {
                ii = 2;
            }
            g.DrawString(checkCode.Substring(i, 1), f, b, 3 + (i * 12), ii - 1);
        }

        for (int j = 0; j < 100; j++)
        {
            int x = rand.Next(image.Width);
            int y = rand.Next(image.Height);
            image.SetPixel(x, y, Color.FromArgb(rand.Next()));
        }

        //画一个边框
        g.DrawRectangle(new Pen(Color.White, 0), 0, 0, image.Width - 1, image.Height - 1);

        image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        context.Response.ContentType = "image/Jpeg";
        g.Dispose();
        image.Dispose();
    }
}