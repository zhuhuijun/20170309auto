using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzbj.models;

namespace zzbj.ibll
{
    public  partial  interface Irel_rolemenusBll
    {
        /// <summary>
        /// 获得角色的菜单
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        List<ZtreeMenuModel> GetZTreeDatas(string roleid);
        /// <summary>
        /// 保存角色和菜单的关联数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="menuids">菜单id</param>
        /// <returns></returns>
        bool SaveRoleMenu(string roleid, string menuids);
    }

}
