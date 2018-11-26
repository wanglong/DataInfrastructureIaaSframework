using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MyDapperComp.CommonHelper
{
    /// <summary>
    /// Dapper Data Interface
    /// </summary>
    public interface IDapperHelper
    {
        /// <summary>
        /// get connection.
        /// </summary>
        /// <returns></returns>
        Database GetConnetion();

        /// <summary>
        /// get instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        T Get<T>(string id, IDbTransaction tran = null, int? commandTimeout = null) where T:class;

        /// <summary>
        /// get all.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<T> GetAll<T>(object predicate = null, IList<ISort> sort = null
            , IDbTransaction tran = null
            , int? commandTimeout = null
            , bool buffered = true) where T:class;

        /// <summary>
        /// get page .
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="buffered"></param>
        /// <returns></returns>
        IEnumerable<T> GetPage<T>(object predicate = null, IList<ISort> sort = null
            , int page = 1
            , int pageSize = 0
            , IDbTransaction tran = null
            , int? commandTimeout = null
            , bool buffered = true) where T : class;

        /// <summary>
        /// insert instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        dynamic Insert<T>(T instance, IDbTransaction tran = null, int? commandTimeout = null) where T : class;

        /// <summary>
        /// insert list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="TRAN"></param>
        /// <param name="commandTimeout"></param>
        void Insert<T>(IEnumerable<T> list, IDbTransaction TRAN = null, int? commandTimeout = null) where T : class;

        /// <summary>
        /// update instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="ignoreAllKeyProperties"></param>
        /// <returns></returns>
        bool Update<T>(T instance, IDbTransaction tran = null, int? commandTimeout = null
            , bool ignoreAllKeyProperties = false) where T : class;

        /// <summary>
        /// insert list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="ignoreAllKeyPorperties"></param>
        /// <returns></returns>
        bool Update<T>(IEnumerable<T> list, IDbTransaction tran = null, int? commandTimeout = null
            , bool ignoreAllKeyPorperties = false) where T : class;

        /// <summary>
        /// delete instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Delete<T>(T instance, IDbTransaction tran = null, int? commandTimeout = null) where T : class;

        /// <summary>
        /// delete list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="tran"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        bool Delete<T>(IEnumerable<T> list, IDbTransaction tran = null, int? commandTimeout = null) where T : class;

        /// <summary>
        /// start transaction.
        /// </summary>
        /// <returns></returns>
        IDbTransaction TranStart();

        /// <summary>
        /// transaction rollback.
        /// </summary>
        /// <param name="tran"></param>
        void TranRollBack(IDbTransaction tran);

        /// <summary>
        /// query sql.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        List<T> Query<T>(string sql, object param = null, IDbTransaction tran = null, bool buffered = true
            , int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// execute sql.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="tran"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        int Execute<T>(string sql, object param = null, IDbTransaction tran = null, bool buffered = true
            , int? commandTimeout = null, CommandType? commandType = null);
    } 
}
