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
	/// IT_Cod_StandardToLimitBll
	/// </summary>	
	public  class  T_Cod_StandardToLimitBll:IT_Cod_StandardToLimitBll
    {
	    private readonly IRepository<T_Cod_StandardToLimit> _repository;
		
        public T_Cod_StandardToLimitBll(IRepository<T_Cod_StandardToLimit> repository)
        {
            _repository = repository;
        }

        public bool Insert(T_Cod_StandardToLimit p)
        {
            return _repository.Insert(p);
        }

        public bool Update(T_Cod_StandardToLimit p)
        {
           return  _repository.Update(p);
        }

        public bool Delete(T_Cod_StandardToLimit p)
        {
           return _repository.Delete(p);
        }
		/// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<T_Cod_StandardToLimit> GetData(Expression<Func<T_Cod_StandardToLimit, bool>> propertyExpr)
		{
			return _repository.GetData(propertyExpr);
		}
    }
}

