using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using zzbj.models;

namespace zzbj.idal
{
    public interface IDal<TEntity> where TEntity : class
    {
        dapper_testEntities GetDb();
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        /// <summary>
        /// 获得记录
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        TEntity FindSingleData(params object[] keyValues);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>返回所有数据</returns>
        IQueryable<TEntity> FindAllData();
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="entityQueryable"></param>
        int ResultDataCount(IQueryable<TEntity> entityQueryable);
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        int ResultDataCount_New(List<CommonSearchModel> queryParas, Expression<Func<TEntity, bool>> propertyExpr = null);
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <returns>返回符合条件数据列表</returns>
        IQueryable<TEntity> FindDataByCondition(List<CommonSearchModel> queryParas, Expression<Func<TEntity, bool>> propertyExpr = null);
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="queryParas">查询参数</param>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
        IQueryable<TEntity> FindDataByPageFilter(List<CommonSearchModel> queryParas, string sortKey, int pageNumber, int pageSize, Expression<Func<TEntity, bool>> propertyExpr = null);
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
        IQueryable<TEntity> FindDataByPageFilter(string sortKey, int pageNumber, int pageSize, IQueryable<TEntity> entityList = null);
    }
}
