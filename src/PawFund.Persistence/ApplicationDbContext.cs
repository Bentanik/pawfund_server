﻿using PawFund.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PawFund.Persistence;
public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() { }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
        => builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);

    public DbSet<Product> Products { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<RoleUser> RoleUsers { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventActivity> EventsActivities { get; set; }
    public DbSet<VolunteerApplicationDetail> VolunteerApplicationDetails { get; set; }
    public DbSet<AdoptPetApplication> AdoptPetApplications { get; set; }
    public DbSet<Cat> Cats { get; set; }
    public DbSet<HistoryCat> HistoryCats { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<ImageCat> ImageCats { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<ChatHistory> ChatHistories { get; set; }
}