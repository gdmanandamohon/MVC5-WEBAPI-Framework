using Core.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using WebClient.Configuration;
using WebClient.Interface;

namespace WebClient.Service
{
    public class GenericService<TEntity> : BaseService where TEntity : class
    {
        protected T GetInstance<T>() where T : IGenericInterface<TEntity>
        {
            var repository = ServiceLocator.GetInstance<T>();

            return repository;
        }

        public virtual Response<int> Insert(TEntity entity)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.Insert(entity));
            return result;
        }

        public virtual Response<int> Update(TEntity entity)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.Update(entity));
            return result;
        }

        public virtual Response<int> Save(TEntity entity)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.Save(entity));
            return result;
        }

        public virtual Response<int> Delete(object id)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.Delete(id));
            return result;
        }

        public virtual Response<int> Delete(TEntity entity)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.Delete(entity));
            return result;
        }

        public virtual Response<int> DeleteAll(string ids)
        {
            char[] delimiterChars = { ',' };
            try
            {
                var result = new Response<int>();
                if (ids != null && !string.IsNullOrEmpty(ids))
                {
                    foreach (var id in ids.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries))
                    {
                        var repository = GetInstance<IGenericInterface<TEntity>>();
                        result = SafeExecute(() => repository.Delete(long.Parse(id)));
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                return new Response<int>();
            }
        }
        public virtual List<TEntity> GetWithRawSql(string query, params object[] parameters)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.GetWithRawSql(query, parameters));
            return result.Data;
        }

        public virtual List<TEntity> GetAllWithLinq(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.GetWithLinq(filter, orderBy, includeProperties));
            return result.Data;
        }

        public virtual TEntity Get(object id)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.Get(id));
            return result.Data;
        }

        public virtual List<TEntity> GetAllWithSP(string queryWithParams)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.GetAllWithSP(queryWithParams));
            return result.Data;
        }

        public virtual object GetValueWithFunction(string queryWithParams)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.GetValueWithFunction(queryWithParams));
            return result.Data;
        }

        public virtual List<TEntity> GetAll()
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.GetAll());
            return result.Data;
        }

        /*public virtual PagingModel<TEntity> GetAllPaged(int pagesize, NameValueCollection queryString, string sortPreference)
        {
            var repository = GetInstance<IGenericInterface<TEntity>>();
            var result = SafeExecute(() => repository.GetAllPaged(pagesize, queryString, sortPreference));
            return result.Data;
        }*/

    }
}