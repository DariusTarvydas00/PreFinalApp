﻿using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class MainDbContext(DbContextOptions<MainDbContext> options) : DbContext(options)
{
    public DbSet<Note?> Notes { get; set; }
    public DbSet<User?> Users { get; set; }
    public DbSet<Category?> Categories { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Note>()
            .HasOne(n => n.Category)
            .WithMany(c => c.Notes)
            .HasForeignKey(n => n.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Note>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}