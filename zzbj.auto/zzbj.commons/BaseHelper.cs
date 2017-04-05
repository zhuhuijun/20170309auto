/************************************************************************
     Copyright (C) 2012 Mapuni.com,All Rights Reserved				
     文    件： BaseHelper                           		
     描    述： 公用函数助手1     					
     创 建 者： yinxw
     创建时间： 2012-11-20
     来    源：
     修改记录：														
  ************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing;
using System.Web;
using System.Security.Cryptography;
using System.Net;
using System.Drawing.Imaging;
using System.Xml;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI;

namespace zzbj.commons
{



    /// <summary>
    /// JavaScript信息输入
    /// </summary>
    public class MessageBox
    {
        public MessageBox()
        {
        }
        /// <summary>
        /// 页面地步注册Alert脚本
        /// </summary>
        /// <param name="page"></param>
        /// <param name="message"></param>
        public static string Show(string message)
        {
            return string.Format("<p><span class=\"ui-icon ui-icon-alert\" style=\"float: left;\"></span>{0}</p>", message);
        }
        public static void Show(Page page, string message, bool blockState)
        {
            string blockScript = string.Empty;
            if (blockState)
            {
                blockScript = "$.blockUI({ css: { border: 'none', padding: '15px', backgroundColor: '#000', '-webkit-border-radius': '10px', '-moz-border-radius': '10px', opacity: .5, color: '#fff'} });";
            }
            else
            {
                blockScript = "$.unblockUI();";
            }
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", string.Format("<script>{1} alert('{0}');</script>", message.Replace("'", "\\'").Replace("\n", "").Replace("\r", ""), blockScript));

        }
        /// <summary>
        /// 页面输出错误信息
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.Write("<html><head><title>警告：</title></head>" + message + "<li><a href=\"javascript:history.go(-1);\">返回上一页</a></li></body></html>");


            System.Web.HttpContext.Current.Response.End();
        }
    }

    /// <summary>
    /// RSA加解密
    /// </summary>
    public class RSACryption
    {
        public RSACryption()
        {
        }
        #region RSA 加密解密

        #region RSA 的密钥产生

        /// <summary>
        /// RSA 的密钥产生 产生私钥 和公钥 
        /// </summary>
        /// <param name="xmlKeys"></param>
        /// <param name="xmlPublicKey"></param>
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            xmlKeys = rsa.ToXmlString(true);
            xmlPublicKey = rsa.ToXmlString(false);
        }
        #endregion

        #region RSA的加密函数
        //############################################################################## 
        //RSA 方式加密 
        //说明KEY必须是XML的行式,返回的是字符串 
        //在有一点需要说明！！该加密方式有 长度 限制的！！ 
        //############################################################################## 

        //RSA的加密函数  string
        public string RSAEncrypt(string xmlPublicKey, string m_strEncryptString)
        {

            byte[] PlainTextBArray;
            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            PlainTextBArray = (new UnicodeEncoding()).GetBytes(m_strEncryptString);
            CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;

        }
        //RSA的加密函数 byte[]
        public string RSAEncrypt(string xmlPublicKey, byte[] EncryptString)
        {

            byte[] CypherTextBArray;
            string Result;
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPublicKey);
            CypherTextBArray = rsa.Encrypt(EncryptString, false);
            Result = Convert.ToBase64String(CypherTextBArray);
            return Result;

        }
        #endregion

        #region RSA的解密函数
        //RSA的解密函数  string
        public string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            byte[] PlainTextBArray;
            byte[] DypherTextBArray;
            string Result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            PlainTextBArray = Convert.FromBase64String(m_strDecryptString);
            DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;

        }

        //RSA的解密函数  byte
        public string RSADecrypt(string xmlPrivateKey, byte[] DecryptString)
        {
            byte[] DypherTextBArray;
            string Result;
            System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(xmlPrivateKey);
            DypherTextBArray = rsa.Decrypt(DecryptString, false);
            Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
            return Result;

        }
        #endregion

        #endregion

        #region RSA数字签名

        #region 获取Hash描述表
        //获取Hash描述表 
        public bool GetHash(string m_strSource, ref byte[] HashData)
        {
            //从字符串中取得Hash描述 
            byte[] Buffer;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);

            return true;
        }

        //获取Hash描述表 
        public bool GetHash(string m_strSource, ref string strHashData)
        {

            //从字符串中取得Hash描述 
            byte[] Buffer;
            byte[] HashData;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(m_strSource);
            HashData = MD5.ComputeHash(Buffer);

            strHashData = Convert.ToBase64String(HashData);
            return true;

        }

        //获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {

            //从文件中取得Hash描述 
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();

            return true;

        }

        //获取Hash描述表 
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {

            //从文件中取得Hash描述 
            byte[] HashData;
            System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
            HashData = MD5.ComputeHash(objFile);
            objFile.Close();

            strHashData = Convert.ToBase64String(HashData);

            return true;

        }
        #endregion

        #region RSA签名
        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;

        }

        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, byte[] HashbyteSignature, ref string m_strEncryptedSignatureData)
        {

            byte[] EncryptedSignatureData;

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;

        }

        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {

            byte[] HashbyteSignature;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            return true;

        }

        //RSA签名 
        public bool SignatureFormatter(string p_strKeyPrivate, string m_strHashbyteSignature, ref string m_strEncryptedSignatureData)
        {

            byte[] HashbyteSignature;
            byte[] EncryptedSignatureData;

            HashbyteSignature = Convert.FromBase64String(m_strHashbyteSignature);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPrivate);
            System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
            //设置签名的算法为MD5 
            RSAFormatter.SetHashAlgorithm("MD5");
            //执行签名 
            EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

            m_strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);

            return true;

        }
        #endregion

        #region RSA 签名验证

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, byte[] DeformatterData)
        {

            byte[] HashbyteDeformatter;

            HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string p_strKeyPublic, byte[] HashbyteDeformatter, string p_strDeformatterData)
        {

            byte[] DeformatterData;

            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            DeformatterData = Convert.FromBase64String(p_strDeformatterData);

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool SignatureDeformatter(string p_strKeyPublic, string p_strHashbyteDeformatter, string p_strDeformatterData)
        {

            byte[] DeformatterData;
            byte[] HashbyteDeformatter;

            HashbyteDeformatter = Convert.FromBase64String(p_strHashbyteDeformatter);
            System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

            RSA.FromXmlString(p_strKeyPublic);
            System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
            //指定解密的时候HASH算法为MD5 
            RSADeformatter.SetHashAlgorithm("MD5");

            DeformatterData = Convert.FromBase64String(p_strDeformatterData);

            if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        #endregion


        #endregion

    }

    /// <summary>
    /// 常用函数助手
    /// </summary>
    public class BaseHelper
    {
        public BaseHelper()
        {
        }
        /// <summary>
        /// 对象克隆
        /// </summary>
        /// <param name="ob"></param>
        /// <returns></returns>
        public static object Clone(object ob)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, ob);
                ms.Seek(0, SeekOrigin.Begin);
                return bf.Deserialize(ms);
            }
        }
        public static int IntParse(object s)
        {
            try
            {
                return Convert.ToInt32(s);
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// MD5加密函数
        /// </summary>
        /// <param name="InputString"></param>
        /// <returns></returns>
        public static string MD5Encryption(string InputString)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            string OutputString = BitConverter.ToString(md5.ComputeHash(System.Text.UTF8Encoding.Default.GetBytes(InputString)));
            OutputString = OutputString.Replace("-", "");
            OutputString = OutputString.ToLower();
            OutputString = OutputString.Substring(8, 16);

            return OutputString;
        }
        public static string RemoveLastOne(string inputString)
        {
            if (inputString.Length > 0)
            {
                return inputString.Substring(0, inputString.Length - 1);
            }
            else
            {
                return "";
            }
        }
        /// <summary>
        /// 方法：格式化日期
        /// </summary>
        /// <param name="Datetime">String型日期时间</param>
        /// <param name="yyMMdd">格式</param>
        /// <returns>格式化后字符串</returns>
        public static string FormatDate(string Datetime, string yyMMdd)
        {
            return Convert.ToDateTime(Datetime).ToString(yyMMdd, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 方法：格式化日期
        /// </summary>
        /// <param name="Datetime">DateTime型日期时间</param>
        /// <param name="yyMMdd">格式</param>
        /// <returns>格式化后字符串</returns>
        public static string FormatDate(DateTime Datetime, string yyMMdd)
        {
            return Datetime.ToString(yyMMdd, System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        /// <summary>
        /// 方法：转换汉字为UrlEncode编码
        /// </summary>
        /// <param name="InputString">输入支符串</param>
        /// <returns>输出UrlEncode格式字符串</returns>
        public static string ToUrlEncode(string InputString)
        {
            ASCIIEncoding asscii = new ASCIIEncoding();
            string OutPutString = "";
            byte[] s = asscii.GetBytes(InputString);
            for (int i = 0; i < s.Length; i++)
            {
                try
                {
                    if ((int)s[i] == 63)
                    {
                        OutPutString += System.Web.HttpUtility.UrlEncode(InputString.Substring(i, 1), System.Text.Encoding.GetEncoding(936));
                    }
                    else
                    {
                        OutPutString += OutPutString.Substring(i, 1);
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
            OutPutString = OutPutString.Replace("%3f", "?");
            OutPutString = OutPutString.Replace("%3F", "?");
            return OutPutString;
        }

        /// <summary>
        /// 方法：按字节截取字符串（汉字为2字节）
        /// </summary>
        /// <param name="inputString">输入字符串</param>
        /// <param name="length">字节长度</param>
        /// <returns>返回字符串</returns>
        public static string CutString(string inputString, int length)
        {
            if (inputString.Length > 0)
            {
                System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
                int tempLength = 0;
                string tempString = "";
                byte[] s = ascii.GetBytes(inputString);
                for (int i = 0; i < s.Length; i++)
                {
                    if ((int)s[i] == 63)
                    {
                        tempLength += 2;
                    }
                    else
                    {
                        tempLength += 1;
                    }
                    try
                    {
                        tempString += inputString.Substring(i, 1);
                    }
                    catch
                    {
                        break;
                    }

                    if (tempLength >= length)
                        break;
                }
                //如果截过则加上..
                byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
                if (mybyte.Length > length)
                    tempString += "...";
                return tempString;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 方法：格式化字符串 ">"→"&gt";
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>输出字符串</returns>
        public static string HTMLEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                str = str.Replace("<", "&lt;");
                str = str.Replace(">", "&gt;");
                str = str.Replace("\\", "&#92;");
                str = str.Replace("--", "&#45;&#45;");
                str = str.Replace("\"", "&quot;");
                str = str.Replace("'", "&#39;");
                str = str.Replace(" ", "&nbsp;");
                return str;
            }
        }

        /// <summary>
        /// 方法：恢复字符串 html
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>输出html字符串</returns>
        public static string HTMLDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                str = str.Replace("<br>", "\r");
                str = str.Replace("&lt;", "<");
                str = str.Replace("&gt;", ">");
                str = str.Replace("&amp;", "&");
                str = str.Replace("&quot;", "\"");
                str = str.Replace("&#39;", "'");
                str = str.Replace("&nbsp;", " ");
                return str;
            }
        }

        /// <summary>
        /// 方法：正则表达式：替换。
        /// </summary>
        public static string ReplaceText(string Inputstr, string patrn, string replStr)
        {
            return System.Text.RegularExpressions.Regex.Replace(Inputstr, patrn, replStr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 方法：剔除所有HTML标签
        /// </summary>
        /// <param name="str">输入字符串</param>
        /// <returns>String型</returns>
        public static string ClearHtml(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            else
            {
                str = str.Replace("　", " ");
                str = str.Replace("\r", "");
                str = str.Replace("\n", "");
                str = ReplaceText(str, @"<script[^>]*?>.*?<\/>", "");
                str = ReplaceText(str, @"<style[^>]*?>.*?<\/styscriptle>", "");
                str = ReplaceText(str, @"<[\/\!]*?[^<>]*?>", "");
                //str = ReplaceText(str, @"(\<.[^\<]*\>)", "");
                //str = ReplaceText(str, @"(\<\/[^\<]*\>)", "");
                return str;
            }
        }

        /// <summary>
        /// 方法：剔除JavaScript脚本
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ClearScript(string val)
        {
            val = ReplaceText(val, @"<script[\s\S]+</script *>", "");
            val = ReplaceText(val, @" href *= *[\s\S]*script *:", "");
            val = ReplaceText(val, @" on[\s\S]*=", "");
            val = ReplaceText(val, @"<iframe[\s\S]+</iframe *>", "");
            val = ReplaceText(val, @"<frameset[\s\S]+</frameset *>", "");
            return val;
        }

        /// <summary>
        /// 方法：构建Cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="days">有效期（如果为0，则浏览器结束时删除）</param>
        public static void CreateCookie(string key, string value, double days)
        {
            System.Web.HttpContext.Current.Response.Cookies[key].Value = value;
            System.Web.HttpContext.Current.Response.Cookies[key].Path = "/";
            if (days > 0)
            {
                System.Web.HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddDays(days);
            }
        }
        public static void SetCookieState(string key, double days)
        {
            System.Web.HttpContext.Current.Response.Cookies[key].Expires = DateTime.Now.AddDays(days);
        }

        /// <summary>
        /// 方法：获得单个Cookie值。为空则返回"";
        /// </summary>
        public static string GetCookie(string key)
        {
            string s = "";
            if (System.Web.HttpContext.Current.Request.Cookies[key] != null)
            {
                s = System.Web.HttpContext.Current.Request.Cookies[key].Value;
            }
            return (s);
        }

        /// <summary>
        /// 方法：格式化HTML,超文本编辑器。
        /// </summary>
        public static string FormatHtmlContent(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                str = "";
            }
            else
            {
                str = str.Replace("\r", "");
                str = str.Replace("\n", "");
                str = str.Replace("'", "''");
                str = ReplaceText(str, @"<(.[^>]*)(javascript:|Document.|onerror|onload|onBlur|onClick|onDblClick|onFocus|onKeyDown|onKeyPress|onKeyUp|onMouseDown|onMouseMove|onMouseOut|onMouseOver|onMouseUp)", "&lt;$1$2");
                str = ReplaceText(str, @"<(\/|)(iframe|script|form|style|object|textarea)", "&lt;$1$2");
            }
            return str;
        }

        /// <summary>
        /// 方法：页面禁止缓存
        /// </summary>
        public static void NoCache()
        {
            System.Web.HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            System.Web.HttpContext.Current.Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
            System.Web.HttpContext.Current.Response.Expires = 0;
            System.Web.HttpContext.Current.Response.CacheControl = "no-cache";
            System.Web.HttpContext.Current.Response.Cache.SetNoStore();
        }

        /// <summary>
        /// 格式化html格式到TextArea
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Memo_ToTextarea(string input)
        {
            try
            {
                input = input.Replace("&nbsp;", " ");
                input = input.Replace("<br/>", "\n");
                return input;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 格式化TextArea到html
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Memo_ToDB(string input)
        {
            try
            {
                input = input.Replace(" ", "&nbsp;");
                input = input.Replace("\n", "<br/>");
                return input;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 把字节转换为十六进制的字符串表现形式
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Escape(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byteArr = System.Text.Encoding.Unicode.GetBytes(s);

            for (int i = 0; i < byteArr.Length; i += 2)
            {
                sb.Append("%u");
                sb.Append(byteArr[i + 1].ToString("X2"));//把字节转换为十六进制的字符串表现形式
                sb.Append(byteArr[i].ToString("X2"));
            }
            return sb.ToString();

        }

        /// <summary>
        /// 把字节转为unicode编码
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string UnEscape(string s)
        {

            string str = s.Remove(0, 2);//删除最前面两个＂%u＂
            string[] strArr = str.Split(new string[] { "%u" }, StringSplitOptions.None);//以子字符串＂%u＂分隔
            byte[] byteArr = new byte[strArr.Length * 2];
            for (int i = 0, j = 0; i < strArr.Length; i++, j += 2)
            {
                byteArr[j + 1] = Convert.ToByte(strArr[i].Substring(0, 2), 16); //把十六进制形式的字串符串转换为二进制字节
                byteArr[j] = Convert.ToByte(strArr[i].Substring(2, 2), 16);
            }
            str = System.Text.Encoding.Unicode.GetString(byteArr); //把字节转为unicode编码
            return str;

        }

        /// <summary>
        /// 返回配置参数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object GetSetting(string key)
        {
            return (object)System.Configuration.ConfigurationManager.AppSettings[key];
        }
        /// <summary>
        /// 返回当前星期
        /// </summary>
        /// <returns></returns>
        public static string WeekDay(DateTime dt)
        {
            int weeknow = Convert.ToInt32(dt.DayOfWeek);//今天星期几
            string weekday = string.Empty;
            switch (weeknow)
            {
                case 1:
                    weekday = "星期一";
                    break;
                case 2:
                    weekday = "星期二";
                    break;
                case 3:
                    weekday = "星期三";
                    break;
                case 4:
                    weekday = "星期四";
                    break;
                case 5:
                    weekday = "星期五";
                    break;
                case 6:
                    weekday = "星期六";
                    break;
                case 7:
                    weekday = "星期日";
                    break;
            }
            return weekday;
        }
        /// <summary>
        /// 返回当前第几周
        /// </summary>
        /// <returns></returns>
        public static int Weeks(DateTime dt)
        {
            int weeknow = Convert.ToInt32(dt.DayOfWeek);//今天星期几
            int daydiff = (-1) * (weeknow + 1);//今日与上周末的天数差 
            int days = System.DateTime.Now.AddDays(daydiff).DayOfYear;//上周末是本年第几天 　
            int weeks = days / 7;
            if (days % 7 != 0)
            {
                weeks++;
            }
            return weeks + 1;
        }
        public static string Quarter(DateTime dt)
        {
            string Quarter = string.Empty;
            int month = dt.Month;
            if (month == 1 || month == 2 || month == 3)
            {
                Quarter = "第一季度";
            }
            else if (month == 4 || month == 5 || month == 6)
            {
                Quarter = "第二季度";
            }
            else if (month == 7 || month == 8 || month == 9)
            {
                Quarter = "第三季度";
            }
            else if (month == 10 || month == 11 || month == 12)
            {
                Quarter = "第四季度";
            }
            return Quarter;
        }
        #region 字符串中数字提取
        /// <summary>
        /// 提取下标数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回string</returns>
        public static string CharSub(string str)
        {
            var charcode = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (Char.IsNumber(str, i) || str[i].ToString() == ".")
                    {
                        charcode += "<sub>" + str[i] + "</sub>";
                    }
                    else
                    {
                        charcode += str[i].ToString();
                    }
                }
            }
            return charcode;
        }
        /// <summary>
        /// 提取上标数字
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回string</returns>
        public static string CharSup(string str)
        {
            string charcode = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (Char.IsNumber(str, i) || str[i].ToString() == ".")
                    {
                        if (str[i - 1].ToString() == "/" || Char.IsNumber(str, i - 1))
                        {
                            charcode += str[i];
                        }
                        else
                        {
                            charcode += "<sup>" + str[i] + "</sup>";
                        }
                    }
                    else
                    {
                        charcode += str[i].ToString();
                    }
                }
            }
            return charcode;
        }
        /// <summary>
        /// 特殊字符过滤
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>返回string</returns>
        public static string CutChar(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("</", "");
                str = str.Replace(">", "");
                str = str.Replace("<", "");
                str = str.Replace("sub", "");
                str = str.Replace("sup", "");
                return str;
            }
            return string.Empty;
        }
        /// <summary>
        /// 获取年限
        /// </summary>
        /// <returns></returns>
        public static IList<object> YearList()
        {
            int MaxNum = DateTime.Now.AddYears(0).Year;
            IList<object> yearlist = new List<object>();
            for (int i = MaxNum; i >= 1990; i--)
            {
                yearlist.Add(i);
            }

            return yearlist;
        }
        /// <summary>
        /// 获得月份的数据
        /// </summary>
        /// <returns></returns>
        public static IList<object> MonthList()
        {
            int MaxNum = 12;
            IList<object> monthList = new List<object>();
            for (int i = MaxNum; i >= 1; i--)
            {
                monthList.Add(i);
            }
            return monthList;
        }
        #endregion

        /// <summary>
        /// 修改图片颜色并且保存
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="newPath"></param>
        /// <param name="newName"></param>
        /// <param name="colorValue"></param>
        /// <param name="oldPath"></param>
        public static void SavePhotoColor(string filePath, string newPath, string newName, Color colorValue, string oldPath)
        {
            //删除旧图片
            if (System.IO.File.Exists(oldPath))
                System.IO.File.Delete(oldPath);
            Bitmap bmp = new Bitmap(Bitmap.FromFile(filePath));
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    var a = bmp.GetPixel(j, i);
                    if (bmp.GetPixel(j, i) != Color.FromArgb(0, 0, 0, 0))
                    {
                        bmp.SetPixel(j, i, colorValue);
                    }
                }
            }
            if (!Directory.Exists(newPath))
                Directory.CreateDirectory(newPath);
            bmp.Save(newPath + newName);
        }
    }


    /// <summary>
    /// Txt文件读取助手
    /// </summary>
    public class FilterWords
    {
        public static FilterWords blackwords;
        private List<string> _KEYWORDS;

        public List<string> KeyWords
        {
            get { return _KEYWORDS; }
        }

        public FilterWords()
        {
        }
        public void AddWords(string path)
        {
            if (path != null && path != "" && path != string.Empty)
            {
                _KEYWORDS = new List<string>();
                string[] kes = File.ReadAllLines(path, Encoding.Default);
                foreach (string s in kes)
                {
                    _KEYWORDS.Add(s);
                }
            }
        }
        public static FilterWords GetFilterWords(string path)
        {
            if (blackwords == null)
            {
                blackwords = new FilterWords();
                blackwords.AddWords(path);
            }
            return blackwords;
        }
    }

    public class Xml
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">错误日志保存路径</param>
        /// <param name="path">错误日志文件名</param>
        /// <param name="Url">错误链接</param>
        /// <param name="unusual">异常信息</param>
        /// <param name="errorsource">错误源</param>
        /// <param name="StackTrace">堆栈信息</param>
        public static void WriterXml(string path, string filename, string Url, string unusual, string errorsource, string StackTrace)
        {
            //创建空对象
            XmlTextWriter writer = null;
            FileInfo TheFile = new FileInfo(path + "/" + filename);
            if (!TheFile.Exists)
            {
                //文件是否存在，存在就直接追加
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                //如果文件不存在，建立目录的同时创建XML文件
                writer = new XmlTextWriter(path + "/" + filename, Encoding.UTF8);
            }
            //判断XML文件是否已经存在，并且是否为空，为空直接创建，不为空则追加
            FileInfo fi = new FileInfo(path + "/" + filename);
            if (fi.Length == 0)
            {
                if (writer == null && fi.Length == 0)
                {
                    System.IO.File.Delete(path + "/" + filename);
                    writer = new XmlTextWriter(path + "/" + filename, Encoding.UTF8);
                }
                //自动缩进
                writer.Formatting = Formatting.Indented;

                // 开始写XML
                writer.WriteStartDocument();
                //根节点
                writer.WriteStartElement("ErrorLogs");

                // 二级及相关属性
                writer.WriteStartElement("baseinfo");
                writer.WriteElementString("Time", DateTime.Now.ToString());
                writer.WriteElementString("ErrorUrl", Url);
                writer.WriteElementString("Errorunusual", unusual);
                writer.WriteElementString("Errorsource", errorsource);
                writer.WriteElementString("ErrorStackTrace", StackTrace);
                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
            else
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(path + "/" + filename);
                XmlNode root = xmldoc.SelectSingleNode("ErrorLogs");
                XmlElement Xmt = xmldoc.CreateElement("baseinfo");

                XmlElement XmntTime = xmldoc.CreateElement("Time");
                XmntTime.InnerText = DateTime.Now.ToString();

                XmlElement XmntUrl = xmldoc.CreateElement("ErrorUrl");
                XmntUrl.InnerText = Url;

                XmlElement Xmntunusual = xmldoc.CreateElement("Errorunusual");
                Xmntunusual.InnerText = unusual;

                XmlElement Xmnterrorsource = xmldoc.CreateElement("Errorsource");
                Xmnterrorsource.InnerText = errorsource;

                XmlElement XmntStackTrace = xmldoc.CreateElement("ErrorStackTrace");
                XmntStackTrace.InnerText = StackTrace;

                Xmt.AppendChild(XmntTime);
                Xmt.AppendChild(XmntUrl);
                Xmt.AppendChild(Xmntunusual);
                Xmt.AppendChild(Xmnterrorsource);
                Xmt.AppendChild(XmntStackTrace);
                root.AppendChild(Xmt);
                xmldoc.Save(path + "/" + filename);
            }
        }
    }

    /// <summary>
    /// 静态页
    /// </summary>
    public class StaticPages
    {
        protected int contentLength;
        protected int buffLength = 1024;
        /// <summary>
        /// 模板路径
        /// </summary>
        public string TemplatePath
        {
            get;
            set;
        }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string TemplateName
        {
            get;
            set;
        }

        /// <summary>
        /// 静态页保存路径
        /// </summary>
        public string StaticPagePath
        {
            get;
            set;
        }

        /// <summary>
        /// 静态页名称
        /// </summary>
        public string StaticPageName
        {
            get;
            set;
        }

        public IDictionary<string, string> ReplaceContent
        {
            get;
            set;
        }
        /// <summary>
        /// 静态页内容
        /// </summary>
        private string HtmlContent;

        private MemoryStream ContentStream;

        private string TemplateContent;

        private StreamReader reader;
        private StreamWriter writer;
        /// <summary>
        /// 读取模板内容
        /// </summary>
        public void LoadTemplate()
        {
            reader = new StreamReader(TemplatePath + TemplateName);
            TemplateContent = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
        }
        /// <summary>
        /// 内容替换
        /// </summary>
        public void Replace()
        {
            HtmlContent = TemplateContent;
            foreach (var s in ReplaceContent)
            {
                HtmlContent = HtmlContent.Replace(s.Key, s.Value);
            }
        }

        /// <summary>
        /// 保存静态页
        /// </summary>
        public bool Save()
        {

            byte[] buffer = new byte[buffLength];
            try
            {
                BuilerContentStream();
                if (!System.IO.Directory.Exists(StaticPagePath))
                {
                    System.IO.Directory.CreateDirectory(StaticPagePath);
                }
                ContentStream.Seek(0, SeekOrigin.Begin);

                contentLength = ContentStream.Read(buffer, 0, buffLength);     //将流的内容读到缓冲区 
                FileStream fileStream = new FileStream(StaticPagePath + StaticPageName, FileMode.Create, FileAccess.Write);
                while (contentLength != 0)
                {
                    fileStream.Write(buffer, 0, contentLength);
                    contentLength = ContentStream.Read(buffer, 0, buffLength);
                }
                Dispose(ContentStream);
                writer.Close();
                writer.Dispose();
                Dispose(fileStream);

                return true;

            }
            catch
            {
                return false;
            }


        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="stream"></param>
        public void Dispose(Stream stream)
        {
            stream.Flush();
            stream.Close();
            stream.Dispose();
        }

        /// <summary>
        /// 构建静态页文件流
        /// </summary>
        private void BuilerContentStream()
        {
            ContentStream = new MemoryStream();
            writer = new StreamWriter(ContentStream, Encoding.UTF8);
            writer.Write(HtmlContent);

            writer.Flush();
        }


    }
}


