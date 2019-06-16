using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    /// <summary>
    /// 博主信息表
    /// </summary>
    public class MasterInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SigName { get; set; }
        public string HeadImg { get; set; }
    }
}
