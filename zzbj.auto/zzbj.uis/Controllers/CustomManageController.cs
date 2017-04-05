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
        private readonly IT_Bas_CityBll _user;
        //
        public CustomManageController(IcustomBll bll, IT_Bas_CityBll city)
        {
            _bll = bll;
            _user = city;
        }
        // GET: CustomManage
        public ActionResult Index()
        {
            _bll.Insert(new custom());
            _user.Insert(new T_Bas_City());
            return View();
        }
    }
}