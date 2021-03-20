using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Library.StorageProcessor.Model;

namespace Library.StorageProcessor
{
    public class Context : DbContext
    {
        public DbSet<GoodType> GoodTypes { get; private set; } = null!;

        public DbSet<Good> Goods { get; private set; } = null!;

        public DbSet<Provider> Providers { get; private set; } = null!;

        public DbSet<GoodsProviders> GoodsProviders { get; private set; } = null!;

        public Context(
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            DbContextOptions<Context> options)
            :base(options)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

            Database.OpenConnection();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .UseSqlServer(_configuration.GetConnectionString("SPORT_GOODS_DB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GoodType>()
                .ToTable("Types")
                .HasKey(g => g.ID);

            modelBuilder.Entity<Good>()
                .ToTable("Goods")
                .HasKey(g => g.ID);

            modelBuilder.Entity<Provider>()
                .ToTable("Providers")
                .HasKey(g => g.ID);

            modelBuilder.Entity<GoodsProviders>()
                .ToTable("GoodsProviders")
                .HasKey(g => g.ID);

            modelBuilder.Entity<GoodType>()
                .HasMany(g => g.Goods)
                .WithOne(t => t.GoodType);

            modelBuilder.Entity<Good>()
                .HasMany(g => g.GoodsProviders)
                .WithOne(t => t.Good);

            modelBuilder.Entity<Provider>()
                .HasMany(g => g.GoodsProviders)
                .WithOne(t => t.Provider);
        }

        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;
    }
}