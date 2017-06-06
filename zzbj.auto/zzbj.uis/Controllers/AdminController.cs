using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using zzbj.commons;
using zzbj.ibll;
using zzbj.models;
using zzbj.uis.Models;

namespace zzbj.uis.Controllers
{
    public class AdminController : Controller
    {
        private Irel_menuactionsBll relma;
        public AdminController(Irel_menuactionsBll _rr)
        {
            this.relma = _rr;
        }
        // GET: Admin
        public ActionResult Login(UserLoginModel user)
        {
            bool isLogin = false;
            if (User.Identity.IsAuthenticated)
            {
                return Redirect(Url.Action("Index", "Home"));
            }
            else
            {
                string validateCookie = BaseHelper.GetCookie("ValidationCode").ToLower();
                if (user.UserAuthCode != null)
                {
                    if (string.Equals(validateCookie, user.UserAuthCode.ToLower()))
                    {
                        if (!string.IsNullOrEmpty(user.UserName) && !string.IsNullOrEmpty(user.Password))
                        {
                            AccountModel amodel = new AccountModel();
                            T_Sys_Users usercurr = amodel.ValidateUserLogin(user.UserName, user.Password);
                            if (usercurr != null)
                            {
                                //创建用户ticket信息
                                amodel.CreateLoginUserTicket(user.UserName, user.Password);
                                //读取用户权限数据
                                List<rel_rolemenus> rolemenus = relma.GetControllerAndActions(usercurr.roleid);
                                //设置用户的权限
                                amodel.GetUserAuthorities(usercurr.roleid, rolemenus);
                                return Redirect(Url.Action("Index", "Home"));
                            }
                            else
                            {
                                user.ErrorMsg = SysCommonResource.LoginErrorUserNameOrPassword;
                            }
                        }
                    }
                    else
                    {
                        user.ErrorMsg = SysCommonResource.LoginOther;
                    }
                }
            }
            if (!isLogin)
                return View(user);
            else
                return Redirect(Url.Action("Index", "Home"));
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOff()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();
            return RedirectToAction("Login", "Admin");
        }
    }
}