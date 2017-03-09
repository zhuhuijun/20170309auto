using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzbj.commons;
using zzbj.idal;

namespace zzbj.dal
{
    public class Dal<T> : IDal<T> where T : class
    {

        public void Insert(T entity)
        {
            Log4NetHepler.WriteLogToFile("insert");
        }

        public void Update(T entity)
        {
            Log4NetHepler.WriteLogToFile("Update");

        }
        public void Delete(T entity)
        {
            Log4NetHepler.WriteLogToFile("Delete");
        }
    }
}