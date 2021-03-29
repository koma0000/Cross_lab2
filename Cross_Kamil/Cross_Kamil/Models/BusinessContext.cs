using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cross_Kamil.Models;

namespace Cross_Kamil.Models
{
    public class BusinessContext : DbContext
    {
        public BusinessContext(DbContextOptions<BusinessContext> options)
           : base(options)
        {
        }

        public DbSet<Businessmen> Businessmens { get; set; }
        public DbSet<Company> Companys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Business>()
                .HasKey(t => new { t.BusinessmenId, t.CompanyId });

            modelBuilder.Entity<Business>()
                .HasOne(sc => sc.Businessmen)
                .WithMany(s => s.Companies)
                .HasForeignKey(sc => sc.BusinessmenId);

            modelBuilder.Entity<Business>()
                .HasOne(sc => sc.Company)
                .WithMany(c => c.Businessmens)
                .HasForeignKey(sc => sc.CompanyId);
        }

 public Dictionary<string, List<string>> GetBusinessmenOfCompany()
        {
            Companys.Include(c => c.Businessmens).ThenInclude(sc => sc.Company).ToList();

            Dictionary<string, List<string>> buf = new Dictionary<string, List<string>>();

            foreach (var c in Companys)
            {
                List<string> bussines_names = new List<string>();
                var bussines = c.Businessmens.Select(sc => sc.Company).ToList();
                foreach (Company a in bussines)
                
                    bussines_names.Add(a.Name);
                

                buf.Add(c.Name, bussines_names);                  

            }
            return buf;
        }
    }
}
