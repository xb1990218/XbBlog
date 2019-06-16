using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Areas.Admin.Filter
{
    /// <summary>
    /// 后台登陆验证过滤器
    /// </summary>
    public class ValidateLoginFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            int? uid = filterContext.HttpContext.Session.GetInt32("userId");
            if (uid == null || uid <= 0)
            {
                filterContext.Result = new RedirectResult("/Admin/Account/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
