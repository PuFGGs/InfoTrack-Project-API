using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;

namespace WebScrapper.DL;

public partial class WebScrapperContext : DbContext
{
    public IConfiguration Configuration { get; }
    public WebScrapperContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public virtual DbSet<Search> Searches { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Search>(entity =>
        {
            entity.ToTable("Searches");
            entity.HasKey(x => x.Id);

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Rank).HasMaxLength(255);
            entity.Property(e => e.SearchPhrase).HasMaxLength(255);
            entity.Property(e => e.Url).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
