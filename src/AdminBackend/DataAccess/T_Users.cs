//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminBackend.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_Users
    {
        public T_Users()
        {
            this.AspNetUsers = new HashSet<AspNetUsers>();
            this.T_Comments = new HashSet<T_Comments>();
            this.T_Videos = new HashSet<T_Videos>();
        }
    
        public int id { get; set; }
        public string nickname { get; set; }
        public string description { get; set; }
        public string AspNetUsersId { get; set; }
        public string pass { get; set; }
    
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual AspNetUsers AspNetUsers1 { get; set; }
        public virtual ICollection<T_Comments> T_Comments { get; set; }
        public virtual ICollection<T_Videos> T_Videos { get; set; }
    }
}
