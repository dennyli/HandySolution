using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Linq.Expressions;

namespace Lighter.Server.Infrastructure
{
    public class GenericRepository<TContext, TEntity> : IGenericRepository<TEntity>
        where TContext : DbContext, new() where TEntity : class
    {
        protected TContext _context;
        protected IDbContextFactory<TContext> _dbContextFactory;
        protected readonly IDbSet<TEntity> _set;

        protected GenericRepository(IDbContextFactory<TContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
            _context = _dbContextFactory.GetDbContext();
            _set = _context.Set<TEntity>();
        }

        protected TContext Context
        {
            get
            {
                return _context;
            }
        }

        protected IDbSet<TEntity> DbSet
        {
            get
            {
                return _set == null ? _context.Set<TEntity>() : _set;
            }
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public IQueryable<TEntity> GetPaged(out int total, Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _reset = Get(filter, orderBy, includeProperties);
            _reset = skipCount == 0 ? _reset.Take(size) : _reset.Skip(skipCount).Take(size);
            total = _reset.Count();
            return _reset.AsQueryable();
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include<TEntity>(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).AsQueryable();
            }
            else
            {
                return query.AsQueryable();
            }
        }


        public IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate).AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> Filter<Key>(Expression<Func<TEntity, bool>> filter, out int total, int index = 0, int size = 50)
        {
            int skipCount = index * size;
            var _resetSet = filter != null ? DbSet.Where(filter).AsQueryable() : DbSet.AsQueryable();
            _resetSet = skipCount == 0 ? _resetSet.Take(size) : _resetSet.Skip(skipCount).Take(size);
            total = _resetSet.Count();
            return _resetSet.AsQueryable();
        }

        public bool Contains(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Count(predicate) > 0; ;
        }

        public virtual TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault<TEntity>(predicate);
        }

        public virtual void Create(TEntity t)
        {
            DbSet.Add(t);
        }

        public virtual void Delete(TEntity t)
        {
            if (Context.Entry<TEntity>(t).State == EntityState.Detached)
            {
                DbSet.Attach(t);
            }
            DbSet.Remove(t);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var toDelete = Filter(predicate);
            foreach (var obj in toDelete)
            {
                DbSet.Remove(obj);
            }
        }

        public void Update(TEntity t)
        {
            var entry = Context.Entry<TEntity>(t);
            DbSet.Attach(t);
            entry.State = EntityState.Modified;
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public int Count
        {
            get { return DbSet.Count(); }
        }

        public IQueryable<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return Context.Database.SqlQuery<TEntity>(query, parameters).AsQueryable();
        }

        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
