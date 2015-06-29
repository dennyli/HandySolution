using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using Utility;

namespace Lighter.Server.Infrastructure
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext,new()
    {
        private IDbContextFactory<TContext> _dbContextFactory;
        private TContext _context;

        public UnitOfWork(IDbContextFactory<TContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = _dbContextFactory.GetDbContext();
        }

        public TContext Context
        {
            get
            {
                return _context ?? (_context = _dbContextFactory.GetDbContext());
            }
        }

        public bool Commit(out string error_message)
        {
            error_message = string.Empty;
            bool bSuccess = false;
            try
            {
                Context.SaveChanges();
                bSuccess = true;
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                IEnumerable<DbEntityValidationResult> results = ex.EntityValidationErrors;
                foreach (DbEntityValidationResult result in results)
                {
                    foreach (DbValidationError error in result.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("Entity: {0} Property: {1} ErrorMessage: {2}",
                            result.Entry.Entity.GetType().ToString(), error.PropertyName, error.ErrorMessage));
                    }
                }

                // Todo: 错误信息详细化 可视化
                error_message = sb.ToString();
            }
            catch (DbUpdateException ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(ex.Message);
                IEnumerable<DbEntityEntry> results = ex.Entries;
                foreach (DbEntityEntry result in results)
                    sb.AppendLine(result.Entity.GetType().ToString());

                // Todo: 错误信息详细化 可视化
                error_message = sb.ToString();
            }
            catch (Exception ex)
            {
                // Todo: 错误信息详细化 可视化
                error_message = CommonUtility.GetErrorMessageFromException(ex);
            }
            finally
            {
            }

            return bSuccess;
        }
    }
}
