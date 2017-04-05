using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zzbj.commons;
using zzbj.models;
using zzbj.uis.Models;

namespace zzbj.uis.Controllers
{
    public class AdminController : Controller
    {
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
                string validateCookie =  BaseHelper.GetCookie("ValidationCode").ToLower();
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
                                //amodel.GetUserAuthorities(usercurr.roleid);
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
    }
}