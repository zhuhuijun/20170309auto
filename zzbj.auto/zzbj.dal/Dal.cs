using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zzbj.commons;
using zzbj.idal;
using zzbj.models;

namespace zzbj.dal
{
    public class Dal<T> : IDal<T> where T : class
    {
        private dapper_testEntities _mDbContext = new dapper_testEntities();
        public bool Insert(T entity)
        {
            _mDbContext.Set<T>().Add(entity);
            return _mDbContext.SaveChanges() > 0;
        }

        public bool Update(T entity)
        {
            if (_mDbContext.Entry(entity).State != EntityState.Modified)
            {
                _mDbContext.Entry(entity).State = EntityState.Modified;
            }
            return _mDbContext.SaveChanges() > 0;

        }
        public bool Delete(T entity)
        {
            this._mDbContext.Set<T>().Remove(entity);
            return this._mDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 查询数据列表
        /// </summary>
        /// <returns>返回所有数据</returns>
        public IQueryable<T> FindAllData()
        {
            try
            {
                return _mDbContext.Set<T>().AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 对象销毁
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        //protected的Dispose方法，保证不会被外部调用。
        //传入bool值disposing以确定是否释放托管资源
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mDbContext.Dispose();
            }
        }
        /// <summary>
        /// 供GC调用的析构函数
        /// </summary>
        ~Dal()
        {
            //释放非托管资源
            Dispose(false);
        }
    }
}