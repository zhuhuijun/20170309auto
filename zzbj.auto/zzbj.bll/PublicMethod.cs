using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzbj.commons;
using zzbj.models;

namespace zzbj.bll
{
    public class PublicMethod
    {
        /// <summary>
        /// 获取所有大模块
        /// </summary>
        public static void LoadModule()
        {
            IList<T_Bas_Module> allModuleList = new List<T_Bas_Module>();
            //获取数据
            IList<T_Bas_Module> ModuleList = SysDataHelper<T_Bas_Module>.GetData(M => M.ParentID == 0 && M.ApplicationID == 1);
            foreach (var mParent in ModuleList)
            {
                allModuleList.Add(mParent);
                var childModules = SysDataHelper<T_Bas_Module>.GetData(cm => cm.ParentID == mParent.MouduleID && cm.Path == 2);
                foreach (var cm in childModules)
                {
                    cm.MouduleName = "   └" + cm.MouduleName;
                    allModuleList.Add(cm);
                }
            }
            //写入缓存
            DataCache.SetCache(ObjectCacheName.Module, allModuleList);
        }
        /// <summary>
        /// 获取所有角色
        /// </summary>
        public static void LoadRole()
        {
            //获取数据
            IList<sys_role> RoleList = SysDataHelper<sys_role>.GetData().ToList();
            //写入缓存
            DataCache.SetCache(ObjectCacheName.Role, RoleList);
        }




    }
}
