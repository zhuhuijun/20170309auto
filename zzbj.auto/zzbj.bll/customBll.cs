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
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<custom> GetData(Expression<Func<custom, bool>> propertyExpr)
        {
            return _repository.GetData(propertyExpr);
        }
    }
}
