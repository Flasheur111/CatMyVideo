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
    
    public partial class T_Videos
    {
        public T_Videos()
        {
            this.T_Comments = new HashSet<T_Comments>();
            this.T_Encode = new HashSet<T_Encode>();
            this.T_Tags = new HashSet<T_Tags>();
        }
    
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public System.DateTime upload_date { get; set; }
        public long view_count { get; set; }
        public int uploader { get; set; }
    
        public virtual ICollection<T_Comments> T_Comments { get; set; }
        public virtual ICollection<T_Encode> T_Encode { get; set; }
        public virtual T_Users T_Users { get; set; }
        public virtual ICollection<T_Tags> T_Tags { get; set; }
    }
}