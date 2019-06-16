using Entities;
using Entities.Models;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Service
{
    public class VisitLogService:BaseService<VisitLog>, IVisitLogService
    {
        public VisitLogService(EFContext eFContext)
        {
            db = eFContext;
        }
    }
}
