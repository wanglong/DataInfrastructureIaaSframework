using System;
using System.Linq;
using System.Threading.Tasks;
using MyDapperComp.CommonHelper;
using DapperExtensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DapperHelperTest
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// 基本测试  详细参考https://github.com/tmsmith/Dapper-Extensions/blob/master/DapperExtensions.Test/IntegrationTests/Oracle/CrudFixture.cs
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            //DapperHelper()
            //conn: "setting:Connections:OracleConnect", databaseType: DatabaseType.Oracle
            //conn: "setting:Connections:MsSqlConnect", databaseType: DatabaseType.SqlServer
            //conn: "setting:Connections:MySqlConnect", databaseType: DatabaseType.MySql
            using (var dp = new DapperHelper(conn: "setting:Connections:MySqlConnect", databaseType: DatabaseType.MySql))
            {
                var obj = new MyTest() { FID = "test222", FNAME = "test", FCREATETIME = DateTime.Now, FREALNAME = "t" };
                //增
                var insert = dp.Insert(obj);
                Assert.IsTrue(insert == "test222");
                obj.FNAME = "test2";
                //改
                var update = dp.Update(obj);
                Assert.IsTrue(update);
                //取所有带条件
                var predicate = Predicates.Field<MyTest>(f => f.FNAME, Operator.Eq, "test2");
                var allrecords = dp.GetAll<MyTest>(predicate);
                Assert.IsTrue(allrecords.Count() > 0);
                //取
                var user2 = dp.Get<MyTest>("test222");
                Assert.IsTrue(user2 != null);
                //删
                bool isdel = dp.Delete(obj);
                Assert.IsTrue(isdel);
            }
        }

        /// <summary>
        /// 测试事务
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            //DapperHelper()
            //conn: "setting:Connections:MsSqlConnect", databaseType: DatabaseType.SqlServer
            //conn: "setting:Connections:MsSqlConnect", databaseType: DatabaseType.SqlServer
            //conn: "setting:Connections:MySqlConnect", databaseType: DatabaseType.MySql
            using (var dp = new DapperHelper(conn: "setting:Connections:MySqlConnect", databaseType: DatabaseType.MySql))
            {
                var tran = dp.TranStart();
                var obj = new MyTest() { FID = "test222", FNAME = "test", FCREATETIME = DateTime.Now, FREALNAME = "t" };
                var obj1 = new MyTest() { FID = "test111", FNAME = "test1", FCREATETIME = DateTime.Now, FREALNAME = "t1" };
                //事务回滚
                var insert = dp.Insert(obj, tran);
                var user2 = dp.Get<MyTest>("test222", tran);
                Assert.IsTrue(user2 != null);
                var insert1 = dp.Insert(obj1, tran);
                var user1 = dp.Get<MyTest>("test111", tran);
                Assert.IsTrue(user1 != null);
                tran.Rollback();
                var user3 = dp.Get<MyTest>("test222");
                Assert.IsTrue(user3 == null);
                var user4 = dp.Get<MyTest>("test111");
                Assert.IsTrue(user4 == null);
                //事务提交
                tran = dp.TranStart();
                insert = dp.Insert(obj, tran);
                Assert.IsTrue(user2 != null);
                insert1 = dp.Insert(obj1, tran);
                user1 = dp.Get<MyTest>("test111", tran);
                Assert.IsTrue(user1 != null);
                tran.Commit();
                user3 = dp.Get<MyTest>("test222");
                Assert.IsTrue(user3 != null);
                user4 = dp.Get<MyTest>("test111");
                Assert.IsTrue(user4 != null);
                //删除测试数据
                tran = dp.TranStart();
                bool isdel = dp.Delete(obj, tran);
                Assert.IsTrue(isdel);
                bool isdel1 = dp.Delete(obj1, tran);
                Assert.IsTrue(isdel1);
                tran.Commit();
            }
        }

        /// <summary>
        /// 测试sql
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            //DapperHelper()
            //conn: "setting:Connections:MsSqlConnect", databaseType: DatabaseType.SqlServer
            //conn: "setting:Connections:MsSqlConnect", databaseType: DatabaseType.SqlServer
            //conn: "setting:Connections:MySqlConnect", databaseType: DatabaseType.MySql
            using (var dp = new DapperHelper(conn: "setting:Connections:MySqlConnect", databaseType: DatabaseType.MySql))
            {
                var tt = dp.Query<MyTest>("select * from MyTest");
                Assert.IsTrue(tt != null);
                Assert.IsTrue(tt.Count() == 0);
            }
        }
    }
}
