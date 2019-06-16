using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Services.IService;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        //缓存服务
        private IMemoryCache cache;
        //首页访问记录表服务
        private IVisitLogService vistSice;
        //文章表服务
        private IArticleService aceSice;
        //博主信息服务
        private IMasterInfoService masterInfoSice;
        //公共服务
        private ICommonService comSice;

        public HomeController(IMemoryCache memoryCache, IVisitLogService visitLogService, IArticleService articleService,ICommonService commonService, IMasterInfoService masterInfoService)
        {
            cache = memoryCache;
            vistSice = visitLogService;
            aceSice = articleService;
            comSice = commonService;
            masterInfoSice = masterInfoService;
        }
        //博客首页
        public IActionResult Index(int id=0,int curr=1,int limit=12,string keyTitle="")
        {
            //博主信息表
            MasterInfo m=masterInfoSice.GetEntity(a => a.Id == 1);
            //文章分类和对应博客数量
            List<CateNum> listCate=comSice.GetCateAndNum();
            ViewBag.listCate = listCate;

            //根据类别id获取对应的文章信息并且分页，id为0表示获取所有类别文章信息
            List<ArticleIndex> listAce=aceSice.GetIndexWzPage(id, curr, limit, out int totalCount,keyTitle);
            ViewBag.listAce = listAce;
            //总条数
            ViewBag.totalCount = totalCount;
            //当前页
            ViewBag.curr = curr;
            //类别id
            ViewBag.id = id;
            //关键词
            ViewBag.keyTitle = keyTitle;

            return View(m);
        }

        //文章详情页面
        public IActionResult Detail(int id)
        {
            //文章信息
            ArticleIndex art=aceSice.GetArtById(id);
            if (art==null)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.art = art;

            //博主信息表
            MasterInfo m = masterInfoSice.GetEntity(a => a.Id == 1);
            //文章分类和对应博客数量
            List<CateNum> listCate = comSice.GetCateAndNum();
            ViewBag.listCate = listCate;
            return View(m);
        }

        //记录首页访问日志，同一个ip 一个小时只记录一次
        public JsonResult WriteVistLog(string ip,string position)
        {
            if (string.IsNullOrWhiteSpace(ip)||string.IsNullOrWhiteSpace(position))
            {
                return Json(new BoolResult { Result=false});
            }
            if (!cache.TryGetValue($"ip_{ip}",out string cip))//如果缓存里面没有 则写入数据库记录下访问信息
            {
                //写入数据库
                VisitLog vlog = new VisitLog();
                vlog.Ip = ip;
                vlog.Position = position;
                vlog.VistTime = DateTime.Now;
                vistSice.Add(vlog);
                //写入缓存一小时，也就是一个ip 一小时内只记录一次访问首页记录
                cache.Set($"ip_{ip}", ip, DateTimeOffset.Now.AddHours(3));
            }
            return Json(new BoolResult { Result = true});
        }
        //更新文章的阅读量，同一ip20分钟内同一篇文章只算一次点击量
        public JsonResult ArcVistNum(int id,string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                return Json(new BoolResult { Result = false });
            }
            var ace=aceSice.GetEntity(a => a.Id == id);
            if (ace==null)
            {
                return Json(new BoolResult { Result = false });
            }
            if (!cache.TryGetValue($"ArcVistNumIp_{id}_{ip}", out string cip))
            {
                //更新数据库 阅读量加1
                ace.VistNum += 1;
                aceSice.Edit(ace);
                //写入缓存20分钟
                cache.Set($"ArcVistNumIp_{id}_{ip}", ip, DateTimeOffset.Now.AddMinutes(20));
            }
            return Json(new BoolResult { Result = true });
        }
    }
}
