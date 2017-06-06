using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebUtility;
using WebUtility.Security;
using zzbj.bll;
using zzbj.commons;
using zzbj.ibll;
using zzbj.models;
using zzbj.uis.Models;

namespace zzbj.uis.Controllers
{
    [RequireAuthorize]
    public class UsersController : WebControllerBase
    {
        readonly IT_Sys_UsersBll _bll;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onebll"></param>
        public UsersController(IT_Sys_UsersBll onebll)
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
            int records = SysDataHelper<T_Sys_Users>.ResultDataCount_New(parasD);
            int total = (int)Math.Ceiling((float)records / (float)rowsint);
            List<T_Sys_Users> usersList = null;
            try
            {
                usersList = SysDataHelper<T_Sys_Users>.FindDataByPageFilter(parasD,
                 string.IsNullOrEmpty(setting.sortColumn) ? "UserId" : setting.sortColumn + " " + setting.sortOrder, pageIndex, rowsint).ToList();

            }
            catch (Exception ex)
            {

                throw;
            }
            var jsonData = new
            {
                total = total,
                page = pageIndex,
                records = records,
                rows = (
                from sta in usersList
                select new
                {
                    i = sta.UserId,
                    cell = new object[]
                    {
                       sta.UserId,
                       sta.UserName,
                       sta.RealName,
                       sta.Email,
                       sta.Sex==0?"男":"女",
                       sta.Tel,
                       DateTime.Parse(sta.CreateDate.ToString()).ToString("yyyy-MM-dd")
                    }
                }
                ).ToArray()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 添加用户的界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            T_Sys_Users user = new T_Sys_Users();
            user.CreateDate = DateTime.Now;
            //角色的设置界面
            ViewBag.roles = new SelectList(SysDataHelper<sys_role>.GetData(), "id", "rowname");
            return View(user);
        }
        /// <summary>
        /// 添加的方法
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Add(T_Sys_Users one)
        {
            one.UserId = Guid.NewGuid();
            one.PassWord = MD5Helper.EncryptString(one.PassWord);
            one.CreateDate = DateTime.Now;
            one.IsStatus = 0;
            one.Sex = 1;
            one.ExamineStep = 2;
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
            T_Sys_Users one = _bll.FindSingleData(Guid.Parse(id));
            //角色的设置界面
            ViewBag.roles = new SelectList(SysDataHelper<sys_role>.GetData(), "id", "rowname");
            return View(one);
        }
        /// <summary>
        /// 保存修改信息
        /// </summary>
        /// <param name="one"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Edit(T_Sys_Users one)
        {
            bool edit = _bll.UpdateSubFields(one, new List<string>()
            {
                "UserName", "RealName", "Tel", "Email", "roleid", "CreateDate"
            });
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
            Guid idg = Guid.Parse(id);
            T_Sys_Users one = _bll.GetData(g => g.UserId == idg).FirstOrDefault();
            bool add = _bll.Delete(one);
            CRUDModel cm = CRUDModelHelper.GetRes(CRUD.DELETE, add);
            return Json(cm, JsonRequestBehavior.AllowGet);
        }
    }
}