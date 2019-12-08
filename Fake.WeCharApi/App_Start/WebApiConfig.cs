using Fake.WeCharApi.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Fake.WeCharApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MessageHandlers.Add(new LogisticsHandler());

            #region 首页
            MapRoute(config.Routes, "Eorderservice", "Logistics", "PostEorderservice");
            #endregion
        }


        static void MapRoute(HttpRouteCollection routes, string url, string controllerName, string actionName)
        {

            //将请求的路径中/替换#作为路由器名称
            string routeName = url.Replace("/", "#");
            MapRoute(routes, routeName, url, controllerName, actionName);
        }
        static HashSet<string> _nameHash = new HashSet<string>();
        static HashSet<string> _urlHahs = new HashSet<string>();


        static void MapRoute(HttpRouteCollection routes, string routeName, string url, string controllerName, string actionName)
        {
            if (_nameHash.Contains(routeName))
            {
                throw new ArgumentException("routeName error");
            }
            if (_urlHahs.Contains(url))
            {
                throw new ArgumentException("url error");
            }
            _nameHash.Add(routeName);
            _urlHahs.Add(url);
            routes.MapHttpRoute(
                routeName,
                url,
                new { controller = controllerName, action = actionName }
            );
        }
    }
}
