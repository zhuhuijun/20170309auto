using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using zzbj.ibll;
using zzbj.models;
using zzbj.repository;

namespace zzbj.bll
{
    public class customBll : IcustomBll
    {
        private readonly IRepository<custom> _repository;

        public customBll(IRepository<custom> repository)
        {
            _repository = repository;
        }

        public bool Insert(custom c)
        {
            return _repository.Insert(c);
        }

        public bool Update(custom c)
        {
            return _repository.Update(c);
        }

        public bool Delete(custom c)
        {
           return _repository.Delete(c);
        }
        /// <summary>
        /// 按照主键获得数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public custom FindSingleData(params object[] keyValues)
        {
            return _repository.FindSingleData(keyValues);
        }
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<custom> GetData(Expression<Func<custom, bool>> propertyExpr)
        {
            return _repository.GetData(propertyExpr);
        }
        /// <summary>2.根据条件查询，返回数据
        /// </summary>
        /// <param name="queryParas">查询条件</param>
        /// <param name="propertyExpr">字段的属性</param>
        /// <returns>返回数据列表</returns>
        public IList<custom> FindDataByCondition(List<CommonSearchModel> queryParas,
            Expression<Func<custom, bool>> propertyExpr = null)
        {
            return _repository.FindDataByCondition(queryParas, propertyExpr);
        }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="entityQueryable"></param>
        public int ResultDataCount(IQueryable<custom> entityQueryable)
        {
            return _repository.ResultDataCount(entityQueryable);
        }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        public int ResultDataCount_New(List<CommonSearchModel> queryParas,
            Expression<Func<custom, bool>> propertyExpr = null)
        {
            return _repository.ResultDataCount_New(queryParas, propertyExpr);
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="queryParas">查询参数</param>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
        public IQueryable<custom> FindDataByPageFilter(List<CommonSearchModel> queryParas, string sortKey, int pageNumber, int pageSize, Expression<Func<custom, bool>> propertyExpr = null)
        {
            return _repository.FindDataByPageFilter(queryParas, sortKey, pageNumber, pageSize, propertyExpr);
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
        public IQueryable<custom> FindDataByPageFilter(string sortKey, int pageNumber, int pageSize, IQueryable<custom> entityList = null)
        {
            return _repository.FindDataByPageFilter(sortKey, pageNumber, pageSize, entityList);
        }
    }
}
