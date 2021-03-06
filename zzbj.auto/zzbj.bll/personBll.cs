﻿using System;
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
    public class personBll : IpersonBll
    {
        private readonly IRepository<person> _repository;
        public personBll(IRepository<person> repository)
        {
            _repository = repository;
        }

        public bool Insert(person p)
        {
            return _repository.Insert(p);
        }

        public bool Update(person p)
        {
            return _repository.Update(p);
        }
        public bool UpdateSubFields(person entity, List<string> fileds)
        {
            return _repository.UpdateSubFields(entity, fileds);
        }
        public bool Delete(person p)
        {
            return _repository.Delete(p);
        }
        /// <summary>
        /// 按照主键删除
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        public person FindSingleData(params object[] keyValues)
        {
            return _repository.FindSingleData(keyValues);
        }

        /// <summary>
        /// 根据查询条件获取数据
        /// </summary>
        /// <param name="propertyExpr">查询条件</param>
        /// <returns></returns>
        public IList<person> GetData(Expression<Func<person, bool>> propertyExpr)
        {
            return _repository.GetData(propertyExpr);
        }
        /// <summary>2.根据条件查询，返回数据
        /// </summary>
        /// <param name="queryParas">查询条件</param>
        /// <param name="propertyExpr">字段的属性</param>
        /// <returns>返回数据列表</returns>
        public IList<person> FindDataByCondition(List<CommonSearchModel> queryParas,
            Expression<Func<person, bool>> propertyExpr = null)
        {
            return _repository.FindDataByCondition(queryParas, propertyExpr);
        }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="entityQueryable"></param>
        public int ResultDataCount(IQueryable<person> entityQueryable)
        {
            return _repository.ResultDataCount(entityQueryable);
        }
        /// <summary>
        /// 返回数据条数
        /// </summary>
        /// <param name="queryParas"></param>
        /// <param name="propertyExpr"></param>
        public int ResultDataCount_New(List<CommonSearchModel> queryParas,
            Expression<Func<person, bool>> propertyExpr = null)
        {
            return _repository.ResultDataCount_New(queryParas, propertyExpr);
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="queryParas">查询参数</param>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
        public IQueryable<person> FindDataByPageFilter(List<CommonSearchModel> queryParas, string sortKey, int pageNumber, int pageSize, Expression<Func<person, bool>> propertyExpr = null)
        {
            return _repository.FindDataByPageFilter(queryParas, sortKey, pageNumber, pageSize, propertyExpr);
        }
        /// <summary>
        /// 分页数据
        /// </summary>
        /// <param name="sortKey">排序</param>
        /// <param name="pageNumber">当前页数</param>
        /// <param name="pageSize">展示数据条数</param>
        /// <returns>返回符合条件数据列表</returns>
        public IQueryable<person> FindDataByPageFilter(string sortKey, int pageNumber, int pageSize, IQueryable<person> entityList = null)
        {
            return _repository.FindDataByPageFilter(sortKey, pageNumber, pageSize, entityList);
        }
    }
}
