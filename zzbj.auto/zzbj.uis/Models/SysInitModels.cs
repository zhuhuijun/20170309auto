using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using zzbj.bll;
using System.Linq.Dynamic;
using zzbj.models;

namespace zzbj.uis.Models
{
    public class SysInitModels
    {

        public static string GetMenuJsonStringResult()
        {
            //Json 数据
            StringBuilder JsonData = new StringBuilder();
            JsonData.Append("{menu:");
            //模块列表
            IList<T_Bas_Module> moduleBigList = SysDataHelper<T_Bas_Module>.GetData(M => M.ApplicationID == 1 && M.IsUse == 0);
            if (moduleBigList.Count > 0)
            {
                JavaScriptSerializer json = new JavaScriptSerializer();
                StringBuilder sb = new StringBuilder();
                if (moduleBigList != null)
                {
                    var menu = from module in moduleBigList
                               where module.ParentID == 0 && module.Path == 1
                               select new
                               {
                                   menuid = module.MouduleID,
                                   name = module.MouduleName,
                                   code = module.MouduleID,
                                   icon = module.IcoPath,
                                   type = module.MouduleType
                               };
                    var Parentmenu = menu.ToList();
                    int ParentCount = Parentmenu.Count;
                    json.Serialize(Parentmenu, sb);
                    JsonData.Append(sb.Append(",app:{"));
                    var childMenu = from chlid in moduleBigList
                                    where chlid.ParentID > 0 && chlid.Path == 2
                                    orderby chlid.OrderID ascending
                                    select new
                                    {
                                        appid = chlid.MouduleID,
                                        icon = chlid.IcoPath,
                                        name = chlid.MouduleName,
                                        url = chlid.MenuUrl,
                                        parentId = chlid.ParentID,
                                        sonMenu = getSonModule(moduleBigList, chlid.MouduleID),
                                        asc = chlid.OrderID,
                                        type = chlid.MouduleType
                                    };
                    sb.Clear();
                    var child = childMenu.ToList();
                    int Chidcount = child.Count;
                    json.Serialize(child, sb);
                    var result = sb.ToString().TrimStart('[').TrimEnd(']').Replace("},", "},*").TrimEnd(new char[] { ',', '*' }).Split('*');
                    sb.Clear();
                    if (Chidcount == result.Length)
                    {
                        for (int i = 0; i < Chidcount; i++)
                        {
                            string singeResult = string.Format("'{0}':{1}", child[i].appid, result[i].Replace('$', ','));
                            JsonData.Append(singeResult);
                        }
                        JsonData.Append("}}*");
                        var ss = JsonData.ToString();
                    }
                    sb.Append("ops = {");
                    bool isRemove = false;
                    for (int n = 0; n < ParentCount; n++)
                    {
                        sb.Append("Icon" + (n + 1) + ":[");
                        var parentId = Parentmenu[n].menuid;
                        for (int i = 0; i < Chidcount; i++)
                        {
                            if (parentId == child[i].parentId)
                            {
                                isRemove = true;
                                sb.Append(string.Format("'{0}',", child[i].appid));
                            }
                        }
                        if (isRemove)
                        {
                            sb.Remove(sb.Length - 1, 1);
                            isRemove = false;
                        }
                        sb.Append("],");
                    }
                    JsonData.Append(sb.ToString().TrimEnd(','));
                    JsonData.Append("}");
                    sb.Clear();
                }
            }
            return JsonData.ToString();
        }
        /// <summary>
        /// 构建子菜单
        /// </summary>
        /// <param name="mList">按条件过滤后的模块列表</param>
        /// <param name="parentId">父级ID</param>
        /// <returns></returns>
        static string getSonModule(IList<T_Bas_Module> mList, int parentId)
        {
            var resultList = mList.Where(m => m.ParentID == parentId).AsQueryable().OrderBy("OrderID asc").ToList();
            if (resultList != null && resultList.Count() > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("[");
                string result = string.Empty;
                foreach (var m in resultList)
                {
                    result += string.Format("{{'appid':'{0}','icon':'{1}','name':'{2}','url':'{3}','type':'{4}'}}$", m.MouduleID, m.IcoPath, m.MouduleName, m.MenuUrl, m.MouduleType);
                }
                result = result.TrimEnd('$');
                sb.Append(result);
                sb.Append("]");
                return sb.ToString();
            }
            else
            {
                return "[]";
            }
        }
    }
}