//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Engine.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_ViewCount
    {
        public int id { get; set; }
        public System.DateTime date { get; set; }
        public long count { get; set; }
        public int video { get; set; }
    
        public virtual T_Videos T_Videos { get; set; }
    }
}