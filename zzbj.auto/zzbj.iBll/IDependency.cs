using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zzbj.iBll
{
    public interface IDependency<T> where T : class
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
