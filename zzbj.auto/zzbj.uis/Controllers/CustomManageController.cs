using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Builder;
using zzbj.bll;
using zzbj.ibll;
using zzbj.models;
using zzbj.repository;

namespace zzbj.uis.Controllers
{
    public class CustomManageController : Controller
    {
        readonly IcustomBll _bll;
        //构造器注入
        public CustomManageController(IcustomBll bll)
        {
            _bll = bll;
        }
        // GET: CustomManage
        public ActionResult Index()
        {
            _bll.Insert(new custom());
            return View();
        }
    }
}