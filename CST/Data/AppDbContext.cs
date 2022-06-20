using CST.Models.EBook;
using CST.Models.Master;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CST.Data
{
    public class AppDbContext : IdentityDbContext<M_User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<M_User> M_User { get; set; } 
        public DbSet<M_Jabatan> M_Jabatan { get; set; }
        public DbSet<M_Unit> M_Unit { get; set; }
        public DbSet<M_Nasabah> M_Nasabah { get; set; } 
        public DbSet<M_Bab> M_Bab{ get; set; }
        public DbSet<M_SubSubMenu> M_SubSubBab { get; set; } 
        public DbSet<M_SubMenu> M_SubBab { get; set; }
        //public DbSet<T_Laporan> T_Laporan { get; set; } 
        public DbSet<T_RumusanNasabah> T_RumusanNasabah{ get; set; }
        public DbSet<T_RumusanBab> T_RumusanBab { get; set; }
        public DbSet<T_RumusanSubBab> T_RumusanSubBab { get; set; }
        public DbSet<T_RumusanSubSubBab> T_RumusanSubSubBab { get; set; }
        public DbSet<T_Transaksi> T_Transaksi { get; set; }
        public DbSet<T_TransDetail> T_TransDetail { get; set; }
        public DbSet<T_TransBab> T_TransBab { get; set; }
        public DbSet<T_TransSubBab> T_TransSubBab { get; set; }
        public DbSet<T_TransSubSubBab> T_TransSubSubBab { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
