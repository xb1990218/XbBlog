using CommonLib;
using Entities;
using Entities.Models;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Services.Service
{
    public class VisitLogService:BaseService<VisitLog>, IVisitLogService
    {
        public VisitLogService(EFContext eFContext)
        {
            db = eFContext;
        }

        //获取首页访问记录-带分页 搜索
        public List<VisitLog> GetVisitLogsPage(int page, int limit, string bedate, string positionKey,out int totalCount)
        {
            Expression<Func<VisitLog, bool>> wherelambda = a=>true;
            List<Expression<Func<VisitLog, bool>>> command = new List<Expression<Func<VisitLog, bool>>>();
            if (!string.IsNullOrWhiteSpace(positionKey))//省市关键词填写了
            {
                command.Add(a => a.Position.Contains(positionKey));
            }
            if (!string.IsNullOrWhiteSpace(bedate))//当日期不为空  也就是说前端搜索填了日期，那要过滤下日期
            {
                var arryDate = bedate.Split(" - ");
                DateTime bgDate = Convert.ToDateTime(arryDate[0]);//开始日期
                DateTime edDate = Convert.ToDateTime(arryDate[1]);//结束日期
                command.Add(a => a.VistTime >= bgDate && a.VistTime <= edDate);
            }

            foreach (var item in command)//遍历条件  组合到表达式树上
            {
                //wherelambda = wherelambda == null ? item : wherelambda.And(item);
                wherelambda= wherelambda.And(item);
            }

            var queryable = db.VisitLog.Where(wherelambda);
            totalCount = queryable.Count();
            List< VisitLog > list= queryable.OrderByDescending(a => a.VistTime).Skip((page - 1) * limit).Take(limit).ToList();
            return list;
        }
    }
}
