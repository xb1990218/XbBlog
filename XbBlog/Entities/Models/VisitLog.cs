using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    /// <summary>
    /// 博客首页访问记录表
    /// </summary>
    public class VisitLog
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Position { get; set; }
        public DateTime VistTime { get; set; }
    }
}
