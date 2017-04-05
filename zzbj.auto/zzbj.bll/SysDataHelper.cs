using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using zzbj.commons;
using zzbj.core;
using zzbj.repository;

namespace zzbj.bll
{
    public class SysDataHelper<Tentity> where Tentity : class, new()
    {
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public static IList<Tentity> GetData(Expression<Func<Tentity, bool>> propertyExpr)
        {
            try
            {
                IRepository<Tentity> adlServeice = CommonContainer.kernel.Resolve<IRepository<Tentity>>();
                return adlServeice.GetData(propertyExpr).ToList();
            }
            catch (Exception ex)
            {
                Log4NetHepler.WriteLogToFile(ex);
            }
            return null;
        }
    }
}
