using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    /// <summary>
    /// 首页文章视图-再文章表基础上加一个所属类别名称
    /// </summary>
    public class ArticleIndex
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Describe { get; set; }

        public string Body { get; set; }

        public DateTime CreateDate { get; set; }

        public int VistNum { get; set; }

        public int CategoryId { get; set; }

        public int IsTop { get; set; }

        public string CateName { get; set; }
    }
}
