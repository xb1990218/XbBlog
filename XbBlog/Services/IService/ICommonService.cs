using Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IService
{
    /// <summary>
    /// 公用接口-主要实现写sql的连表查返回自定义的实体结果，或者自己懒得见service时候可以直接在这里写
    /// </summary>
    public interface ICommonService
    {
        /// <summary>
        /// 后台文章列表管理页面获取文章列表视图数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="titleKey"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<WzView> GetWzListPage(int page, int limit, string titleKey, out int totalCount);
        /// <summary>
        /// 获取所有分类和对应的文章数量返回视图list
        /// </summary>
        /// <returns></returns>
        List<CateNum> GetCateAndNum();
    }
}
