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

        public void BusinessmensInclude()
        {
            Businessmens.Include(c => c.Companies).ThenInclude(sc => sc.Company).ToList();
        }

        public void ComponyInclude()
        {
            Companys.Include(c => c.Businessmens).ThenInclude(sc => sc.Businessmen).ToList();
        }

         public string SetBussinesmenOfComapany( long bussnesmenId, long companyId)
        {
            
            foreach (var u in Businessmens)
            {
                
                if (u.Id == bussnesmenId)
                {
                    try
                    {
                        u.Companies.Add(new Business { CompanyId = companyId, BusinessmenId = u.Id });
                        SaveChanges();
                        return "Okey";
                    }
                    catch
                    {
                        return "Error";
                    }
                    
                }
                
            }

            return "Error. Not Found this";
        }

        public Dictionary<string, List<string>> GetBusinessmenOfCompany()
        {
            //Companys.Include(c => c.Businessmens).ThenInclude(sc => sc.Businessmen).ToList();

            //Dictionary<string, List<string>> buf = new Dictionary<string, List<string>>();

            //foreach (var c in Companys)
            //{
            //    List<string> com_names = new List<string>();
            //    var com = c.Businessmens.Select(sc => sc.Businessmen).ToList();
            //    foreach (Businessmen b in com)
            //     com_names.Add(b.Surname);

            //    buf.Add(c.Name, com_names);
            //    long k = com_names.Count();
            //}

            //return buf;


            return new Dictionary<string, List<string>>
            (Companys.Include(c => c.Businessmens).ThenInclude(sc => sc.Businessmen).ToList()
            .Select(b => new KeyValuePair<string, List<string>>
                (b.Name,
                b.Businessmens.Select(c => c.Businessmen.Surname)
                .ToList()))
            );

        }

        public Dictionary<string, List<string>> GetCompanyOfBusinessmen()
        {
            return new Dictionary<string, List<string>>
                (Businessmens.Include(c => c.Companies).ThenInclude(sc => sc.Company).ToList()
                .Select(b => new KeyValuePair<string, List<string>>
                    (b.Surname, 
                    b.Companies.Select(c => c.Company.Name)
                    .ToList()))
                );

            //Dictionary<string, List<string>> buf = new Dictionary<string, List<string>>();

            //foreach (var b in Businessmens)
            //{
            //    List<string> bus_names = new List<string>();
            //    var com = b.Companies.Select(sc => sc.Company).ToList();
            //    foreach (Company c in com)
            //        bus_names.Add(c.Name);

            //    buf.Add(b.Surname, bus_names);
               
            //}

            //return buf;

        }



        public Dictionary<string, long> GetForebs()
        {
            Businessmens.Include(c => c.Companies).ThenInclude(sc => sc.Company).ToList();

            Dictionary<string, long> buf = new Dictionary<string, long>();


            foreach (var b in Businessmens)
            {
                List<long> profits = new List<long>();
                List<long> kolvo = new List<long>();

                var com = b.Companies.Select(sc => sc.Company).ToList();
                foreach (Company c in com)
                    profits.Add(c.Profit);

                long prof = profits.ToArray().Sum();
                long k = profits.Count();

                buf.Add(b.Surname, prof);
                
            }


            return buf;


            //return new Dictionary<string, long>
            //   (Businessmens.Include(c => c.Companies).ThenInclude(sc => sc.Company).ToList()
            //   .Select(b => new KeyValuePair<string,long>
            //       (b.Surname,
            //       b.Companies.Select(
            //       .ToList()))
            //   );

        }
     

    }
}



