using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using zzbj.commons;
using zzbj.idal;

namespace zzbj.repository
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private IDal<T> _dal;
        public Repository(IDal<T> dal)
        {
            _dal = dal;
        }

        public bool Insert(T entity)
        {
            return _dal.Insert(entity);
        }

        public bool Update(T entity)
        {
            return _dal.Update(entity);
        }

        public bool Delete(T entity)
        {
            return _dal.Delete(entity);
        }
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<T> GetData(Expression<Func<T, bool>> propertyExpr)
        {
            try
            {
                return _dal.FindAllData().Where(propertyExpr).ToList();
            }
            catch (Exception ex)
            {
                Log4NetHepler.WriteLogToFile(ex);
                return null;
            }
        }

    }
}
