using Demo.Core.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Context
{
    public class DatabaseInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DatabaseInitializer> _logger;

        public DatabaseInitializer(ApplicationDbContext context, ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            if (_context.Database.GetMigrations().Any())
            {
                if ((await _context.Database.GetPendingMigrationsAsync()).Any())
                {
                    _logger.LogInformation("Applying Migrations....");
                    await _context.Database.MigrateAsync();
                }

                if (await _context.Database.CanConnectAsync())
                {
                    _logger.LogInformation("Connection to Database Succeeded.");

                    await SeedDatabaseAsync();
                }
            }
        }

        private async Task SeedDatabaseAsync()
        {
            if (!_context.Users.Any())
            {
                _logger.LogInformation("Start Seeding Data...");
                var user = new User
                {
                    Name = "Captain American",
                    Email = "captain@email.com",
                    Password = "password",
                    PhoneNumber = "1234567890",
                    CreateAt = DateTime.UtcNow,
                    UpdateAt = DateTime.UtcNow,
                };
                _context.Add(user);
                await _context.SaveChangesAsync();
                _logger.LogInformation("End Seeding Data...");
            }
        }
    }
}
