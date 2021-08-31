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
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Tb_Client
    {
        public Tb_Client()
        {
            this.Tb_Collect = new HashSet<Tb_Collect>();
        }
    
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public byte AreaId { get; set; }
        public byte TypeId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Start { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //[DefaultValue(typeof(DateTime), DateTime.Now.ToString("yyyy-MM-dd"))]
        public Nullable<DateTime> Close { get; set; }
        public decimal MonAmt { get; set; }
        public decimal SrvAmt { get; set; }
        public bool Status { get; set; }
        public string Remark { get; set; }
    
        public virtual Tb_Area Tb_Area { get; set; }
        public virtual ICollection<Tb_Collect> Tb_Collect { get; set; }
        public virtual Tb_Type Tb_Type { get; set; }
    }
}