//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMSProject.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Project
    {
        public Project()
        {
            this.Sessions = new HashSet<Session>();
        }
    
        public int ProjectID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Session> Sessions { get; set; }
    }
}