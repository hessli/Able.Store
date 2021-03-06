﻿using Able.Store.Infrastructure.Domain;
using System.Collections.Generic;

namespace Able.Store.Adminstrator.Model.CategoriesDomain
{
    public interface ICategoryRepository:IRepository<Category>
    {
        IList<Category> GetCategories(int? id=null, int size = 0);
     
    }
}
