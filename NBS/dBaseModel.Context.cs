//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NBS
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DbCon : DbContext
    {
        public DbCon()
            : base("name=DbCon")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Tb_Area> Tb_Area { get; set; }
        public virtual DbSet<Tb_Client> Tb_Client { get; set; }
        public virtual DbSet<Tb_Roles> Tb_Roles { get; set; }
        public virtual DbSet<Tb_Type> Tb_Type { get; set; }
        public virtual DbSet<Tb_Users> Tb_Users { get; set; }
        public virtual DbSet<Tb_Collect> Tb_Collect { get; set; }
        public virtual DbSet<Vw_Dues> Vw_Dues { get; set; }
        public virtual DbSet<Vw_Paid> Vw_Paid { get; set; }
    
        public virtual ObjectResult<Sp_Login_Result> Sp_Login(string usr, string pwd)
        {
            var usrParameter = usr != null ?
                new ObjectParameter("usr", usr) :
                new ObjectParameter("usr", typeof(string));
    
            var pwdParameter = pwd != null ?
                new ObjectParameter("pwd", pwd) :
                new ObjectParameter("pwd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Sp_Login_Result>("Sp_Login", usrParameter, pwdParameter);
        }
    }
}
