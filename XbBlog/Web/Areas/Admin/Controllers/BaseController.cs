using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Areas.Admin.Filter;

namespace Web.Areas.Admin.Controllers
{
    [ValidateLoginFilter]
    [Area("Admin")]
    public class BaseController : Controller
    {
    }
}