using Entities;
using Entities.Models;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Service
{
    /// <summary>
    /// 后台登陆用户服务
    /// </summary>
    public class AdminUserService:BaseService<AdminUser>,IAdminUserService
    {
        public AdminUserService(EFContext eFContext)
        {
            db = eFContext;
        }
    }
}
