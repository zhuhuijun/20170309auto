using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using WebUtility;
using zzbj.bll;
using zzbj.commons;
using zzbj.models;

namespace zzbj.uis.Models
{
    public class AccountModel
    {
        /// <summary>
        /// 创建登录用户的票据信息
        /// </summary>
        /// <param name="strUserName"></param>
        internal void CreateLoginUserTicket(string strUserName, string strPassword)
        {
            //构造Form验证的票据信息
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, strUserName, DateTime.Now, DateTime.Now.AddMinutes(240),
                true, string.Format("{0}:{1}", strUserName, strPassword), FormsAuthentication.FormsCookiePath);

            string ticString = FormsAuthentication.Encrypt(ticket);

            //把票据信息写入Cookie和Session
            //SetAuthCookie方法用于标识用户的Identity状态为true
            HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, ticString));
            FormsAuthentication.SetAuthCookie(strUserName, true);
            HttpContext.Current.Session["USER_LOGON_TICKET"] = ticString;

            //重写HttpContext中的用户身份，可以封装自定义角色数据；
            //判断是否合法用户，可以检查：HttpContext.User.Identity.IsAuthenticated的属性值
            string[] roles = ticket.UserData.Split(',');
            IIdentity identity = new FormsIdentity(ticket);
            IPrincipal principal = new GenericPrincipal(identity, roles);
            HttpContext.Current.User = principal;
        }

        /// <summary>
        /// 获取用户权限列表数据
        /// [
        ///  {"Controller": "员工请假", "Actions": "申请, 审核"},
        ///  {"Controller": "产品订单申请", "Actions": "申请, 列表, 详细"}
        /// ]
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        internal void GetUserAuthorities(string roleid, List<rel_rolemenus> rolemenus)
        {
            string lastre = GetControllerAndActions(roleid, rolemenus);
            //从WebApi 访问用户权限数据，然后写入Session
            var userAuthList = ServiceStack.Text.JsonSerializer.DeserializeFromString(lastre, typeof(UserAuthModel[]));
            HttpContext.Current.Session["USER_AUTHORITIES"] = userAuthList;
        }

        /// <summary>
        /// 读取数据库用户表数据，判断用户密码是否匹配
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        internal T_Sys_Users ValidateUserLogin(string name, string password)
        {
            string encpawd = MD5Helper.EncryptString(password);
            IList<T_Sys_Users> sysUsers = SysDataHelper<T_Sys_Users>.GetData(m => m.UserName == name && m.PassWord == encpawd).ToList();
            if (sysUsers.ToList().Count > 0)
            {
                T_Sys_Users sysUser = sysUsers.ToList().FirstOrDefault();
                HttpContext.Current.Session["userinfo"] = JsonConvert.SerializeObject(sysUser);
                return sysUser;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 用户注销执行的操作
        /// </summary>
        internal void Logout()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 根据角色获得行为
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="rolemenus"></param>
        /// <returns></returns>
        public static string GetControllerAndActions(string roleid, List<rel_rolemenus> rolemenus)
        {
            Dictionary<string, string> controllerDic = new Dictionary<string, string>();
            List<rel_rolemenus> menuandaction = rolemenus;
            List<T_Bas_Module> menus = SysDataHelper<T_Bas_Module>.GetData().ToList();
            List<sys_action> actions = SysDataHelper<sys_action>.GetData().ToList();
            //1.将控制器和行为的主键进行分离
            foreach (rel_rolemenus tmp in menuandaction)
            {
                string[] tmparr = tmp.menuid.Split('*');
                int moduleid = Convert.ToInt32(tmparr[0]);
                var firstOrDefault = menus.FirstOrDefault(g => g.MouduleID == moduleid);
                if (firstOrDefault != null)
                {
                    string controllername = firstOrDefault.MenuUrl;
                    var sysAction = actions.FirstOrDefault(g => g.id == tmparr[1]);
                    if (sysAction != null)
                    {
                        string action = sysAction.actioncode;
                        action = AppendActions(action);
                        if (!string.IsNullOrEmpty(controllername) && !string.IsNullOrEmpty(action))
                        {
                            //如果有此控制器就继续追加行为
                            if (controllerDic.ContainsKey(controllername))
                            {
                                string val = controllerDic[controllername];
                                string newval = val + "," + action;
                                controllerDic[controllername] = newval;
                            }
                            else//如果没有就添加
                            {
                                controllerDic.Add(controllername, action);
                            }

                        }
                    }
                }
            }
            string retl = DicToStr(controllerDic);
            return retl;
        }
        /// <summary>
        /// 将字典转为字符串
        /// </summary>
        /// <param name="dics"></param>
        /// <returns></returns>
        private static string DicToStr(Dictionary<string, string> dics)
        {
            StringBuilder sb = new StringBuilder();

            if (dics.Count > 0)
            {
                sb.Append("[ ");
                foreach (KeyValuePair<string, string> tmp in dics)
                {
                    StringBuilder ss = new StringBuilder("{");
                    ss.AppendFormat("\"controller\": \"{0}\", \"actions\":\"{1}\"", tmp.Key.ToLower(), tmp.Value.ToLower());
                    ss.Append(" },");
                    sb.Append(ss);
                }
                sb = sb.Remove(sb.Length - 1, 1);
                sb.Append(" ]");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 额外的行为
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private static string AppendActions(string action)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(action);
            switch (action.ToLower())
            {
                case "index":
                    sb.Append(",GetData");
                    break;
            }
            return sb.ToString();
        }
    }
}