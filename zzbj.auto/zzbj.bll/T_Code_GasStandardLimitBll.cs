//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2017-04-01 17:49:35 by ding
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
	/// IT_Code_GasStandardLimitBll
	/// </summary>	
	public  class  T_Code_GasStandardLimitBll:IT_Code_GasStandardLimitBll
    {
	    private readonly IRepository<T_Code_GasStandardLimit> _repository;
		
        public T_Code_GasStandardLimitBll(IRepository<T_Code_GasStandardLimit> repository)
        {
            _repository = repository;
        }

        public bool Insert(T_Code_GasStandardLimit p)
        {
            return _repository.Insert(p);
        }

        public bool Update(T_Code_GasStandardLimit p)
        {
           return  _repository.Update(p);
        }

        public bool Delete(T_Code_GasStandardLimit p)
        {
           return _repository.Delete(p);
        }
		/// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<T_Code_GasStandardLimit> GetData(Expression<Func<T_Code_GasStandardLimit, bool>> propertyExpr)
		{
			return _repository.GetData(propertyExpr);
		}
    }
}

