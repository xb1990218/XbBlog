using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    /// <summary>
    /// 文章表实体
    /// </summary>
    public class Article
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Describe { get; set; }

        public string Body { get; set; }

        public DateTime CreateDate { get; set; }

        public int VistNum { get; set; }

        public int CategoryId { get; set; }

        public int IsTop { get; set; }
    }
}
