//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2017-05-27 13:31:38 by zhuhj
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
	/// IT_Cod_RiverOriginStandardBll
	/// </summary>	
	public partial class  T_Cod_RiverOriginStandardBll:IT_Cod_RiverOriginStandardBll
    {
	    private readonly IRepository<T_Cod_RiverOriginStandard> _repository;
		
        public T_Cod_RiverOriginStandardBll(IRepository<T_Cod_RiverOriginStandard> repository)
        {
            _repository = repository;
        }

        public bool Insert(T_Cod_RiverOriginStandard p)
        {
            return _repository.Insert(p);
        }

        public bool Update(T_Cod_RiverOriginStandard p)
        {
           return  _repository.Update(p);
        }
        /// <summary>
        /// 修改指定的字段
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fileds"></param>
        /// <returns></returns>
		public bool UpdateSubFields(T_Cod_RiverOriginStandard entity, List<string> fileds)
	    {
	        return _repository.UpdateSubFields(entity,fileds);
	    }
        public bool Delete(T_Cod_RiverOriginStandard p)
        {
           return _repository.Delete(p);
        }
        /// <summary>
        /// 按照主键获得数据
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public T_Cod_RiverOriginStandard FindSingleData(params object[] keyValues)
        {
            return _repository.FindSingleData(keyValues);
        }
		/// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<T_Cod_RiverOriginStandard> GetData(Expression<Func<T_Cod_RiverOriginStandard, bool>> propertyExpr=null)
		{
			return _repository.GetData(propertyExpr);
		}
		/// <summary>2.根据条件查询，返回数据
        /// </summary>
        /// <param name="queryParas">查询条件</param>
        /// <param name="propertyExpr">字段的属性</param>
        /// <returns>返回数据列表</returns>
		public IList<T_Cod_RiverOriginStandard> FindDataByCondition(List<CommonSearchModel> queryParas,
            Expression<Func<T_Cod_RiverOriginStandard, bool>> propertyExpr = null)
		{
			return _repository.FindDataByCondition(queryParas,propertyExpr);
		}
		/// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="entityQueryable"></param>
		public int ResultDataCount(IQueryable<T_Cod_RiverOriginStandard> entityQueryable)
		{
			return _repository.ResultDataCount(entityQueryable);
		}
		/// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        public int ResultDataCount_New(List<CommonSearchModel> queryParas,
            Expression<Func<T_Cod_RiverOriginStandard, bool>> propertyExpr = null)
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
		 public IQueryable<T_Cod_RiverOriginStandard> FindDataByPageFilter(List<CommonSearchModel> queryParas, string sortKey, int pageNumber, int pageSize, Expression<Func<T_Cod_RiverOriginStandard, bool>> propertyExpr = null)
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
		 public IQueryable<T_Cod_RiverOriginStandard> FindDataByPageFilter(string sortKey, int pageNumber, int pageSize, IQueryable<T_Cod_RiverOriginStandard> entityList = null)
		 {
			return _repository.FindDataByPageFilter(sortKey,pageNumber,pageSize,entityList);
		 }
    }
}

