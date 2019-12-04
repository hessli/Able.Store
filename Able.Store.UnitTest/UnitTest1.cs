//using Able.Store.Model.Categories.Domain;
//using Able.Store.Model.ProductsDomain;
//using Able.Store.Repository.Categories;
//using Able.Store.Service.ViewModel;
//using AutoMapper;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;

//namespace Able.Store.UnitTest
//{

//    public class BA
//    {
//        public string Name { get; set; }

//        public string Cost { get; set; }

//        public ICollection<BaItem> MaItems { get; set; }
//    }

//    public class BaItem
//    {
//        public decimal Price { get; set; }
//    }

//    public class MA {
//         public string Name { get; set; }

//         public decimal Cost { get; set; }
//         public ICollection<MaItem> MaItems { get; set; }
//    }

//    public class CurrencyFormatter : IValueConverter<decimal, string>
//    {
//        public string Convert(decimal source, ResolutionContext context)
//            => source.ToString("c");
//    }

//    public class MaItem
//    {
//         public decimal Price { get; set; }
//    }

//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public void TestExpressPropertyHelper()
//        {
//            ICategoryRepository repository = new CategoryRepository();

//            var entiy = repository.GetFirstOrDefault(x=>true);

//        }


//        [TestMethod]
//        public void TestAutoMapper()
//        {
//            var config = new MapperConfiguration(cfg =>
//            {
//                cfg.CreateMap<ProductTitle, ProductSummaryView>().ReverseMap();
//                cfg.CreateMap<MA, BA>().ForMember(x=>x.Cost,opt=>opt.ConvertUsing<decimal>(new CurrencyFormatter())).ReverseMap();
//                cfg.CreateMap<MaItem, BaItem>().ReverseMap();
//            });

//            AutoMapper.Mapper mapper = new Mapper(config);

        

//          var data=   mapper.Map<ProductSummaryView>(new ProductTitle
//          {
//                 Price=5
//            });
//            var data2 = mapper.Map<ProductTitle >(new ProductSummaryView
//            {
//                Price = "5"
//            });
//            MA ma = new MA {
//                 Name="zhangsan",
//                  Cost=5,
//                  MaItems=new List<MaItem> {
//                       new MaItem{
//                            Price=5
//                       }
//                  }
//            };
//          var fbi=   mapper.Map<BA>(ma);
//        }
//    }
//}
