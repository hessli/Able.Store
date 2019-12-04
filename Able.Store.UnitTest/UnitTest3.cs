
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Able.Store.UnitTest
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestCategory()
        {
            IList<dynamic> list = new List<dynamic>();
            list.Add(new
            {
                Desc = "这个真的好好吃看第一张图片就知道了",
                Img = "https://i3.meishichina.com/attachment/recipe/2018/04/25/20180425152463349310413.jpg?x-oss-process=style/c320",
                Sort=1
            });
            list.Add(new
            {
                Desc = "第二张图片也可以吧看图片就知道了",
                Img = "https://i3.meishichina.com/attachment/recipe/2018/04/25/20180425152463349310413.jpg?x-oss-process=style/c320",
                Sort=2
            });

           var str= Newtonsoft.Json.JsonConvert.SerializeObject(list);
             

           // AutoMapperBootStrapper.Initialize.ConfigureAutoMapper();

           // IProductRepository productRepository = new ProductRepository(new EFUnitOfWork());

           // IProductCatalogService productService = new ProductCatalogService(productRepository);

           // var list= new List<string>();
           // list.Add("Id");
           //var result=  productService.GetProducts(new ProductQuery {
           //      FildNames= list,
           //       IsDesc=true,
           //        Title= "红烧排骨",
           //         PageIndex=1,
           //          PageSize=10
           // });

      

            
            //var item = result;
        }
  
    }
}
