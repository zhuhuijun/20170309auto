﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUtility;
using zzbj.bll;
using zzbj.uis.Models;

namespace zzbj.uis.Controllers
{
    public class HomeController : WebControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 加载主模块的数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string GetSysModule()
        {
            //初始化系统基础数据
            
               //清空所有缓存
               IDictionaryEnumerator cacheEnmu = HttpRuntime.Cache.GetEnumerator();
               while (cacheEnmu.MoveNext())
               {
                   HttpRuntime.Cache.Remove(cacheEnmu.Key.ToString());
               }
               PublicMethod.LoadModule();
               PublicMethod.LoadRole();
               

            return SysInitModels.GetMenuJsonStringResult();
        }
    }
}