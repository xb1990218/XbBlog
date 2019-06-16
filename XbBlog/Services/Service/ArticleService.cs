using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities;
using Entities.Models;
using Entities.ViewModels;
using Services.IService;
using CommonLib;
using System.Linq.Expressions;

namespace Services.Service
{
    public class ArticleService:BaseService<Article>,IArticleService
    {
        public ArticleService(EFContext eFContext)
        {
            db = eFContext;
        }
        #region 扩展方法
        /// <summary>
        /// 根据类别id获取对应的文章并且分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="curr"></param>
        /// <param name="limit"></param>
        /// <param name="TotalCount"></param>
        /// <param name="keyTitle"></param>
        /// <returns></returns>
        public List<ArticleIndex> GetIndexWzPage(int id, int curr, int limit, out int TotalCount, string keyTitle = "")
        {
            List<ArticleIndex> list = null;

            Expression<Func<Article, bool>> wherelambda =null;
            List<Expression<Func<Article, bool>>> command = new List<Expression<Func<Article, bool>>>();
            if (id!=0)
            {
                command.Add(a => a.CategoryId == id);
            }
            if (!string.IsNullOrWhiteSpace(keyTitle))
            {
                command.Add(a => a.Title.Contains(keyTitle));
            }
            foreach (var item in command)
            {
                wherelambda = wherelambda == null ? item : wherelambda.And(item);
            }
            if (wherelambda==null)//过滤条件为空表示id为0 并且搜索关键词为空
            {
                var queryable = db.Article.Join(db.Category, ar => ar.CategoryId, ca => ca.Id, (ar, ca) => new ArticleIndex
                {
                    Id = ar.Id,
                    Title = ar.Title,
                    Describe = ar.Describe,
                    Body = ar.Body,
                    CreateDate = ar.CreateDate,
                    VistNum = ar.VistNum,
                    CategoryId = ar.CategoryId,
                    IsTop = ar.IsTop,
                    CateName = ca.Name
                });
                TotalCount = queryable.Count();
                list = queryable.OrderByDescending(a => a.CreateDate).Skip((curr - 1) * limit).Take(limit).ToList();
            }
            else
            {
                var queryable = db.Article.Where(wherelambda).Join(db.Category, ar => ar.CategoryId, ca => ca.Id, (ar, ca) => new ArticleIndex
                {
                    Id = ar.Id,
                    Title = ar.Title,
                    Describe = ar.Describe,
                    Body = ar.Body,
                    CreateDate = ar.CreateDate,
                    VistNum = ar.VistNum,
                    CategoryId = ar.CategoryId,
                    IsTop = ar.IsTop,
                    CateName = ca.Name
                });
                TotalCount = queryable.Count();
                list = queryable.OrderByDescending(a => a.CreateDate).Skip((curr - 1) * limit).Take(limit).ToList();
            }
            return list;
        } 

        //根据文章id获取文章信息及分类名
        public ArticleIndex GetArtById(int id)
        {
            return db.Article.Where(a => a.Id == id).Join(db.Category, ar => ar.CategoryId, ca => ca.Id, (ar, ca) => new ArticleIndex
            {
                Id = ar.Id,
                Title = ar.Title,
                CreateDate = ar.CreateDate,
                Describe = ar.Describe,
                Body = ar.Body,
                CategoryId = ar.CategoryId,
                CateName = ca.Name,
                IsTop = ar.IsTop,
                VistNum = ar.VistNum
            }).FirstOrDefault();
        }
        #endregion
    }
}
