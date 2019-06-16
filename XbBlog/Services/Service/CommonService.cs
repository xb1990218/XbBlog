using Entities;
using Entities.Models;
using Entities.ViewModels;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Service
{
    public class CommonService : ICommonService
    {
        private EFContext db;

        public CommonService(EFContext eFContext)
        {
            db = eFContext;
        }

        //后台文章列表管理页面获取文章列表视图数据
        public List<WzView> GetWzListPage(int page,int limit,string titleKey,out int totalCount)
        {
            IQueryable<Article> queryable = db.Article.Where(a=>true);
            if (!string.IsNullOrWhiteSpace(titleKey))
            {
                queryable=db.Article.Where(a => a.Title.Contains(titleKey));
            }
            //总条数
            totalCount = queryable.Count();
            //取分页数据并且连分类表
            var list=queryable.OrderByDescending(a => a.CreateDate).Skip((page - 1) * limit).Take(limit).Join(db.Category, art => art.CategoryId, ctg => ctg.Id, (art, ctg) => new WzView
            {
                Id = art.Id,
                Title = art.Title,
                CreateDate = art.CreateDate,
                Category = ctg.Name,
                IsTop = art.IsTop,
                VistNum = art.VistNum
            }).ToList();
            return list;
        }

        //获取所有分类和对应的文章数量返回视图list
        public List<CateNum> GetCateAndNum()
        {
            var list=db.Category.GroupJoin(db.Article, c => c.Id, a => a.CategoryId, (c, a) => new CateNum { Id = c.Id, Name = c.Name, Num = a.Count() }).ToList();
            return list;
        }
    }
}
