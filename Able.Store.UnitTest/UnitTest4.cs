using Able.Store.Infrastructure;
using Able.Store.Infrastructure.Crypt;
using Able.Store.Infrastructure.Utils;
using Able.Store.Model.Users;
using Able.Store.Model.UsersDomain;
using Able.Store.Repository.EF;
using Able.Store.Repository.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

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
            //IUserRepository r = new UserRepository(new EFUnitOfWork());

            Expression<Func<User, bool>> expression = x => x.Id == 3;


            expression.Compile();

            //var u = r.GetFirstOrDefault(x => true);


            //r.Remove(u);


            //r.Commit();
        }


        [TestMethod]
        public void TestW()
        {


            Base64 uy = new Base64();

            //var f= uy.Base64EnCode("12345678张三李四");//\u001f 这个表示的是一个编码的对应值，其实也就是一个数字


            var qs = uy.Base64EnCode("12345678张三李四");

            var fs= uy.Base64EnCode("abcdsdfdsdfdsdfdsssssssf");

            //输入一个字符串，经过64编码后有24个字符，获取字节数只有16个
            //tiihtNczf5v6AKRyjwEUhQ==

            IEncrypted encrypted = new AesEncrypt(fs, qs);

            var q = encrypted.CryptStr(Newtonsoft.Json.JsonConvert.SerializeObject(new
            {
                Name = "张三",
                Age = 14
            }));


          var b=  encrypted.DecryptStr(q);

        }

        [TestMethod]
        public void TestConsumer()
        {

            ParameterExpression parameter = Expression.Parameter(typeof(User), "x");

            Expression<Func<User, bool>> expression = x => true;


            MemberExpression member = Expression.Property(parameter, "Nick");

            ConstantExpression constant = Expression.Constant("z");

            var contains = Expression.Call(constant, typeof(string).GetMethod("Contains"), member);

            var k = Expression.Not(contains);

            expression = expression.And(Expression.Lambda<Func<User, bool>>(k));

            var s = expression.ToString();

        }


    }
}
