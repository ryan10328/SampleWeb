using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.Models
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext db
        {
            get;
            set;
        }

        public GenericRepository()
            : this(new SampleDbContext())
        {

        }

        public GenericRepository(DbContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("Null DbContext");
            }
            this.db = db;
        }

        public GenericRepository(ObjectContext db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("Null DbContext");
            }
            this.db = new DbContext(db, true);
        }


        public void Create(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("Null Instance");
            }
            else
            {
                this.db.Set<TEntity>().Add(instance);
                this.SaveChanges();
            }
        }

        public void Update(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("Null Instance");
            }
            else
            {
                this.db.Entry<TEntity>(instance).State = EntityState.Modified;
                this.SaveChanges();
            }
        }

        public void Delete(TEntity instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("Null Instance");
            }
            else
            {
                this.db.Entry<TEntity>(instance).State = EntityState.Deleted;
                this.SaveChanges();
            }
        }

        public TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return this.db.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IQueryable<TEntity> GetAll()
        {
            return this.db.Set<TEntity>().AsQueryable();
        }

        public void SaveChanges()
        {
            this.db.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.db != null)
                {
                    this.db.Dispose();
                    this.db = null;
                }
            }
        }
    }
}
