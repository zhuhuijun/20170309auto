using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzbj.models;

namespace zzbj.bll
{
    /// <summary>
    /// 角色和菜单的关联
    /// </summary>
    public partial class rel_rolemenusBll
    {
        /// <summary>
        /// 获得角色的菜单
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        public List<ZtreeMenuModel> GetZTreeDatas(string roleid)
        {
            List<ZtreeMenuModel> res = new List<ZtreeMenuModel>();
            List<T_Bas_Module> menus = SysDataHelper<T_Bas_Module>.GetData().Where(g => g.IsUse == 0).ToList();
            //1.获得1级节点将节点数据的isparent置为true,其它为false
            List<T_Bas_Module> firsts = menus.Where(g => g.Path == 1 && g.ApplicationID == 1 && g.ParentID == 0).ToList();
            if (firsts.Count > 0)
            {
                GetFirstData(firsts, ref res);
                //2.获得所有无子级节点的菜单
                List<T_Bas_Module> nochilds = menus.Where(one => !string.IsNullOrEmpty(one.MenuUrl) && one.MenuUrl.ToLower() != "null" && one.MenuUrl.ToUpper() != "NULL").ToList();
                GetNoChildData(nochilds, ref res);
                //3.为无子级的元素拼接行为数据
                List<T_Bas_Module> nofirst = menus.Except(firsts).ToList();
                //4.获得中间节点
                List<T_Bas_Module> middList = nofirst.Except(nochilds).ToList();
                if (middList.Count > 0)
                {
                    GetMiddleData(middList, ref res);
                }
                //获得角色的所有菜单
               
                List<rel_rolemenus> rolemenus = SysDataHelper<rel_rolemenus>.GetData().Where(g => g.roleid == roleid).ToList();
                //循环遍历原有的数据为其选中的字段赋值
                foreach (var tt in res)
                {
                    if (rolemenus.Count(g => g.menuid == tt.id) > 0)
                    {
                        tt.ischeck = true;
                    }
                }
              
            }
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="menuids"></param>
        /// <returns></returns>
        public bool SaveRoleMenu(string roleid, string menuids)
        {
            List<rel_rolemenus> datas = null;
            bool flag = false;
            menuids = menuids.Remove(menuids.Length - 1, 1);
            string[] menuarr = menuids.Split(',');
            if (menuarr.Any())
            {
                datas = new List<rel_rolemenus>();
                for (int i = 0; i < menuarr.Count(); i++)
                {
                    rel_rolemenus tmp = new rel_rolemenus()
                    {
                        roleid = roleid,
                        menuid = menuarr[i],
                        createdate = DateTime.Now

                    };
                    datas.Add(tmp);
                }
                flag = SaveRoleMenu_Db(roleid, datas);
            }
            return flag;
        }
        /// <summary>
        /// 数据库保存角色的菜单
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        private bool SaveRoleMenu_Db(string roleid, List<rel_rolemenus> datas)
        {
            dapper_testEntities db = _repository.GetDb();
            using (var scope = db.Database.BeginTransaction())
            {
                try
                {
                    string sql = string.Format("delete from rel_rolemenus where roleid='{0}'", roleid);
                    db.Database.ExecuteSqlCommand(sql);
                    if (datas != null && datas.Count > 0)
                    {
                        foreach (var one in datas)
                        {
                            db.Set<rel_rolemenus>().Add(one);
                        }
                    }
                    db.SaveChanges();
                    scope.Commit();//正常完成就可以提交
                    return true;
                }
                catch (Exception ex)
                {
                    scope.Rollback();//发生异常就回滚
                    return false;
                }
            }
        }
        /// <summary>
        /// 获得顶级的菜单的树形数据
        /// </summary>
        /// <param name="firsts"></param>
        /// <param name="trees">结果树</param>
        /// <returns></returns>
        private static void GetFirstData(List<T_Bas_Module> firsts, ref List<ZtreeMenuModel> trees)
        {
            trees.AddRange(firsts.Select(tmp => new ZtreeMenuModel()
            {
                id = tmp.MouduleID.ToString(),
                pid = tmp.ParentID.ToString(),
                isparent = true,
                ischeck = false,
                name = tmp.MouduleName
            }));
        }

        /// <summary>
        /// 中间元素拼接树形数据
        /// </summary>
        /// <param name="firsts"></param>
        /// <param name="trees"></param>
        private static void GetMiddleData(List<T_Bas_Module> firsts, ref List<ZtreeMenuModel> trees)
        {
            trees.AddRange(firsts.Select(tmp => new ZtreeMenuModel()
            {
                id = tmp.MouduleID.ToString(),
                pid = tmp.ParentID.ToString(),
                isparent = false,
                ischeck = false,
                name = tmp.MouduleName
            }));
        }

        /// <summary>
        /// 为无子级元素添加节点数据
        /// </summary>
        /// <param name="firsts"></param>
        /// <param name="trees"></param>
        private static void GetNoChildData(List<T_Bas_Module> firsts, ref List<ZtreeMenuModel> trees)
        {
            List<sys_action> actions = SysDataHelper<sys_action>.GetData().ToList();
            List<rel_menuactions> menuactions = SysDataHelper<rel_menuactions>.GetData().ToList();
            foreach (T_Bas_Module tmp in firsts)
            {
                ZtreeMenuModel one = new ZtreeMenuModel()
                {
                    id = tmp.MouduleID.ToString(),
                    pid = tmp.ParentID.ToString(),
                    isparent = false,
                    ischeck = false,
                    name = tmp.MouduleName
                };
                trees.Add(one);
                //添加action菜单
                List<rel_menuactions> myactions = menuactions.Where(t => t.menuid == tmp.MouduleID.ToString()).ToList();
                foreach (rel_menuactions mya in myactions)
                {
                    sys_action action1 = actions.FirstOrDefault(f => f.id == mya.actionid);
                    if (action1 != null)
                    {
                        ZtreeMenuModel one2 = new ZtreeMenuModel()
                        {
                            id = tmp.MouduleID + "*" + mya.actionid,
                            pid = tmp.MouduleID.ToString(),
                            isparent = false,
                            ischeck = false,
                            name = action1.actionname
                        };
                        trees.Add(one2);
                    }
                }
            }
        }
    }
}
