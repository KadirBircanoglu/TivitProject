using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TivitProject_EL.Entities;
using TivitProject_EL.IdentityModels;

namespace TivitProject_DL.ContextInfo
{
    public class MyContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public MyContext(DbContextOptions<MyContext> opt)
             : base(opt)
        {

        }

        public virtual DbSet<UserTivit> UserTivitTable { get; set; }
        public virtual DbSet<TivitPhoto> TivitPhotoTable { get; set; }
        public virtual DbSet<TivitTags> TivitTagTable { get; set; }
    }
}
