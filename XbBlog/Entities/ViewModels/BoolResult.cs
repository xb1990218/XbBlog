using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.ViewModels
{
    /// <summary>
    /// 请求返回结果模型类
    /// </summary>
    public class BoolResult
    {
        /// <summary>
        /// true成功 false失败,默认为false
        /// </summary>
        public bool Result { get; set; } = false;
        /// <summary>
        /// 返回提示信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 返回状态码，在某些请求备用的
        /// </summary>
        public int State { get; set; } = 0;

        /// <summary>
        /// 返回的数据
        /// </summary>
        public Object Data { get; set; } = null;

        /// <summary>
        /// 返回数据总记录数
        /// </summary>
        public int Total { get; set; } = 0;
    }
}
