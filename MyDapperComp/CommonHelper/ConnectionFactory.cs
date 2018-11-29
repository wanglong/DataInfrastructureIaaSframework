using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using Dapper;
using DapperExtensions;
using System.Reflection;
using DapperExtensions.Sql;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace MyDapperComp.CommonHelper
{
    /// <summary>
    /// 数据库连接辅助类
    /// </summary>
    public class ConnectionFactory
    {
        /// <summary>
        /// 转换数据库类型
        /// </summary>
        /// <param name="databaseType">数据库类型</param>
        /// <returns></returns>
        public static DatabaseType GetDataBaseType(string databaseType)
        {
            DatabaseType returnValue = DatabaseType.SqlServer;
            foreach (DatabaseType dbType in Enum.GetValues(typeof(DatabaseType)))            
            {
                if(dbType.ToString().Equals(databaseType, StringComparison.OrdinalIgnoreCase))
                {
                    returnValue = dbType;
                    break;
                }
            }

            return returnValue;
        }

        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <param name="databaseType">数据库类型</param>
        /// <returns></returns>
        public static Database CreateConnection(string strConn, DatabaseType databaseType = DatabaseType.Oracle)
        {
            Database connection = null;
            //获取配置进行转换
            switch(databaseType)
            {
                case DatabaseType.SqlServer:
                    var sqlConn = new SqlConnection(strConn);
                    var sqlConfig = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>)
                        , new List<Assembly>()
                        , new SqlServerDialect());
                    var sqlGenerator = new SqlGeneratorImpl(sqlConfig);
                    connection = new Database(sqlConn, sqlGenerator);
                    break;
                case DatabaseType.MySql:
                    var mysqlConn = new MySqlConnection(strConn);
                    var mysqlConfig = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>)
                        , new List<Assembly>()
                        , new MySqlDialect());
                    var mysqlGenerator = new SqlGeneratorImpl(mysqlConfig);
                    connection = new Database(mysqlConn, mysqlGenerator);
                    break;
                case DatabaseType.SqlLite:
                    var sqlLiteConn = new SQLiteConnection(strConn);
                    var sqlLiteConfig = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>)
                        , new List<Assembly>()
                        , new SqliteDialect());
                    var sqlliteGenerator = new SqlGeneratorImpl(sqlLiteConfig);
                    connection = new Database(sqlLiteConn, sqlliteGenerator);
                    break;
                case DatabaseType.Oracle:
                    var oracleConn = new OracleConnection(strConn);
                    var oracleConfig = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>)
                        , new List<Assembly>()
                        , new OracleDialect());
                    var oracleGenerator = new SqlGeneratorImpl(oracleConfig);
                    connection = new Database(oracleConn, oracleGenerator);
                    break;
            }

            return connection;
        }
    }
}
