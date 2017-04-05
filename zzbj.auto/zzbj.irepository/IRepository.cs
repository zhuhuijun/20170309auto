using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace zzbj.repository
{
    public interface IRepository<Tentity> where Tentity : class
    {
        bool Insert(Tentity entity);
        bool Update(Tentity entity);
        bool Delete(Tentity entity);
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        IList<Tentity> GetData(Expression<Func<Tentity, bool>> propertyExpr);
    }
}
