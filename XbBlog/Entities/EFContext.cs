using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class EFContext : DbContext
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {

        }

        #region 实体集
        public DbSet<Article> Article { get; set; } //注意 这里名字和实体名字必须一致
        public DbSet<MasterInfo> MasterInfo { get; set; }
        public DbSet<AdminUser> AdminUser { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<VisitLog> VisitLog { get; set; }
        #endregion
    }
}
