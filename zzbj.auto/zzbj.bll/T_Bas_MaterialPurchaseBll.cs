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
	/// IT_Bas_MaterialPurchaseBll
	/// </summary>	
	public  class  T_Bas_MaterialPurchaseBll:IT_Bas_MaterialPurchaseBll
    {
	    private readonly IRepository<T_Bas_MaterialPurchase> _repository;
		
        public T_Bas_MaterialPurchaseBll(IRepository<T_Bas_MaterialPurchase> repository)
        {
            _repository = repository;
        }

        public bool Insert(T_Bas_MaterialPurchase p)
        {
            return _repository.Insert(p);
        }

        public bool Update(T_Bas_MaterialPurchase p)
        {
           return  _repository.Update(p);
        }

        public bool Delete(T_Bas_MaterialPurchase p)
        {
           return _repository.Delete(p);
        }
		/// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<T_Bas_MaterialPurchase> GetData(Expression<Func<T_Bas_MaterialPurchase, bool>> propertyExpr)
		{
			return _repository.GetData(propertyExpr);
		}
    }
}

