﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PagosAcademicos.Models.Entities;

public partial class PagosacademicosContext : DbContext
{
    public PagosacademicosContext()
    {
    }

    public PagosacademicosContext(DbContextOptions<PagosacademicosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Carrera> Carrera { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Semestre> Semestre { get; set; }

    public virtual DbSet<TipoPago> TipoPago { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseMySql("server=localhost;user=root;password=root;database=pagosacademicos", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
    
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //            => optionsBuilder.UseMySql("server = sql.freedb.tech;user = freedb_FirtsUser; database = freedb_cliente; password = 7Z%@TYfXYnxjs4b", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pago");

            entity.HasIndex(e => e.TipoPagoId, "fk_pago_tipo_pago1_idx");

            entity.HasIndex(e => e.UsuarioId, "fk_pago_usuario1_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Concepto)
                .HasMaxLength(45)
                .HasColumnName("concepto");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Monto)
                .HasPrecision(2, 2)
                .HasColumnName("monto");
            entity.Property(e => e.TipoPagoId).HasColumnName("tipo_pago_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.TipoPago).WithMany(p => p.Pago)
                .HasForeignKey(d => d.TipoPagoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pago_tipo_pago1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Pago)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pago_usuario1");
        });

        modelBuilder.Entity<Semestre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("semestre");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<TipoPago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipo_pago");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.CarreraId, "fk_usuario_carrera1_idx");

            entity.HasIndex(e => e.SemestreId, "fk_usuario_semestre_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .HasColumnName("apellido");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.Estatus)
                .HasColumnType("bit(1)")
                .HasColumnName("estatus");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.SemestreId).HasColumnName("semestre_id");

            entity.HasOne(d => d.Carrera).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.CarreraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_carrera1");

            entity.HasOne(d => d.Semestre).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.SemestreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_usuario_semestre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
