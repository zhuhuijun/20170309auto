using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using zzbj.bll;
using zzbj.commons;
using zzbj.ibll;
using zzbj.models;
using zzbj.uis.Models;

namespace zzbj.uis.Controllers
{
    public class ModulesController : Controller
    {
        readonly IT_Bas_ModuleBll _bll;
        private readonly Irel_menuactionsBll _menuaction;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onebll"></param>
        /// <param name="menuaction"></param>
        public ModulesController(IT_Bas_ModuleBll onebll, Irel_menuactionsBll menuaction)
        {
            _bll = onebll;
            _menuaction = menuaction;
        }
        // GET: Modules
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获得subgrid的数据
        /// </summary>
        /// <param name="setting"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetData(GridSettings setting, int? id = 0)
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
            int records = SysDataHelper<T_Bas_Module>.ResultDataCount_New(parasD,
                M => M.ParentID == id && M.ApplicationID == 1);
            int total = (int)Math.Ceiling((float)records / (float)rowsint);
            var dataList = SysDataHelper<T_Bas_Module>.FindDataByPageFilter(parasD,
                string.IsNullOrEmpty(setting.sortColumn) ? "MouduleID" : setting.sortColumn + " " + setting.sortOrder, pageIndex, rowsint,
                M => M.ParentID == id && M.ApplicationID == 1).ToList();
            var jsonData = new
            {
                total = total,
                page = pageIndex,
                records = records,
                rows = (
                from sta in dataList
                select new
                {
                    i = sta.MouduleID,
                    cell = new object[]
                    {
                       sta.MouduleID,
                       sta.MouduleName,
                       sta.IsUse==0?"是":"否",
                       "<img src='../Content/ModulesImages/"+ sta.IcoPath+"' width='20' height='20' alt='' />",
                       sta.MenuUrl,
                       sta.IsFuntion==0?"有":"无",
                       sta.CreateDate.Value.ToString("yyyy-MM-dd")
                    }
                }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            ViewData["IsUse"] = PublicCommon.GetPublicCommon()["IsUse"];
            ViewData["IsFuntion"] = PublicCommon.GetPublicCommon()["IsFuntion"];
            ViewBag.applicationList = new SelectList(PublicCommon.GetPublicCommon()["ApplicationID"], "Value", "ShowName");
            ViewBag.moduleList = new SelectList((DataCache.GetCache(ObjectCacheName.Module) as IList<T_Bas_Module>), "MouduleID", "MouduleName");
            ViewBag.moduleTypeList = new SelectList(PublicCommon.GetIsMouduleType(), "Value", "ShowName");
            T_Bas_Module module = new T_Bas_Module();
            module.CreateDate = DateTime.Now;
            return View(module);
        }
        /// <summary>
        /// 保存添加数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(T_Bas_Module one)
        {
            var parent = _bll.FindSingleData(one.ParentID);
            one.Path = parent.Path + 1;
            bool add = _bll.Insert(one);
            CRUDModel cm = CRUDModelHelper.GetRes(CRUD.ADD, add);
            return Json(cm, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int? id)
        {
            ViewData["IsUse"] = PublicCommon.GetPublicCommon()["IsUse"];
            ViewData["IsFuntion"] = PublicCommon.GetPublicCommon()["IsFuntion"];
            T_Bas_Module module = _bll.FindSingleData(id);
            if (!object.Equals(module, null))
            {
                ViewBag.applicationList = new SelectList(PublicCommon.GetPublicCommon()["ApplicationID"], "Value", "ShowName");
                ViewBag.moduleList = new SelectList((DataCache.GetCache(ObjectCacheName.Module) as IList<T_Bas_Module>), "MouduleID", "MouduleName");
                ViewBag.moduleTypeList = new SelectList(PublicCommon.GetIsMouduleType(), "Value", "ShowName");
            }
            return View(module);
        }
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            CRUDModel cm = null;
            if (id == 0)
            {
                cm = new CRUDModel();
            }
            else
            {
                //判断是否有关联的子级菜单
                int cou = _bll.GetData(k => k.ParentID == id).Count;
                if (cou > 0)
                {
                    cm = new CRUDModel(CRUD.HAVELINK);
                }
                else
                {
                    var uu = _bll.FindSingleData(id);
                    bool del = _bll.Delete(uu);
                    cm = CRUDModelHelper.GetRes(CRUD.DELETE, del);
                }
            }
            return Json(cm, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetPathData(int id)
        {
            int path;
            T_Bas_Module module = SysDataHelper<T_Bas_Module>.FindSingleData(id);
            if (module != null)
            {
                path = Convert.ToInt32(module.Path);
            }
            else
            {
                path = 0;
            }
            return path;
        }
        /// <summary>
        /// 获得操作行为的界面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetActionTree(int id)
        {
            List<sys_action> actions = SysDataHelper<sys_action>.GetData().ToList();
            ViewBag.Actions = actions;
            ViewBag.moduleid = id;
            //已经勾选过的按钮
            ViewBag.HaveMenu = _menuaction.GetData().Where(g => g.menuid == id.ToString()).ToList();
            return View();
        }
        /// <summary>
        /// 保存菜单和按钮的关联数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetActionTree(string menuid, List<String> actionids)
        {
            CRUDModel cm = null;
            if (string.IsNullOrEmpty(menuid) || actionids == null || actionids.Count < 1)
            {
                cm = new CRUDModel();
            }
            else
            {
                bool add = _menuaction.SaveMenuAction(menuid, actionids);
                cm = CRUDModelHelper.GetRes(CRUD.MENUACTION, add);
            }
            return Json(cm, JsonRequestBehavior.DenyGet);
        }
    }
}