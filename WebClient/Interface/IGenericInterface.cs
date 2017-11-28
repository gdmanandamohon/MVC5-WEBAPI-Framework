using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.Interface
{
    public interface IGenericInterface<TEntity> where TEntity : class
    {
        int Insert(TEntity entity);
        int Update(TEntity entity);
        int Save(TEntity entity);
        int Delete(object id);
        int Delete(TEntity entity);
        List<TEntity> GetAllWithSP(string queryWithParams);
        object GetValueWithFunction(string queryWithParams);
        List<TEntity> GetWithRawSql(string query, params object[] parameters);
        TEntity Get(object Id);
        List<TEntity> GetAll();
        List<TEntity> GetWithLinq(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>,
        IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        //PagingModel<TEntity> GetAllPaged(int pagesize, NameValueCollection queryString, string sortPreference);
    }
}