using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using WebClient.Interface;

namespace WebClient.DataAccess
{
    public class GenericDataAccess<TEntity> : IGenericInterface<TEntity> where TEntity : class
    {
        internal FDBContext FDBContext;
        internal DbSet<TEntity> FDBSet;

        public GenericDataAccess(FDBContext context)
        {
            this.FDBContext = context;
            this.FDBSet = context.Set<TEntity>();
        }

        public virtual int Insert(TEntity entity)
        {
            FDBSet.Add(entity);
            return SaveChanges();
        }

        public virtual int Update(TEntity entity)
        {
            if (FDBContext.Entry(entity).State == EntityState.Detached)
                FDBSet.Attach(entity);

            FDBContext.Entry(entity).State = EntityState.Modified;

            return SaveChanges();
        }

        public virtual int Save(TEntity entity)
        {
            PropertyInfo prop = entity.GetType().GetProperty("Id");
            if (prop != null)
            {
                long Id = (long)prop.GetValue(entity, null);
                if (Id == 0)
                    return this.Insert(entity);
                else
                {
                    return this.Update(entity);
                }
            }
            return 0;
        }

        public virtual int Delete(object id)
        {
            var entity = FDBSet.Find(id);
            return Delete(entity);
        }

        public virtual int Delete(TEntity entity)
        {
            if (FDBContext.Entry(entity).State == EntityState.Detached)
            {
                FDBSet.Attach(entity);
            }

            FDBSet.Remove(entity);
            return SaveChanges();
        }

        public virtual List<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            return FDBSet.SqlQuery(query, parameters).ToList();
        }

        public virtual TEntity GetWithRawSqlSingle(string query, params object[] parameters)
        {
            return FDBSet.SqlQuery(query, parameters).FirstOrDefault();
        }

        public virtual List<TEntity> GetWithLinq(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = FDBSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                 (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }

        public virtual TEntity Get(object Id)
        {
            return FDBSet.Find(Id);
        }

        public virtual List<TEntity> GetAllWithSP(string queryWithParams)
        {
            return FDBContext.Database.SqlQuery<TEntity>(queryWithParams).ToList();
        }

        public virtual int SetAllWithSP(string queryWithParams)
        {
            return FDBContext.Database.ExecuteSqlCommand(queryWithParams);
        }

        public virtual object GetValueWithFunction(string queryWithParams)
        {
            return FDBContext.Database.SqlQuery<object>(queryWithParams).FirstOrDefault();
        }

        public virtual List<TEntity> GetAll()
        {
            return FDBSet.ToList();
        }

        public virtual int SaveChanges()
        {
            try
            {
                return FDBContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}