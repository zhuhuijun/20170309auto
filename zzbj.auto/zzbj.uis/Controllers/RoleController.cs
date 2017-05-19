﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using zzbj.bll;
using zzbj.ibll;
using zzbj.models;
using zzbj.uis.Models;

namespace zzbj.uis.Controllers
{
    public class RoleController : Controller
    {
        readonly Isys_roleBll _bll;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onebll"></param>
        public RoleController(Isys_roleBll onebll)
        {
            _bll = onebll;
        }
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 分页的数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetData(GridSettings setting)
        {
            string requestStringPar = Request["customPar"];
            string page = Request["page"];//当前页
            string rows = Request["rows"];//每页显示
            int pageIndex = Convert.ToInt32(page);//当前页
            int rowsint = Convert.ToInt32(rows);
            //过滤参数
            List<CommonSearchModel> parasD = null;
            if (!string.IsNullOrEmpty(requestStringPar))
            {
                parasD = JsonConvert.DeserializeObject<List<CommonSearchModel>>(requestStringPar); ;
            }
            int records = _bll.ResultDataCount_New(parasD);
            int total = (int)Math.Ceiling((float)records / (float)rowsint);
            var dataList = _bll.FindDataByPageFilter(parasD,
                string.IsNullOrEmpty(setting.sortColumn) ? "createtime" : setting.sortColumn + " " + setting.sortOrder, pageIndex, rowsint).ToList();
            var jsonData = new
            {
                total = total,
                page = pageIndex,
                records = records,
                rows = dataList
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加用户的界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            sys_role one = new sys_role {createtime = DateTime.Now};
            return View(one);
        }
        /// <summary>
        /// 添加的方法
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(sys_role one)
        {
            one.id = Guid.NewGuid().ToString();
            bool add = _bll.Insert(one);
            CRUDModel cm = CRUDModelHelper.GetRes(CRUD.ADD, add);
            return Json(cm, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 修改的界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(string id)
        {
            sys_role one = _bll.FindSingleData(id);
            return View(one);
        }
        /// <summary>
        /// 保存修改信息
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(sys_role one)
        {
            bool edit = _bll.Update(one);
            CRUDModel cm = CRUDModelHelper.GetRes(CRUD.EDIT, edit);
            return Json(cm);
        }
        /// <summary>
        /// 删除的方法
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Delete(string id)
        {
            var uu = _bll.FindSingleData(id);
            bool add = _bll.Delete(uu);
            CRUDModel cm = CRUDModelHelper.GetRes(CRUD.DELETE, add);
            return Json(cm, JsonRequestBehavior.AllowGet);
        }
    }
}