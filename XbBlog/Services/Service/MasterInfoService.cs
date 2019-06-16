using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.Models;
using Services.IService;

namespace Services.Service
{
    public class MasterInfoService:BaseService<MasterInfo>,IMasterInfoService
    {
        public MasterInfoService(EFContext eFContext)
        {
            db = eFContext;
        }
    }
}
