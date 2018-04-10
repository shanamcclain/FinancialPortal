﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FinancialPortal.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string DisplayName { get; set; }
        public string ProfilePic { get; set; }
        public string InviteEmail { get; set; }
        public int HouseHoldId { get; set; }

        public virtual ICollection<Household> Households { get; set; }
        public virtual ICollection<PersonalAccount> PersonalAccounts { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Invite> Invites { get; set; }

        public ApplicationUser()
        {
            PersonalAccounts = new HashSet<PersonalAccount>();
            Transactions = new HashSet<Transaction>();
            Invites = new HashSet<Invite>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            if (DisplayName != null)
            {
                userIdentity.AddClaim(new Claim("Name", DisplayName));
            }
            if (ProfilePic != null)
            {
                userIdentity.AddClaim(new Claim("Image", ProfilePic));
            }
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public virtual DbSet<BudgetItem> BudgetItems { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Household> Households { get; set; }
        public virtual DbSet<Invite> Invites { get; set; }
        public virtual DbSet<PersonalAccount> PersonalAccounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

    }
}