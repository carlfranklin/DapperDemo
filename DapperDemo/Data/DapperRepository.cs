using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;

namespace DapperDemo.Data
{
    public class DapperRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DataContext<TEntity> _dataContext;
        private string _sqlConnectionString;
        private string entityName;
        private string primaryKeyName;
        private string primaryKeyType;
        private bool PKNotIdentity = false;
        private Type entityType;

        public DapperRepository(DataContext<TEntity> dataContext, string sqlConnectionString)
        {
            _dataContext = dataContext;
            _sqlConnectionString = sqlConnectionString;
            entityType = typeof(TEntity);
            entityName = entityType.Name;

            var props = entityType.GetProperties().Where(
                prop => Attribute.IsDefined(prop,
                typeof(KeyAttribute)));
            if (props.Count() > 0)
            {
                primaryKeyName = props.First().Name;
                primaryKeyType = props.First().PropertyType.Name;
            }
            else
            {
                // Default
                primaryKeyName = "Id";
                primaryKeyType = "Int32";
            }

            // look for [ExplicitKey]
            props = entityType.GetProperties().Where(
                prop => Attribute.IsDefined(prop,
                typeof(ExplicitKeyAttribute)));
            if (props.Count() > 0)
            {
                PKNotIdentity = true;
                primaryKeyName = props.First().Name;
                primaryKeyType = props.First().PropertyType.Name;
            }
        }

        public async Task<bool> Delete(TEntity entityToDelete)
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString))
            {
                //string sql = $"delete from {entityName} where {primaryKeyName}" +
                //    $" = @{primaryKeyName}";
                try
                {
                    //await db.ExecuteAsync(sql, entityToDelete);
                    await db.DeleteAsync<TEntity>(entityToDelete);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }

        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                // Get the dbSet from the Entity passed in                
                IQueryable<TEntity> query = _dataContext.Data;

                // Apply the filter
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // Include the specified properties
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                // Sort
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString))
            {
                db.Open();
                //string sql = $"select * from {entityName}";
                //IEnumerable<TEntity> result = await db.QueryAsync<TEntity>(sql);
                //return result;
                return await db.GetAllAsync<TEntity>();
            }
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString))
            {
                db.Open();
                // start a transaction in case something goes wrong
                await db.ExecuteAsync("begin transaction");
                try
                {
                    // Get the primary key property
                    var prop = entityType.GetProperty(primaryKeyName);

                    // int key?
                    if (primaryKeyType == "Int32")
                    {
                        // not an identity?
                        if (PKNotIdentity == true)
                        {
                            // get the highest value
                            var sql = $"select max({primaryKeyName}) from {entityName}";
                            // and add 1 to it
                            var Id = Convert.ToInt32(db.ExecuteScalar(sql)) + 1;
                            // update the entity
                            prop.SetValue(entity, Id);
                            // do the insert
                            db.Insert<TEntity>(entity);
                        }
                        else
                        {
                            // key will be created by the database
                            var Id = (int)db.Insert<TEntity>(entity);
                            // set the value
                            prop.SetValue(entity, Id);
                        }
                    }
                    else if (primaryKeyType == "String")
                    {
                        // string primary key. Use my helper
                        string sql = DapperSqlHelper.GetDapperInsertStatement(entity, entityName);
                        await db.ExecuteAsync(sql, entity);
                    }
                    // if we got here, we're good!
                    await db.ExecuteAsync("commit transaction");
                    return entity;
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                    await db.ExecuteAsync("rollback transaction");
                    return null;
                }
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionString))
            {
                db.Open();
                try
                {
                    //string sql = DapperSqlHelper.GetDapperUpdateStatement(entity, entityName, primaryKeyName);
                    //await db.ExecuteAsync(sql, entity);
                    await db.UpdateAsync<TEntity>(entity);
                    return entity;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
