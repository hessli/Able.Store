
using Able.Store.Model.AdministrativeAreaDomain;
using System;
using System.Collections.Generic;

namespace Able.Store.IService.Administration
{
    public class StrativeView
    {
        public string code { get; set; }

        public string name { get; set; }

        public string parentId { get; set; }

        public int score { get; set; }

        public static IList<StrativeView> ToProvinces(IList<Province> entities)
        {
            IList<StrativeView> provinces = new List<StrativeView>();
            foreach (var item in entities)
            {
                provinces.Add(new StrativeView
                {
                    code = item.Code,
                    name = item.Name
                });
            }
            return provinces;
        }

        public static IList<StrativeView> ToCities(Province entity)
        {
            IList<StrativeView> cities = new List<StrativeView>();
            
                foreach (var subItem in entity.Cities)
                {
                    cities.Add(new StrativeView
                    {
                        code = subItem.Code,
                        name = subItem.Name,
                        parentId = entity.Code
                    });
               
            }
            return cities;
        }

        public static IList<StrativeView> ToArea(City entity)
        {
            IList<StrativeView> areas = new List<StrativeView>();

            foreach (var subItem in entity.Areas)
            {
                areas.Add(new StrativeView
                {
                    code = subItem.Code,
                    name = subItem.Name,
                    parentId = entity.Code
                });

            }
            return areas;
        }

    }
}
