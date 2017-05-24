using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzbj.commons
{
    public class PublicCommon
    {
        /// <summary>
        /// 展示名称
        /// </summary>
        public string ShowName { get; set; }
        /// <summary>
        /// 选中值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool isChecked { get; set; }
        /// <summary>
        /// 数据库表字段名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// 所有集合数据
        /// </summary>
        private static Dictionary<string, IList<PublicCommon>> dicResult = new Dictionary<string, IList<PublicCommon>>();
        /// <summary>
        /// 返回所有
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, IList<PublicCommon>> GetPublicCommon()
        {
            if (dicResult.Keys.Count < 20)
            {
                fillAllDicList();
            }
            return dicResult;
        }
        /// <summary>
        /// 将数据插入到dicList
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        private static void SetDicList(IList<PublicCommon> list, string key, bool isRemove = true)
        {
            if (!dicResult.Keys.Contains(key))
            {
                var resultList = new List<PublicCommon>(list.ToArray());
                if (isRemove)
                    resultList.RemoveAt(0);
                dicResult.Add(key, resultList);
            }
        }
        /// <summary>
        /// 是否使用
        /// </summary>
        /// <returns></returns>
        public static IList<PublicCommon> GetIsUse()
        {
            IList<PublicCommon> IsUseList = new List<PublicCommon>() {
                new PublicCommon { ShowName="全部", Value = "-1", isChecked=true },
                new PublicCommon { ShowName="是", Value="0", isChecked=false} ,
                new PublicCommon { ShowName="否", Value="1", isChecked=false}
            };
            SetDicList(IsUseList, "IsUse", true);
            return IsUseList;
        }
        /// <summary>
        /// 是否有功能区
        /// </summary>
        /// <returns></returns>
        public static IList<PublicCommon> GetIsFuntion()
        {
            IList<PublicCommon> IsFuntionList = new List<PublicCommon>() {
                new PublicCommon { ShowName="有", Value="0", isChecked=true} ,
                new PublicCommon { ShowName="无", Value="1", isChecked=false}
            };
            SetDicList(IsFuntionList, "IsFuntion", false);
            return IsFuntionList;
        }
        /// <summary>
        /// 模块类别
        /// </summary>
        /// <returns></returns>
        public static IList<PublicCommon> GetIsMouduleType()
        {
            IList<PublicCommon> IsMouduleType = new List<PublicCommon>() {
                new PublicCommon { ShowName = "基本功能", Value = "1", isChecked=true },
                new PublicCommon { ShowName="统计分析", Value="2", isChecked=false}
            };
            SetDicList(IsMouduleType, "IsMouduleType", false);
            return IsMouduleType;
        }
        /// <summary>
        /// 应用程序类别
        /// </summary>
        /// <returns></returns>
        public static IList<PublicCommon> GetApplicationID()
        {
            IList<PublicCommon> ApplicationIDList = new List<PublicCommon>() {
                new PublicCommon { ShowName="应用系统", Value="1", isChecked=true} ,
                new PublicCommon { ShowName="地理信息系统", Value="2", isChecked=false}
            };
            SetDicList(ApplicationIDList, "ApplicationID", false);
            return ApplicationIDList;
        }
        /// <summary>
        /// 填充所有数据
        /// </summary>
        public static void fillAllDicList()
        {
            GetIsUse();
            GetIsFuntion();
            GetIsMouduleType();
            GetApplicationID();
        }
    }
}
