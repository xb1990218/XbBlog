using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    /// <summary>
    /// 后台文章列表页面文章视图模型
    /// </summary>
    public class WzView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string Category { get; set; }
        public int IsTop { get; set; }
        public int VistNum { get; set; }
    }
}
