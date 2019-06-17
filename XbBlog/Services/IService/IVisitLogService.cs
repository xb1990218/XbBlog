using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.IService
{
    public interface IVisitLogService:IBaseService<VisitLog>
    {
        /// <summary>
        /// 获取首页访问记录-带分页 搜索
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="bedate"></param>
        /// <param name="positionKey"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<VisitLog> GetVisitLogsPage(int page, int limit, string bedate, string positionKey, out int totalCount);
    }
}
