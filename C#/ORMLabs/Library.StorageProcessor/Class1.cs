using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Library.ORM
{
    public class Context : DbContext
    {
        public Context(
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .UseSqlServer(_configuration.GetConnectionString("SPORT_GOODS_DB"));
        }

        private readonly ILoggerFactory _loggerFactory;
        private readonly IConfiguration _configuration;
    }
}
