using System;
using System.Collections.Generic;
using BackendServices.DataAccess.Models.USERMANAGEMENT;
using Microsoft.EntityFrameworkCore;

namespace BackendServices.DataAccess.Context;

public partial class USERMANAGEMENTContext : DbContext
{
    public USERMANAGEMENTContext(DbContextOptions<USERMANAGEMENTContext> options)
        : base(options)
    {
    }

    public virtual DbSet<users> users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<users>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__users__3213E83FCB158AEF");

            entity.HasIndex(e => e.email, "users_email_unique").IsUnique();

            entity.Property(e => e.created_at).HasColumnType("datetime");
            entity.Property(e => e.email).HasMaxLength(255);
            entity.Property(e => e.email_verified_at).HasColumnType("datetime");
            entity.Property(e => e.name).HasMaxLength(255);
            entity.Property(e => e.password).HasMaxLength(255);
            entity.Property(e => e.remember_token).HasMaxLength(100);
            entity.Property(e => e.updated_at).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
