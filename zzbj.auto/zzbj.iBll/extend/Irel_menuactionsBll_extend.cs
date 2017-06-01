using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzbj.models;

namespace zzbj.ibll
{
    /// <summary>
    /// 关系表的扩展方法
    /// </summary>
    public partial interface Irel_menuactionsBll
    {
        /// <summary>
        /// 保存菜单和行为的关联
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="actionids"></param>
        /// <returns></returns>
        bool SaveMenuAction(string menuid, List<string> actionids);
        /// <summary>
        /// 根据角色获得菜单
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        List<rel_rolemenus> GetControllerAndActions(string roleid);
    }
}
