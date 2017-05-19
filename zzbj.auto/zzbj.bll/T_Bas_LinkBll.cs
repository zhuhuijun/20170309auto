//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2017-05-19 09:39:00 by zhuhj
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------
/**-----------------------------------------------------------------------------
 * 以下是代码工具生成，请不要做任何添加
 * 否则代码会被覆盖
 *
 * @date 2017-02-08
 * @author <kngcbzzdsn@outlook.com> 
 * 
 * 功 能： N/A
 * 描 述： 数据处理抽象基类 (增、删、改、查、分页等基础方法)
 * 
 * Ver    变更日期              负责人  变更内容
 * ───────────────────────────────────
 * V0.01  2017-02-08 18:10:00   朱会军    初版
 *
 * Copyright (c) Yu Qian Technology Co. Ltd. All rights reserved.
 ------------------------------------------------------------------------------*/
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
	/// IT_Bas_LinkBll
	/// </summary>	
	public partial class  T_Bas_LinkBll:IT_Bas_LinkBll
    {
	    private readonly IRepository<T_Bas_Link> _repository;
		
        public T_Bas_LinkBll(IRepository<T_Bas_Link> repository)
        {
            _repository = repository;
        }

        public bool Insert(T_Bas_Link p)
        {
            return _repository.Insert(p);
        }

        public bool Update(T_Bas_Link p)
        {
           return  _repository.Update(p);
        }

        public bool Delete(T_Bas_Link p)
        {
           return _repository.Delete(p);
        }
        /// <summary>
        /// 按照主键获得数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T_Bas_Link FindSingleData(params object[] keyValues)
        {
            return _repository.FindSingleData(keyValues);
        }
		/// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<T_Bas_Link> GetData(Expression<Func<T_Bas_Link, bool>> propertyExpr=null)
		{
			return _repository.GetData(propertyExpr);
		}
		/// <summary>2.根据条件查询，返回数据
        /// </summary>
        /// <param name="queryParas">查询条件</param>
        /// <param name="propertyExpr">字段的属性</param>
        /// <returns>返回数据列表</returns>
		public IList<T_Bas_Link> FindDataByCondition(List<CommonSearchModel> queryParas,
            Expression<Func<T_Bas_Link, bool>> propertyExpr = null)
		{
			return _repository.FindDataByCondition(queryParas,propertyExpr);
		}
		/// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="entityQueryable"></param>
		public int ResultDataCount(IQueryable<T_Bas_Link> entityQueryable)
		{
			return _repository.ResultDataCount(entityQueryable);
		}
		/// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        public int ResultDataCount_New(List<CommonSearchModel> queryParas,
            Expression<Func<T_Bas_Link, bool>> propertyExpr = null)
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
		 public IQueryable<T_Bas_Link> FindDataByPageFilter(List<CommonSearchModel> queryParas, string sortKey, int pageNumber, int pageSize, Expression<Func<T_Bas_Link, bool>> propertyExpr = null)
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
		 public IQueryable<T_Bas_Link> FindDataByPageFilter(string sortKey, int pageNumber, int pageSize, IQueryable<T_Bas_Link> entityList = null)
		 {
			return _repository.FindDataByPageFilter(sortKey,pageNumber,pageSize,entityList);
		 }
    }
}

