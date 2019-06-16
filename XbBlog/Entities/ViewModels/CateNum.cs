using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    /// <summary>
    /// 首页左侧显示的分类和对应文章数量的视图模型
    /// </summary>
    public class CateNum
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
    }
}
