using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzbj.idal
{
    public interface IDal<TEntity> where TEntity : class
    {
        bool Insert(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(TEntity entity);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns>返回所有数据</returns>
        IQueryable<TEntity> FindAllData();
    }
}
