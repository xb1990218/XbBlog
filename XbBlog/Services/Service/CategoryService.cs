using System;
using System.Collections.Generic;
using System.Text;
using Entities;
using Entities.Models;
using Services.IService;

namespace Services.Service
{
    public class CategoryService:BaseService<Category>,ICategoryService
    {
        public CategoryService(EFContext eFContext)
        {
            db = eFContext;
        }
    }
}
