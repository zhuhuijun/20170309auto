using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using zzbj.commons;
using zzbj.idal;
using zzbj.models;

namespace zzbj.dal
{
    public class Dal<T> : IDal<T> where T : class
    {
        private dapper_testEntities _mDbContext = new dapper_testEntities();
        /// <summary>
        /// 获得数据库的上下文
        /// </summary>
        /// <returns></returns>
        public dapper_testEntities GetDb()
        {
            return _mDbContext;
        }

        public bool Insert(T entity)
        {
            _mDbContext.Set<T>().Add(entity);
            return _mDbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity">需要更新的实例</param>
        /// <returns>更新成功返回True，否则返回false</returns>
        public bool Update(T entity)
        {
            if (_mDbContext.Entry(entity).State != EntityState.Modified)
            {
                _mDbContext.Entry(entity).State = EntityState.Modified;
            }
            return _mDbContext.SaveChanges() > 0;

        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="entity">要删除的数据</param>
        /// <returns>删除成功返回True，否则返回false</returns>
        public bool Delete(T entity)
        {
            try
            {
                _mDbContext.Set<T>().Remove(entity);
                if (_mDbContext.SaveChanges() > 0)
                    return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 修改指定字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        public bool UpdateSubFields(T entity, List<string> fileds)
        {
            if (entity != null && fileds != null)
            {
                _mDbContext.Set<T>().Attach(entity);
                var setEntry = ((IObjectContextAdapter)_mDbContext).ObjectContext.
                    ObjectStateManager.GetObjectStateEntry(entity);
                foreach (var t in fileds)
                {
                    setEntry.SetModifiedProperty(t);
                }
                return _mDbContext.SaveChanges() > 0;
            }
            return false;
        }

        /// <summary>
        /// 根据主键获得记录
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T FindSingleData(params object[] keyValues)
        {
            try
            {
                return _mDbContext.Set<T>().Find(keyValues);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <returns>返回所有数据</returns>
        public IQueryable<T> FindAllData()
        {
            try
            {
                return _mDbContext.Set<T>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="entityQueryable"></param>
        /// <returns></returns>
        public int ResultDataCount(IQueryable<T> entityQueryable)
        {
            try
            {
                if (object.Equals(null, entityQueryable))
                    return this.FindAllData().Count();
                else
                    return entityQueryable.Count();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        public int ResultDataCount_New(List<CommonSearchModel> queryParas, Expression<Func<T, bool>> propertyExpr = null)
        {
            return this.FindDataByCondition(queryParas, propertyExpr).Count();
        }

        /// <summary>2.根据条件查询，返回数据
        /// </summary>
        /// <param name="queryParas">查询条件</param>
        /// <param name="propertyExpr">字段的属性</param>
        /// <returns>返回数据列表</returns>
        public virtual IQueryable<T> FindDataByCondition(List<CommonSearchModel> queryParas, Expression<Func<T, bool>> propertyExpr = null)
        {
            try
            {
                string searchSqlStr = " 1=1 ";
                if (queryParas != null && queryParas.Count > 0)
                {
                    foreach (CommonSearchModel one in queryParas)
                    {
                        string nextkeyvalue = one.searchop;//成对的操作符号
                        string currentkey = one.paraname;
                        string currentvalue = one.paravalue;
                        nextkeyvalue = nextkeyvalue.ToLower();
                        switch (nextkeyvalue)
                        {
                            case "eq":
                                searchSqlStr += GetSqlSearch(nextkeyvalue, currentkey, currentvalue, "eq");
                                break;
                            case "gt":
                                searchSqlStr += GetSqlSearch(nextkeyvalue, currentkey, currentvalue, "gt");
                                break;
                            case "gte":
                                searchSqlStr += GetSqlSearch(nextkeyvalue, currentkey, currentvalue, "gte");
                                break;
                            case "lt":
                                searchSqlStr += GetSqlSearch(nextkeyvalue, currentkey, currentvalue, "lt");
                                break;
                            case "lte":
                                searchSqlStr += GetSqlSearch(nextkeyvalue, currentkey, currentvalue, "lte");
                                break;
                            case "contains":
                                if (!string.IsNullOrEmpty(currentvalue))
                                    searchSqlStr += (" and " + currentkey + ".Contains (\"" + currentvalue + "\")");
                                break;
                            case "in"://WRF扩展，使用请谨慎；
                                if (!string.IsNullOrEmpty(currentvalue))
                                {
                                    string[] valuess = currentvalue.Split(',');
                                    searchSqlStr += (" and (");
                                    for (int j = 0; j < valuess.Count(); j++)
                                    {
                                        searchSqlStr += currentkey + ".Contains (\"" + valuess[j] + "\") or ";
                                    }
                                    searchSqlStr = searchSqlStr.Substring(0, searchSqlStr.Length - 3) + ")";
                                }
                                break;
                            default:
                                T entity = Activator.CreateInstance<T>();
                                Type type = entity.GetType();
                                string dataType = "int";
                                foreach (PropertyInfo info in type.GetProperties())
                                {
                                    if (info.Name == one.paraname)
                                    {
                                        dataType = info.PropertyType.ToString().ToLower();
                                    }
                                }
                                if (dataType.IndexOf("int", StringComparison.Ordinal) > 0)
                                {
                                    if (!string.IsNullOrEmpty(one.paravalue))
                                    {
                                        if (int.Parse(one.paravalue) >= 0)
                                            searchSqlStr += (" and " + one.paraname + one.searchop + int.Parse(one.paravalue));
                                    }
                                }
                                break;
                        }
                    }
                }
                //searchSqlStr += (" and isdelete=false");
                if (propertyExpr != null)
                {
                    return FindAllData().Where(searchSqlStr).Where(propertyExpr);
                }
                else
                {
                    return FindAllData().Where(searchSqlStr);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>3.分页，返回数据列表
        /// </summary>
        /// <param name="queryParas">查询条件</param>
        /// <param name="sortKey">排序字段</param>
        /// <param name="pageNumber">页数</param>
        /// <param name="pageSize">显示条数</param>
        /// <param name="propertyExpr">字段的属性</param>
        /// <returns>返回数据列表</returns>
        public virtual IQueryable<T> FindDataByPageFilter(List<CommonSearchModel> queryParas, string sortKey, int pageNumber, int pageSize, Expression<Func<T, bool>> propertyExpr = null)
        {
            try
            {
                return FindDataByPageFilter(sortKey, pageNumber, pageSize, FindDataByCondition(queryParas, propertyExpr));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 分页，返回数据列表
        /// </summary>
        /// <param name="sortKey">排序字段</param>
        /// <param name="pageNumber">页数</param>
        /// <param name="pageSize">显示条数</param>
        /// <returns>返回数据列表</returns>
        public IQueryable<T> FindDataByPageFilter(string sortKey, int pageNumber, int pageSize, IQueryable<T> entityList = null)
        {
            try
            {
                if (object.Equals(null, entityList))
                    return this.FindAllData().OrderBy(sortKey).Skip((pageNumber - 1) * pageSize).Take(pageSize);
                else
                    return entityList.OrderBy(sortKey).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /**提供通用查询分页的接口输入参数为字典End****************************************/
        #region 用于时间查询的一些帮助方法
        /// <summary>
        /// 操作符的字典
        /// </summary>
        private static Dictionary<string, string> operationDic = new Dictionary<string, string>()
        {
            {"eq","="},
            {"gt",">"},//大于
            {"gte",">="},//大于等于
            {"lt","<"},//小于
            {"lte","<="}//小于等于
        };
        /// <summary>
        /// 获得操作的符号
        /// </summary>
        /// <param name="flag">标识</param>
        /// <returns>返回操作符号</returns>
        private string GetOperationFlag(string flag)
        {
            string end = "=";
            foreach (KeyValuePair<string, string> kv in operationDic)
            {
                if (kv.Key == flag)
                {
                    end = kv.Value;
                    break;
                }
            }
            return end;
        }
        /// <summary>
        /// 获得时间的分割
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>返回操作符号</returns>
        private string GetTimeSplit(string time)
        {
            DateTime dt = new DateTime(1970, 1, 1);
            DateTime.TryParse(time, out dt);
            StringBuilder sb = new StringBuilder(" DateTime( ");
            sb.AppendFormat("{0},{1},{2} ) ", dt.Year, dt.Month, dt.Day);
            return sb.ToString();
        }
        /// <summary>
        /// 获得时间的分割
        /// </summary>
        /// <param name="key">操作的字符串</param>
        /// <param name="op">操作符</param>
        /// <returns>返回操作符号</returns>
        private string GetKeyRemoveOp(string key, string op)
        {
            string end = key.Substring(0, key.Length - op.Length);
            return end;
        }

        /// <summary>获得拼接的字符串
        /// </summary>
        /// <param name="nextkeyvalue">nextkeyvalue</param>
        /// <param name="currentkey">currentkey</param>
        /// <param name="currentvalue">currentvalue</param>
        /// <param name="op">op</param>
        /// <returns></returns>
        private string GetSqlSearch(string nextkeyvalue, string currentkey, string currentvalue, string op)
        {
            if (!string.IsNullOrEmpty(currentvalue))
            {
                string opeq = GetOperationFlag(nextkeyvalue);
                string keyeq = GetKeyRemoveOp(currentkey, op);
                string opsql = GetTimeSplit(currentvalue);
                string searchSqlStr = " and " + keyeq + " " + opeq + opsql;
                return searchSqlStr;
            }
            else
            {
                return "";
            }
        }
        #endregion
    }
}