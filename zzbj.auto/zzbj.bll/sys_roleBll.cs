//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4模板自动生成
//	   生成时间 2017-04-01 13:04:52 by ding
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zzbj.ibll;
using zzbj.models;
using zzbj.repository;

namespace zzbj.bll
{
	/// <summary>
	/// Isys_roleBll
	/// </summary>	
	public  class  sys_roleBll:Isys_roleBll
    {
	    private readonly IRepository<sys_role> _repository;
        public sys_roleBll(IRepository<sys_role> repository)
        {
            _repository = repository;
        }

        public void Insert(sys_role p)
        {
            _repository.Insert(p);
        }

        public void Update(sys_role p)
        {
            _repository.Update(p);
        }

        public void Delete(sys_role p)
        {
            _repository.Delete(p);
        }

    }
}

