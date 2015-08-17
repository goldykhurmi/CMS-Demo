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
using System.ComponentModel.DataAnnotations;
    
    public partial class Beneficiary
    {
        public Beneficiary()
        {
            this.SessionParticipants = new HashSet<SessionParticipant>();
            this.Organizations = new HashSet<Organization>();
            this.ProductiveSectors = new HashSet<ProductiveSector>();
        }
        
        public int BeneficiaryID { get; set; }
        public string IdCardNumber { get; set; }
        [Required(ErrorMessage="Name is Required")]
        [DisplayName("Name")]
        public string Names { get; set; }
        [DisplayName("Last Name")]
        public string LastNames { get; set; }
        public string Sex { get; set; }
        public string Address { get; set; }
       
        public int CommunityID { get; set; }
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage= "Please enter a valid email")]
        public string Email { get; set; }
        [DisplayName("Date")]
        public System.DateTime DateofRecord { get; set; }
        [DisplayName("Photo")]
        public string ImagePath { get; set; }
        public Nullable<int> TypeOfBeneficiaryID { get; set; }
    
        public virtual Community Community { get; set; }
        public virtual TypeOfBeneficiary TypeOfBeneficiary { get; set; }
        public virtual ICollection<SessionParticipant> SessionParticipants { get; set; }
        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<ProductiveSector> ProductiveSectors { get; set; }
    }
}