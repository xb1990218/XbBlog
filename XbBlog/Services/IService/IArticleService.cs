using System;
using System.Collections.Generic;
using System.Text;
using Entities.Models;
using Entities.ViewModels;

namespace Services.IService
{
    /// <summary>
    /// 文章表服务接口
    /// </summary>
    public interface IArticleService:IBaseService<Article>
    {
        /// <summary>
        /// 根据类别id获取对应的文章并且分页
        /// </summary>
        /// <param name="id"></param>
        /// <param name="curr"></param>
        /// <param name="limit"></param>
        /// <param name="TotalCount"></param>
        /// <param name="keyTitle"></param>
        /// <returns></returns>
        List<ArticleIndex> GetIndexWzPage(int id, int curr, int limit, out int TotalCount, string keyTitle = "");
        /// <summary>
        /// 根据文章id获取文章信息及分类名
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ArticleIndex GetArtById(int id);
    }
}
