using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzbj.commons
{
    public class Log4NetHepler
    {
        /// <summary>
        /// 日志记录异常信息
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLogToFile(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("异常消息：{0}", ex.Message);
            sb.AppendFormat("\r\n异常名称：{0}", ex.Source);
            sb.AppendFormat("\r\n堆栈信息：{0}", ex.StackTrace);
            //sb.AppendFormat("\r\n详细信息：{0}", ex.InnerException.Message);
            CommonGlobal.Onlinelog.Error(sb.ToString());
        }
        /// <summary>
        /// 记录正常提示信息
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLogToFile(string info)
        {
            CommonGlobal.Onlinelog.Info(info);
        }
    }
}
