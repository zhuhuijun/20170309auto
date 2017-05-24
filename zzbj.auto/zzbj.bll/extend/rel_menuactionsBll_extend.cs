using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzbj.models;

namespace zzbj.bll
{
    /// <summary>
    /// 关联表的扩展
    /// </summary>
    public partial class rel_menuactionsBll
    {
        /// <summary>
        /// 保存菜单和行为的关联
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="actionids"></param>
        /// <returns></returns>
        public bool SaveMenuAction(string menuid, List<string> actionids)
        {
            dapper_testEntities db = _repository.GetDb();
            using (var scope = db.Database.BeginTransaction())
            {
                try
                {
                    string sql = string.Format("delete from rel_menuactions where menuid='{0}'", menuid);
                    db.Database.ExecuteSqlCommand(sql);
                    if (actionids != null && actionids.Count > 0)
                    {
                        foreach (var actid in actionids)
                        {
                            rel_menuactions one = new rel_menuactions()
                            {
                                menuid = menuid,
                                actionid = actid,
                                isuse = true
                            };
                            db.Set<rel_menuactions>().Add(one);
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
    }
}
