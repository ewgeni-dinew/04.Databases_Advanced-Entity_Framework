﻿using Microsoft.EntityFrameworkCore;
using Stations.Data.EntityConfig;
using Stations.Models;

namespace Stations.Data
{
	public class StationsDbContext : DbContext
	{
		public StationsDbContext()
		{
		}

		public StationsDbContext(DbContextOptions options)
			: base(options)
		{
		}

        public DbSet<Station> Stations { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<CustomerCard> Cards { get; set; }
        public DbSet<SeatingClass> SeatingClasses { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TrainSeat> TrainSeats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(Configuration.ConnectionString);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            modelBuilder.ApplyConfiguration(new StationConfig());
            modelBuilder.ApplyConfiguration(new TrainConfig());
            modelBuilder.ApplyConfiguration(new TrainSeatConfig());
            modelBuilder.ApplyConfiguration(new SeatingClassConfig());
            modelBuilder.ApplyConfiguration(new TicketConfig());
            modelBuilder.ApplyConfiguration(new TripCongif());
            modelBuilder.ApplyConfiguration(new CustomerConfig());
        }
	}
}