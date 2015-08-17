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
using System.ComponentModel;
    
    public partial class Organization
    {
        public Organization()
        {
            this.Beneficiaries = new HashSet<Beneficiary>();
        }
    
        public int OrganizationID { get; set; }
        [DisplayName("Organization")]
        public string Name { get; set; }
        public int CommunityID { get; set; }
        [DisplayName("Year Of Formalization")]
        public string YearOfFormalization { get; set; }
        [DisplayName("Year Of Foundation")]
        public string YearofFoundation { get; set; }
        [DisplayName("Enrolled Members")]
        public Nullable<int> NoOfEnrolledMembers { get; set; }
        [DisplayName("Active Members")]
        public Nullable<int> NoOfActiveMembers { get; set; }
    
        public virtual Community Community { get; set; }
        public virtual ICollection<Beneficiary> Beneficiaries { get; set; }
    }
}