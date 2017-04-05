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
    public class personBll : IpersonBll
    {
        private readonly IRepository<person> _repository;
        public personBll(IRepository<person> repository)
        {
            _repository = repository;
        }

        public bool Insert(person p)
        {
           return  _repository.Insert(p);
        }

        public bool Update(person p)
        {
            return _repository.Update(p);
        }

        public bool Delete(person p)
        {
            return  _repository.Delete(p);
        }
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<person> GetData(Expression<Func<person, bool>> propertyExpr)
        {
            return _repository.GetData(propertyExpr);
        }
    }
}
