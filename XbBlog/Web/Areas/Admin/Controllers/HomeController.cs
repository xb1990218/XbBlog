using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.IService;

namespace Web.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        //访问记录表服务
        private IVisitLogService vlogSice;
        //公共服务
        private ICommonService comSice;
        //后台登陆用户表服务
        private IAdminUserService adminUserSice;
        //博主信息表服务
        private IMasterInfoService msInfoSice;
        //文章分类服务
        private ICategoryService cgSice;
        //文章表服务
        private IArticleService aceSice;
        //环境服务
        private IHostingEnvironment env;
        public HomeController(IVisitLogService visitLogService, ICommonService commonService, IArticleService articleService, ICategoryService categoryService, IHostingEnvironment environment, IMasterInfoService masterInfoService, IAdminUserService adminUserService)
        {
            vlogSice = visitLogService;
            comSice = commonService;
            aceSice = articleService;
            cgSice = categoryService;
            env = environment;
            msInfoSice = masterInfoService;
            adminUserSice = adminUserService;
        }
        //后台管理主页
        public IActionResult Index()
        {
            int? id= HttpContext.Session.GetInt32("userId");
            AdminUser aUser=adminUserSice.GetEntity(a => a.Id == id);
            return View(aUser);
        }

        //个人资料页面
        public IActionResult MyInfo()
        {
            MasterInfo mInfo=msInfoSice.GetEntity(a => a.Id == 1);
            ViewBag.mInfo = mInfo;
            return View();
        }

        //新增文章页面
        public IActionResult AddWz()
        {
            List<Category> list=cgSice.GetListEntities(a => true);
            ViewBag.list = list;
            return View();
        }

        //新增文章接口
        public IActionResult AddNewWz(string title,string describe,int categoryId,string container, int isTop = 0)
        {
            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(container))
            {
                return Json(new BoolResult { Result = false,Msg="文章标题或者内容不能为空！" });
            }
            Category cte=cgSice.GetEntity(a => a.Id == categoryId);
            if (cte==null)
            {
                return Json(new BoolResult { Result = false, Msg = "分类不存在！" });
            }
            Article art = new Article();
            art.Title = title;
            art.Describe = describe;
            art.Body = container;
            art.IsTop = isTop;
            art.CreateDate = DateTime.Now;
            art.VistNum = 0;
            art.CategoryId = categoryId;
            try
            {
                aceSice.Add(art);
            }
            catch (Exception)
            {

                return Json(new BoolResult { Result = false,Msg="新增失败！" });
            }
            
            return Json(new BoolResult {Result=true });
        }
        //文章列表管理页
        public IActionResult WzList()
        {
            return View();
        }
        //删除一篇文章
        public JsonResult DelWz(int id)
        {
            bool b = false;
            try
            {
                b = aceSice.DelByIds(id);
            }
            catch (Exception e)
            {

                return Json(new BoolResult { Result = false, Msg = e.Message });
            }
            
            if (b)
            {
                return Json(new BoolResult { Result=true});
            }
            else
            {
                return Json(new BoolResult { Result = false,Msg="删除失败！" });
            }
        }
        //编辑文章页面
        public IActionResult EditWz(int id)
        {
            Article ace=aceSice.GetEntity(a => a.Id == id);
            if (ace==null)
            {
                return Redirect("/Admin/Home/Index");
            }
            List<Category> list = cgSice.GetListEntities(a => true);
            ViewBag.list = list;
            ViewBag.ace = ace;
            return View();
        }
        //更新文章接口
        public JsonResult UpdateWz(int id,string title,string describe,int categoryId,string container, int isTop = 0)
        {
            Article ace = aceSice.GetEntity(a => a.Id == id);
            if (ace==null)
            {
                return Json(new BoolResult { Result = false, Msg = "文章不存在！" });
            }
            ace.Title = title;
            ace.Describe = describe;
            ace.CategoryId = categoryId;
            ace.Body = container;
            ace.IsTop = isTop;
            bool b = false;
            try
            {
                b=aceSice.Edit(ace);
            }
            catch (Exception e)
            {
                return Json(new BoolResult { Result = false, Msg =e.Message  });
            }
            if (b)
            {
                return Json(new BoolResult { Result = true});
            }
            else
            {
                return Json(new BoolResult { Result = false,Msg="更新失败！" });
            }  
        }

        //获取文章分页数据带文章关键词搜索
        public JsonResult GetWzList(int page,int limit,string titleKey)
        {
            List<WzView> list = null;
            int totalCount = 0;
            try
            {
                list=comSice.GetWzListPage(page, limit, titleKey, out totalCount);
            }
            catch (Exception)
            {

                return Json(new { code = 0, msg = "", count = 0, data = "" });
            }
            return Json(new { code = 0, msg = "", count = totalCount, data = list });
        }

        //文章分类页面
        public IActionResult Category()
        {
            return View();
        }
        //分页获取文章分类列表
        public JsonResult GetCategoryList(int page,int limit)
        {
            List<Category> list=cgSice.GetPageList(page, limit, a => true, a => a.CreateDate, false, out int TotalCount);
            return Json(new{code=0,msg="",count=TotalCount,data=list});
        }

        //新增分类
        public JsonResult AddCategory(string name)
        {
            Category ca=cgSice.GetEntity(a => a.Name == name);
            if (ca!=null)
            {
                return Json(new BoolResult {Result=false,Msg=$"分类名【{name}】已存在！" });
            }
            Category newCa = new Category();
            newCa.Name = name;
            newCa.CreateDate = DateTime.Now;
            cgSice.Add(newCa);
            return Json(new BoolResult { Result = true});
        }
        //根据id删除分类
        public JsonResult DelCategory(int id)
        {
            //该分类下面有文章 则不能删除
            var list=aceSice.GetListEntities(a => a.CategoryId == id);
            if (list.Count>0)
            {
                return Json(new BoolResult { Result=false,Msg="该分类下面有文章 不能删除！"});
            }
            //执行删除
            var cg=cgSice.GetEntity(a => a.Id == id);
            bool b=cgSice.Del(cg);
            if (b)
            {
                return Json(new BoolResult { Result = true});
            }
            else
            {
                return Json(new BoolResult { Result = false,Msg="删除失败！" });
            }
        }
        //修改分类
        public JsonResult EditCategory(int id,string name)
        {
            var cg=cgSice.GetEntity(a => a.Id == id);
            cg.Name = name;
            bool b=cgSice.Edit(cg);
            if (b)
            {
                return Json(new BoolResult { Result = true });
            }
            else
            {
                return Json(new BoolResult { Result = false, Msg = "修改失败！" });
            }
        }
        //首页访问记录 页面
        public IActionResult IndexVisLog()
        {
            return View();
        }
        //获取首页访问记录接口-分页 搜索
        public JsonResult GetIndexVitLog(int page,int limit,string bedate,string positionKey)
        {
            List<VisitLog> list=vlogSice.GetVisitLogsPage(page, limit, bedate, positionKey, out int totalCount);
            return Json(new { code = 0, msg = "", count = totalCount, data = list });
        }
        //上传头像
        public JsonResult UploadImg()
        {
            var uploadfile = Request.Form.Files[0];//获取文件对象
            var fileExtension = Path.GetExtension(uploadfile.FileName);//获取文件后缀
            if (fileExtension == null)
            {
                return Json(new{code=-1,msg= "上传的文件没有后缀",data="" });
            }
            //获取绝对路径 如：F:\Work\ZhiXiangWeb\ZhiXiang\ZhiXiangWeb\wwwroot
            var root = env.WebRootPath;//这里这个env需要注入IHostingEnvironment
            //建立文件路径
            string sYear = DateTime.Now.ToString("yyyy");
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();
            var filePath = string.Format("/Upload/{0}/{1}/{2}/", sYear, sMonth, sDay);

            var resPath = root + "/" + filePath;
            //创建文件夹
            if (!Directory.Exists(resPath))
            {
                Directory.CreateDirectory(resPath);
            }
            //新图片的名称，用guid生成 全局唯一不重复
            string newImageName = Guid.NewGuid().ToString("N") + fileExtension;
            using (FileStream fs = System.IO.File.Create(resPath + "/" + newImageName))
            {
                uploadfile.CopyTo(fs);
                fs.Flush();
            }
            //新文件的相对路径，保存到数据库就存这个路径
            var resImageUrl = filePath + newImageName;
            return Json(new { code = 0, msg = "", data =new{src= resImageUrl } });
        }
        //更新信息
        public JsonResult EditInfo(string name,string signame,string headImg)
        {
            MasterInfo m=msInfoSice.GetEntity(a => a.Id == 1);
            m.HeadImg = headImg;
            m.Name = name;
            m.SigName = signame;
            bool b=msInfoSice.Edit(m);
            if (b)
            {
                return Json(new BoolResult {Result = true, Msg = "更新成功"});
            }
            else
            {
                return Json(new BoolResult { Result = false, Msg = "更新失败" });
            }
        }

        //修改密码界面
        public IActionResult Password()
        {
            return View();
        }

        //修改密码
        public JsonResult UpdatePassword(string oldpassword, string newpassword)
        {
            int? id = HttpContext.Session.GetInt32("userId");
            AdminUser u = adminUserSice.GetEntity(a=>a.Id==id);
            //判断原密码是否正确
            if (u.Password != oldpassword)
            {
                return Json(new BoolResult { Result = false, Msg = "原密码错误！" });
            }
            //更新密码
            u.Password = newpassword;
            bool b = adminUserSice.Edit(u);
            if (b)
            {
                HttpContext.Session.Remove("userId");
                return Json(new BoolResult { Result = true, Msg = "密码更新成功！" });
            }
            else
            {
                return Json(new BoolResult { Result = false, Msg = "网络异常，请重试！" });
            }
        }
    }
}