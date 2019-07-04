using Entities;
using Entities.Models;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Service
{
    public class CommentService:BaseService<Comment>,ICommentService
    {
        public CommentService(EFContext eFContext)
        {
            db = eFContext;
        }
    }
}
