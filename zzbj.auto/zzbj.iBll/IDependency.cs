using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace zzbj.iBll
{
    public interface IDependency<T> where T : class
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        IList<T> GetData(Expression<Func<T, bool>> propertyExpr);
    }
}
