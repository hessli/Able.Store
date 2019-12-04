using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Able.Store.Model.Users;
using Able.Store.Repository.Users;
using Able.Store.Repository.EF;
using System.Linq;
using Able.Store.Infrastructure.Queue.Rabbit;
using Able.Store.Infrastructure.Queue.Rabbit.Consumer;
using Able.Store.Infrastructure.Queue.Rabbit.Product;
using Able.Store.Infrastructure.Jobs;
using Able.Store.Infrastructure.Queue.Rabbit.RabbitTempContainer;
using Able.Store.Infrastructure.Cache.RabbitTempContainer;
using Able.Store.Infrastructure.Cache.Redis;

namespace Able.Store.UnitTest
{
    /// <summary>
    /// UnitTest4 的摘要说明
    /// </summary>
    [TestClass]
    public class UnitTest4
    {
        public UnitTest4()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性: 
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            IUserRepository r = new UserRepository(new EFUnitOfWork());

            var u = r.GetFirstOrDefault(x => true);


            r.Remove(u);


            r.Commit();
        }

        [TestMethod]
        public void TestConsumer()
        {

            // JobController.AddJob(new RabbitConnectionJob(), 1300000);

            //JobController.AddJob(new RedisConnectionJob(), 140000,500000);

            //JobController.Start();

             (new RedisConnectionJob()).Excute();

            CacheController redisCache = new CacheController();

            redisCache.SetStrPrimitive("s", "s");

            redisCache.SetStrPrimitive("b","b");


            string a = "xx";

           var f= a.GetHashCode();

            string b = "xxx";

          var qs=  b.GetHashCode();
        }

       

        
    }
}
