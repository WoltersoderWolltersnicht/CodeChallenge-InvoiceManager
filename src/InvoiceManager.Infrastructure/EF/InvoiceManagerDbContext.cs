using InvoiceManager.Domain.Businesses;
using InvoiceManager.Domain.InvoiceLines;
using InvoiceManager.Domain.Invoices;
using InvoiceManager.Domain.People;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Infrastructure.DataBase;

public class InvoiceManagerDbContext : DbContext
{
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<InvoiceLine> InvoiceLines { get; set; }
    public DbSet<Business> Business { get; set; }
    public DbSet<Person> People { get; set; }

    public InvoiceManagerDbContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InvoiceLine>(invoiceline =>
        {
            invoiceline.ToTable("LineasFactura");
            invoiceline.Property(il => il.Id).HasColumnName("IDLineaFactura");
            invoiceline.Property(il => il.VAT).HasColumnName("IVA");
            invoiceline.Property(il => il.Amount).HasColumnName("Importe");
        });

        modelBuilder.Entity<Business>(business =>
        {
            business.ToTable("Empresa");
            business.HasAlternateKey(i => i.Name);
            business.HasAlternateKey(i => i.CIF);
            business.Property(il => il.Id).HasColumnName("IdEmpresa");
            business.Property(il => il.Name).HasColumnName("RazonSocial");
            business.Property(il => il.CIF).HasColumnName("CIF");
        });

        modelBuilder.Entity<Person>(person =>
        {
            person.ToTable("Persona");
            person.HasAlternateKey(i => i.NIF);
            person.Property(il => il.Id).HasColumnName("IdPersona");
            person.Property(il => il.Name).HasColumnName("Nombre");
            person.Property(il => il.Surname1).HasColumnName("Apellido1");
            person.Property(il => il.Surname2).HasColumnName("Apellido2");
            person.Property(il => il.NIF).HasColumnName("NIF");
        });

        modelBuilder.Entity<Invoice>(
        invoice =>
        {
            invoice.ToTable("Factura");
            invoice.HasAlternateKey(i => i.GUID);
            invoice.Property(i => i.Id).HasColumnName("IdFactura");
            invoice.Property(i => i.GUID).HasColumnName("GUIDFactura");
            invoice.Property(i => i.Number).HasColumnName("NumFactura");
            invoice.Property(i => i.Amount).HasColumnName("Importe");
            invoice.Property(i => i.VAT).HasColumnName("IVA");

            invoice.HasOne(i => i.Business).WithMany(b => b.Invoices).HasForeignKey("IdEmpresa").IsRequired();
            invoice.HasOne(i => i.Person).WithMany(p => p.Invoices).HasForeignKey("IdPersona");
            invoice.HasMany(i => i.InvoiceLines).WithOne(il => il.Invoice).HasForeignKey("IdFactura").IsRequired();
        });
    }
}

