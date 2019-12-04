using System;
using System.Collections.Generic;
using System.Web.Http;
namespace Able.Store.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            #region 首页
            MapRoute(config.Routes, "api/index/getbanner", "Index", "GetBanner");
            MapRoute(config.Routes, "api/index/getcategoris", "Index", "GetCategoris");
            MapRoute(config.Routes, "api/index/getnew", "Index", "GetNewProducts");
            MapRoute(config.Routes, "api/index/getrecommend", "Index", "GetRecommendProduct");
            #endregion

            #region 产品查询页
            MapRoute(config.Routes, "api/products/getcategoris", "Category", "GetCategories");

            MapRoute(config.Routes, "api/products/getcategorisby", "Category", "GetCategoriesBy");
            
            MapRoute(config.Routes, "api/products/getgategoryproducts", "Category", "PostCategoryProducts");

            MapRoute(config.Routes, "api/products/pages", "Product", "PostPaging");
            MapRoute(config.Routes, "api/products/detail", "Product", "GetDetail");
            #endregion

            #region 购物页面
            MapRoute(config.Routes, "api/shopping/tobasket", "Shopping", "PostToBasket");

            MapRoute(config.Routes, "api/shopping/changenumber", "Shopping", "PostChangeNumber");

            MapRoute(config.Routes, "api/shopping/remove", "Shopping", "PostRemove");

            MapRoute(config.Routes, "api/shopping/getbasket", "Shopping", "GetBasket");

            MapRoute(config.Routes, "api/shopping/getbasketbysku", "Shopping", "PostBaskBySku");

            #endregion

            #region 通用模块

            MapRoute(config.Routes, "api/adtrative/getprovince", "AdministrativeArea", "GetProvince");
            MapRoute(config.Routes, "api/adtrative/getcities", "AdministrativeArea", "GetCities");
            MapRoute(config.Routes, "api/adtrative/getareas", "AdministrativeArea", "GetAreas");
            #endregion


            #region 用户
            MapRoute(config.Routes, "api/user/createreceiver", "User", "CreateReceiverInfo");
            MapRoute(config.Routes, "api/user/getreceiver", "User", "GetReceiverInfos");
            MapRoute(config.Routes, "api/user/getdefault", "User", "GetDefaultReceiver");
            MapRoute(config.Routes, "api/user/setdefault", "User", "PostSetDefault");
            #endregion

            #region 订单

            MapRoute(config.Routes, "api/order/create", "Order", "PostCreateOrder");
            MapRoute(config.Routes, "api/order/get", "Order", "GetOrder");
            MapRoute(config.Routes, "api/order/getPayWay", "Order", "GetPayWay");

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
