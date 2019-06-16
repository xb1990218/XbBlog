using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IService;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        //后台登陆用户表服务
        private IAdminUserService adminUserSice;
        public AccountController(IAdminUserService adminUserService)
        {
            adminUserSice = adminUserService;
        }
        public IActionResult Login()
        {
            return View();
        }

        //登录验证，成功后写入session
        public JsonResult LoginIn(string username,string password)
        {
            if (string.IsNullOrWhiteSpace(username)|| string.IsNullOrWhiteSpace(password))
            {
                return Json(new BoolResult { Result = false, Msg = "用户名或者密码错误！" });
            }
            AdminUser aUser=adminUserSice.GetEntity(a => a.Account == username && a.Password == password);
            if (aUser==null)
            {
                return Json(new BoolResult { Result = false, Msg = "用户名或者密码错误！" });
            }

            //写入session
            HttpContext.Session.SetInt32("userId", aUser.Id);
            return Json(new BoolResult { Result = true, Msg = "登陆成功！" });
        }

        //退出后台
        public JsonResult Exit()
        {
            HttpContext.Session.Remove("userId");
            return Json(new BoolResult { Result = true});
        }
    }
}