namespace FinancialPortal.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Household
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PrimaryUserId { get; set; }


        public Household()
        {
            User = new HashSet<ApplicationUser>();
            Budgets = new HashSet<Budget>();
            Invites = new HashSet<Invite>();
            PersonalAccounts = new HashSet<PersonalAccount>();
            Categories = new HashSet<Category>();
        }


        public virtual ICollection<ApplicationUser> User { get; set; }
        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Invite> Invites { get; set; }
        public virtual ICollection<PersonalAccount> PersonalAccounts { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}
