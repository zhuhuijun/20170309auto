using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using zzbj.commons;
using zzbj.core;
using zzbj.models;
using zzbj.repository;

namespace zzbj.bll
{
    /// <summary>
    /// 公用的处理数据库的方法
    /// </summary>
    /// <typeparam name="Tentity"></typeparam>
    public class SysDataHelper<Tentity> where Tentity : class, new()
    {
        /// <summary>
        /// 按照主键删除
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public static Tentity FindSingleData(params object[] keyValues)
        {
            try
            {
                IRepository<Tentity> adlServeice = CommonContainer.kernel.Resolve<IRepository<Tentity>>();
                return adlServeice.FindSingleData(keyValues);
            }
            catch (Exception ex)
            {
                Log4NetHepler.WriteLogToFile(ex);
            }
            return null;
        }
        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public static IList<Tentity> GetData(Expression<Func<Tentity, bool>> propertyExpr=null)
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
        /// <summary>
        /// 获得总条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        /// <returns></returns>
        public static int ResultDataCount_New(List<CommonSearchModel> queryParas,
            Expression<Func<Tentity, bool>> propertyExpr = null)
        {
            try
            {
                IRepository<Tentity> adlServeice = CommonContainer.kernel.Resolve<IRepository<Tentity>>();
                return adlServeice.ResultDataCount_New(queryParas, propertyExpr);
            }
            catch (Exception ex)
            {
                Log4NetHepler.WriteLogToFile(ex);
            }
            return 0;
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="queryParas">查询参数</param>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <param name="propertyExpr"></param>
        /// <returns>返回符合条件数据列表</returns>
        public static IQueryable<Tentity> FindDataByPageFilter(
            List<CommonSearchModel> queryParas,
            string sortKey,
            int pageNumber,
            int pageSize,
            Expression<Func<Tentity, bool>> propertyExpr = null)
        {
            try
            {
                IRepository<Tentity> adlServeice = CommonContainer.kernel.Resolve<IRepository<Tentity>>();
                return adlServeice.FindDataByPageFilter(queryParas, sortKey, pageNumber, pageSize, propertyExpr);
            }
            catch (Exception ex)
            {
                Log4NetHepler.WriteLogToFile(ex);
            }
            return null;
        }

        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="entity">修改数据并入库</param>
        /// <returns></returns>
        public static bool UpdateDataToBase(Tentity entity)
        {
            try
            {
                IRepository<Tentity> adlServeice = CommonContainer.kernel.Resolve<IRepository<Tentity>>();
                return adlServeice.Update(entity);
            }
            catch (Exception ex)
            {
                Log4NetHepler.WriteLogToFile(ex);
            }
            return false;
        }
    }
}
