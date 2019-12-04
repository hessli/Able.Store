using Able.Store.IService;
using Able.Store.Model.CategoriesDomain;
using AutoMapper;
using System.Collections.Generic;

namespace Able.Store.Service.IService.Categories
{
    public class CategoryView
    {
         public int id { get; set; }
         public string title { get; set; }
         public int sort { get; set; }
        public static IList<CategoryView> ToView(IList<Category> models)
        {
            Mapper mapper = new Mapper(AutoMapperBootStrapper.Configuration);

            var results = mapper.Map<IList<CategoryView>>(models);
            return results;
        }

    }
}
