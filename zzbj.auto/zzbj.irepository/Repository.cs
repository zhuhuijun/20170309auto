using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using zzbj.commons;
using zzbj.idal;
using zzbj.models;

namespace zzbj.repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private IDal<T> _dal;
        public Repository(IDal<T> dal)
        {
            _dal = dal;
        }

        public bool Insert(T entity)
        {
            return _dal.Insert(entity);
        }

        public bool Update(T entity)
        {
            return _dal.Update(entity);
        }

        public bool Delete(T entity)
        {
            return _dal.Delete(entity);
        }
        /// <summary>
        /// 根据主键获得记录
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T FindSingleData(params object[] keyValues)
        {
            return _dal.FindSingleData(keyValues);
        }

        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<T> GetData(Expression<Func<T, bool>> propertyExpr)
        {
            try
            {
                return _dal.FindAllData().Where(propertyExpr).ToList();
            }
            catch (Exception ex)
            {
                Log4NetHepler.WriteLogToFile(ex);
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        /// <returns></returns>
        public IList<T> FindDataByCondition(List<CommonSearchModel> queryParas, Expression<Func<T, bool>> propertyExpr = null)
        {
            return _dal.FindDataByCondition(queryParas, propertyExpr).ToList();
        }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="entityQueryable"></param>
        public int ResultDataCount(IQueryable<T> entityQueryable)
        {
            return _dal.ResultDataCount(entityQueryable);
        }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        public int ResultDataCount_New(List<CommonSearchModel> queryParas, Expression<Func<T, bool>> propertyExpr = null)
        {
            return _dal.ResultDataCount_New(queryParas, propertyExpr);
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="queryParas">查询参数</param>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <param name="propertyExpr"></param>
        /// <returns>返回符合条件数据列表</returns>
        public IQueryable<T> FindDataByPageFilter(List<CommonSearchModel> queryParas, string sortKey, int pageNumber, int pageSize,
            Expression<Func<T, bool>> propertyExpr = default(Expression<Func<T, bool>>))
        {
            return _dal.FindDataByPageFilter(queryParas, sortKey, pageNumber, pageSize, propertyExpr);
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <param name="entityList"></param>
        /// <returns>返回符合条件数据列表</returns>
        public IQueryable<T> FindDataByPageFilter(string sortKey, int pageNumber, int pageSize,
            IQueryable<T> entityList = default(IQueryable<T>))
        {
            return _dal.FindDataByPageFilter(sortKey, pageNumber, pageSize, entityList);
        }
    }
}
