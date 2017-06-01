using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using zzbj.models;

namespace zzbj.repository
{
    public interface IRepository<Tentity> where Tentity : class
    {
        /// <summary>
        /// 获得数据库的上下文
        /// </summary>
        /// <returns></returns>
        dapper_testEntities GetDb();
        bool Insert(Tentity entity);
        bool Update(Tentity entity);
        /// <summary>
        /// 修改指定的字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
        bool UpdateSubFields(Tentity entity, List<string> fileds);
        bool Delete(Tentity entity);
        /// <summary>
        /// 获得记录
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        Tentity FindSingleData(params object[] keyValues);
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        IList<Tentity> GetData(Expression<Func<Tentity, bool>> propertyExpr);
        /// <summary>2.根据条件查询，返回数据
        /// </summary>
        /// <param name="queryParas">查询条件</param>
        /// <param name="propertyExpr">字段的属性</param>
        /// <returns>返回数据列表</returns>
        IList<Tentity> FindDataByCondition(List<CommonSearchModel> queryParas,
            Expression<Func<Tentity, bool>> propertyExpr = null);
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="entityQueryable"></param>
        int ResultDataCount(IQueryable<Tentity> entityQueryable);

        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        int ResultDataCount_New(List<CommonSearchModel> queryParas, Expression<Func<Tentity, bool>> propertyExpr = null);
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="queryParas">查询参数</param>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
        IQueryable<Tentity> FindDataByPageFilter(List<CommonSearchModel> queryParas, string sortKey, int pageNumber, int pageSize, Expression<Func<Tentity, bool>> propertyExpr = null);
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
        IQueryable<Tentity> FindDataByPageFilter(string sortKey, int pageNumber, int pageSize, IQueryable<Tentity> entityList = null);
    }
}
