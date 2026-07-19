using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ElearingEnglis.DataCon.Module;

namespace ElearingEnglis.DataCon;

public class DBCon : IdentityDbContext
{

    public DbSet<Patient> patients {get; set;}
    public DbSet<Appointment> appointments {get; set;}
    public DbSet<Doctor> doctors {get; set;}
    public DbSet<Drug> drugs { get; set; }
    public DbSet<DrugItem> drugItems {get; set;}
    public DbSet<Prescription> prescriptions {get; set;}    
    public DbSet<VitalSignMaster> VitalSignMaster { get; set;}
    public DbSet<VitalSign>VitalSigns { get; set;}
    public DbSet<Diagnos> Diagnoses { get; set; }
    public DbSet<queueMQ> queueMQs { get; set; }

    public DBCon(DbContextOptions<DBCon> options) : base(options){}
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Drug>()
            .Property(d => d.PriceEgp)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Prescription>().HasMany(p => p.Items).WithOne(i => i.Prescription).HasForeignKey(i => i.PrescriptionId);
        modelBuilder.Entity<Prescription>().HasOne(p => p.Doctor).WithMany(d => d.prescriptions);

        modelBuilder.Entity<Appointment>()
         .HasOne(a => a.Prescription)
         .WithOne(p => p.Appointment)
     .HasForeignKey<Prescription>(p => p.AppointmentId);

        modelBuilder.Entity<VitalSignMaster>().HasMany(vm=>vm.VitalSigns).WithOne(v => v.VitalSignMaster).HasForeignKey(v => v.VitalSignMasterId);
    }
    
}
