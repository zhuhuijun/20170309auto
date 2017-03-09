using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace zzbj.commons
{
    /// <summary>全局
    /// </summary>
    public class CommonGlobal
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static ILog onlinelog;
        public static ILog Onlinelog
        {
            get
            {
                if (onlinelog == null)
                    return onlinelog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                return onlinelog;
            }
        }
    }
}
