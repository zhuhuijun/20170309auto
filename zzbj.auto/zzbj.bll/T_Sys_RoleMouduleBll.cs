//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2017-05-05 18:08:32 by ding
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using zzbj.ibll;
using zzbj.models;
using zzbj.repository;

namespace zzbj.bll
{
	/// <summary>
	/// IT_Sys_RoleMouduleBll
	/// </summary>	
	public  class  T_Sys_RoleMouduleBll:IT_Sys_RoleMouduleBll
    {
	    private readonly IRepository<T_Sys_RoleMoudule> _repository;
		
        public T_Sys_RoleMouduleBll(IRepository<T_Sys_RoleMoudule> repository)
        {
            _repository = repository;
        }

        public bool Insert(T_Sys_RoleMoudule p)
        {
            return _repository.Insert(p);
        }

        public bool Update(T_Sys_RoleMoudule p)
        {
           return  _repository.Update(p);
        }

        public bool Delete(T_Sys_RoleMoudule p)
        {
           return _repository.Delete(p);
        }
        /// <summary>
        /// 按照主键获得数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T_Sys_RoleMoudule FindSingleData(params object[] keyValues)
        {
            return _repository.FindSingleData(keyValues);
        }
		/// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<T_Sys_RoleMoudule> GetData(Expression<Func<T_Sys_RoleMoudule, bool>> propertyExpr=null)
		{
			return _repository.GetData(propertyExpr);
		}
		/// <summary>2.根据条件查询，返回数据
        /// </summary>
        /// <param name="queryParas">查询条件</param>
        /// <param name="propertyExpr">字段的属性</param>
        /// <returns>返回数据列表</returns>
		public IList<T_Sys_RoleMoudule> FindDataByCondition(List<CommonSearchModel> queryParas,
            Expression<Func<T_Sys_RoleMoudule, bool>> propertyExpr = null)
		{
			return _repository.FindDataByCondition(queryParas,propertyExpr);
		}
		/// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="entityQueryable"></param>
		public int ResultDataCount(IQueryable<T_Sys_RoleMoudule> entityQueryable)
		{
			return _repository.ResultDataCount(entityQueryable);
		}
		/// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        public int ResultDataCount_New(List<CommonSearchModel> queryParas,
            Expression<Func<T_Sys_RoleMoudule, bool>> propertyExpr = null)
		{
			return _repository.ResultDataCount_New(queryParas,propertyExpr);
		}
		/// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="queryParas">查询参数</param>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
		 public IQueryable<T_Sys_RoleMoudule> FindDataByPageFilter(List<CommonSearchModel> queryParas, string sortKey, int pageNumber, int pageSize, Expression<Func<T_Sys_RoleMoudule, bool>> propertyExpr = null)
		 {
			return _repository.FindDataByPageFilter(queryParas,sortKey,pageNumber,pageSize,propertyExpr);
		 }
		/// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
		 public IQueryable<T_Sys_RoleMoudule> FindDataByPageFilter(string sortKey, int pageNumber, int pageSize, IQueryable<T_Sys_RoleMoudule> entityList = null)
		 {
			return _repository.FindDataByPageFilter(sortKey,pageNumber,pageSize,entityList);
		 }
    }
}

