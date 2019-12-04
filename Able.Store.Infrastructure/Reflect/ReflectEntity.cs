using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Able.Store.Infrastructure.Reflect
{
    public class ReflectEntity: BaseReflect
    {   
        public ReflectEntity(Type type)
        {
            base.Type = type;
        }
        private IList<PropertyInfo> _includePropertyInfos = null;
        public IList<PropertyInfo> IncludePropertyInfos
        {
            get
            {
                if (_includePropertyInfos == null)
                {
                    GetIncludeProperty();
                }
                return _includePropertyInfos;
            }
        }
        public IList<string> IncludePropertyNames {

            get {

                  var items= IncludePropertyInfos
                                 .Select(x=>x.Name)
                                 .ToList();

                return items;
            }
        }
        /// <summary>
        /// 获取所有的导航属性
        /// </summary>
        /// <returns></returns>
        private void GetIncludeProperty()
        {
           
            Func<PropertyInfo, bool> predicate = x =>
                     (x.PropertyType
                       .Equals(typeof(System.ComponentModel.DataAnnotations
                           .Schema.ForeignKeyAttribute)));


            _includePropertyInfos = base.PropertyInfos.Where(x =>
                                       (x.PropertyType.IsClass && x.CustomAttributes.Any(y => y.AttributeType.Name.Equals("ForeignKeyAttribute"))) ||
                                       (x.CustomAttributes.Any(z=>z.AttributeType.Name.Equals("InversePropertyAttribute")) && x.PropertyType.Equals(typeof(ICollection<>)))

                                     ).ToList();
        }
    }
}
