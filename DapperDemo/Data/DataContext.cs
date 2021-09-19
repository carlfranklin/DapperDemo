using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Linq2Dapper;
using Dapper.Contrib.Linq2Dapper.Extensions;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DapperDemo.Data
{
    // Remove virtual relation properties from models
    public class DataContext<TEntity> : IDisposable where TEntity: class
    {
        private readonly SqlConnection _connection;
        private readonly IConfiguration _config;
        private Linq2Dapper<TEntity> _data;

        public Linq2Dapper<TEntity> Data =>
             _data ?? (_data = CreateObject<TEntity>());

        public DataContext(IConfiguration config)
        {
            _config = config;
            var sqlConnectionString = _config.GetConnectionString("ChinnokConnectionString");
            _connection = new SqlConnection(sqlConnectionString);
        }

        private Linq2Dapper<T> CreateObject<T>()
        {
            return new Linq2Dapper<T>(_connection);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
