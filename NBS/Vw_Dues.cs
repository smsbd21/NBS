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
    using System.Collections.Generic;
    
    public partial class Vw_Dues
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal MonAmt { get; set; }
        public Nullable<System.DateTime> FMon { get; set; }
        public Nullable<System.DateTime> TMon { get; set; }
        public int TotMon { get; set; }
        public int TotAmt { get; set; }
    }
}