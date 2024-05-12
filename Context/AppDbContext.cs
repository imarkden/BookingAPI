using Microsoft.EntityFrameworkCore;
using SaintJohnDentalClinicApi.Models.DTO;
using SaintJohnDentalClinicApi.Models.Entity;

namespace SaintJohnDentalClinicApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<AdminAccount> AdminAccounts { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DoctorAccount> DoctorAccounts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<OtpCode> OtpCodes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminAccount>().ToTable("AdminAccounts");
            modelBuilder.Entity<Appointment>().ToTable("Appointments");
            modelBuilder.Entity<Book>().ToTable("Books");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<DoctorAccount>().ToTable("DoctorAccounts");
            modelBuilder.Entity<Service>().ToTable("Services");
            modelBuilder.Entity<SuperAdmin>().ToTable("SuperAdmin");
            modelBuilder.Entity<OtpCode>().ToTable("Otps");
        }
    }
}
