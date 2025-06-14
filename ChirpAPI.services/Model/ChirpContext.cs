﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ChirpAPI;

namespace ChirpAPI.Model;

public partial class ChirpContext : DbContext
{
    public ChirpContext()
    {
    }

    public ChirpContext(DbContextOptions<ChirpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chirp> Chirps { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(AppConfig.GetConfigurationString());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chirp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("chirp_pk");

            entity.ToTable("chirps");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationTime)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_time");
            entity.Property(e => e.ExtUrl)
                .HasMaxLength(2083)
                .HasColumnName("ext_url");
            entity.Property(e => e.Lat).HasColumnName("lat");
            entity.Property(e => e.Lng).HasColumnName("lng");
            entity.Property(e => e.Text)
                .HasMaxLength(140)
                .HasColumnName("text");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("comment_pk");

            entity.ToTable("comments");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('commets_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.ChirpId).HasColumnName("chirp_id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("creation_date");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Text)
                .HasMaxLength(140)
                .HasColumnName("text");

            entity.HasOne(d => d.Chirp).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ChirpId)
                .HasConstraintName("chirp_fk");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("parent_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
