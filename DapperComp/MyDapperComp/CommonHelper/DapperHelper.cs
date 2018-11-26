using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace MyDapperComp.CommonHelper
{
    /// <summary>
    /// dapper 帮助类
    /// </summary>
    public class DapperHelper : IDapperHelper, IDisposable
    {
        private string ConnectionString = string.Empty;
        private Database Connection = null;
        /// <summary>
        /// 初始化 若不传则默认从appsettings.json读取Connections:DefaultConnect节点
        /// 传入setting:xxx:xxx形式 则会从指定的配置文件中读取内容
        /// 直接传入连接串则
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="jsonConfigFileName"> 配置文件名称</param>
        public DapperHelper(string conn = "", string jsonConfigFileName = "appsettings.json", DatabaseType databaseType = DatabaseType.Oracle)
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile(jsonConfigFileName, optional: true)
              .Build();
            if (string.IsNullOrEmpty(conn))
            {
                conn = config.GetSection("Connections:DefaultConnect").Value;
            }
            else if (conn.StartsWith("setting:"))
            {
                conn = config.GetSection(conn.Substring(8)).Value;
            }

            ConnectionString = conn;
            Connection = ConnectionFactory.CreateConnection(ConnectionString, databaseType);
        }

        public Database GetConnetion()
        {
            return Connection;
        }

        public IDbTransaction TranStart()
        {
            if (Connection.Connection.State == ConnectionState.Closed)
                Connection.Connection.Open();
            return Connection.Connection.BeginTransaction();
        }

        public void TranRollBack(IDbTransaction tran)
        {
            tran.Rollback();
            if (Connection.Connection.State == ConnectionState.Open)
                tran.Connection.Close();
        }

        public void TranCommit(IDbTransaction tran)
        {
            tran.Commit();
            if (Connection.Connection.State == ConnectionState.Open)
                tran.Connection.Close();
        }

        public bool Delete<T>(T obj, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {

            return Connection.Delete(obj, tran, commandTimeout);
        }

        public bool Delete<T>(IEnumerable<T> list, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {

            return Connection.Delete(list, tran, commandTimeout);
        }

        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Dispose();
            }
        }

        public T Get<T>(string id, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {
            return Connection.Get<T>(id, tran, commandTimeout);
        }

        public IEnumerable<T> GetAll<T>(object predicate = null, IList<ISort> sort = null, IDbTransaction tran = null, int? commandTimeout = null, bool buffered = true) where T : class
        {
            return Connection.GetList<T>(predicate, sort, tran, commandTimeout, buffered);
        }

        public IEnumerable<T> GetPage<T>(object predicate, IList<ISort> sort, int page, int pagesize, IDbTransaction tran = null, int? commandTimeout = null, bool buffered = true) where T : class
        {
            return Connection.GetPage<T>(predicate, sort, page, pagesize, tran, commandTimeout, buffered);
        }

        public dynamic Insert<T>(T obj, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {
            return Connection.Insert(obj, tran, commandTimeout);
        }

        public void Insert<T>(IEnumerable<T> list, IDbTransaction tran = null, int? commandTimeout = null) where T : class
        {
            Connection.Insert(list, tran, commandTimeout);
        }

        public bool Update<T>(T obj, IDbTransaction tran = null, int? commandTimeout = null, bool ignoreAllKeyProperties = true) where T : class
        {
            return Connection.Update(obj, tran, commandTimeout, ignoreAllKeyProperties);
        }

        public bool Update<T>(IEnumerable<T> list, IDbTransaction tran = null, int? commandTimeout = null, bool ignoreAllKeyProperties = true) where T : class
        {
            return Connection.Update(list, tran, commandTimeout, ignoreAllKeyProperties);
        }

        public List<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.Connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType).AsList();
        }

        public int Execute<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Connection.Connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
