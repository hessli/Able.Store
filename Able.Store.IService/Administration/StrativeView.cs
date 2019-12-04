
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

        public static IList<StrativeView> ToCities(IList<Province> entities)
        {
            IList<StrativeView> cities = new List<StrativeView>();
            foreach (var item in entities)
            {
                foreach (var subItem in item.Cities)
                {
                    cities.Add(new StrativeView
                    {
                        code = subItem.Code,
                        name = subItem.Name,
                        parentId = item.Code
                    });
                }
            }
            return cities;
        }


        public static Tuple<IList<StrativeView>, IList<StrativeView>, IList<StrativeView>>
            ToView(IList<Province> entities)
        {

            IList<StrativeView> provinces = new List<StrativeView>();
            IList<StrativeView> cities = new List<StrativeView>();
            IList<StrativeView> areas = new List<StrativeView>();

            foreach (var item in entities)
            {
                provinces.Add(new StrativeView
                {
                    code = item.Code,
                    name = item.Name
                });

                foreach (var subItem in item.Cities)
                {
                    cities.Add(new StrativeView
                    {
                        code = subItem.Code,
                        name = subItem.Name,
                        parentId = item.Code
                    });


                    foreach (var cItem in subItem.Areas)
                    {
                        areas.Add(new StrativeView
                        {

                            code = cItem.Code,
                            name = cItem.Name,
                            parentId = subItem.Code
                        });
                    }
                }
            }
            Tuple<IList<StrativeView>, IList<StrativeView>, IList<StrativeView>> tuple =
             new Tuple<IList<StrativeView>, IList<StrativeView>, IList<StrativeView>>(provinces, cities, areas);

            return tuple;

        }


    }
}
