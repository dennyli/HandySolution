using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Configuration;

namespace Lighter.Server.Infrastructure
{
    public class DbContextFactory<TContext> : IDbContextFactory<TContext>, IDisposable where TContext:DbContext,new()
    {
        private string _connectionString;
        private TContext _context;

        public DbContextFactory()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["dbContext"].ConnectionString;
            _context.Database.Connection.ConnectionString = _connectionString;
            _context = Activator.CreateInstance<TContext>();
        }

        public DbContextFactory(string connectionString)
            : this()
        {
            _connectionString = connectionString;
        }

        public TContext GetDbContext()
        {
            return _context == null ? Activator.CreateInstance<TContext>() : _context;
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
