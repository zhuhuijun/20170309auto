using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using zzbj.bll;
using zzbj.commons;
using zzbj.models;

namespace zzbj.uis.Controllers
{
    public class DepartMentInfoController : Controller
    {
        // GET: DepartMentInfo
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 分页的数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetData()
        {
            string requestStringPar = Request["customPar"];
            string page = Request["page"];//当前页
            string rows = Request["rows"];//每页显示
            int pageIndex = Convert.ToInt32(page);//当前页
            int rowsint = Convert.ToInt32(rows);
            //过滤参数
            List<CommonSearchModel> parasli = null; 
            if (!string.IsNullOrEmpty(requestStringPar))
            {
                parasli = JsonConvert.DeserializeObject<List<CommonSearchModel>>(requestStringPar);
            }
            int records = SysDataHelper<T_Sys_DEPARTMENTINFO>.ResultDataCount_New(parasli);
            int total = (int)Math.Ceiling((float)records / (float)rowsint);
            var dataList = SysDataHelper<T_Sys_DEPARTMENTINFO>.FindDataByPageFilter(parasli, "DEPTID", pageIndex, rowsint).ToList();
            var jsonData = new
            {
                total = total,
                page = pageIndex,
                records = records,
                rows = dataList
            };
            return Json(jsonData);
        }
    }
}