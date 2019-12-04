using Able.Store.Model.Core;
using System.Collections.Generic;

namespace Able.Store.IService.Comm
{
    public class ImgDescView
    {
        public string img { get; set; }
        public string desc { get; set; }


        public static string GetImg(ImgDescView imgDesc)
        {
            if (imgDesc == null)
                return string.Empty;

            return imgDesc.img;
        }

        public static ImgDescView ToView(ImgJson imgJson)
        {
            if (imgJson == null)
                   return null;

            var item = new ImgDescView
            {
                desc = imgJson.Desc,
                img = imgJson.Img
            };

            return item;
        }
        public static IList<ImgDescView> ToViews(ImgJsonCollection imgJsonCollection)
        {
            IList<ImgDescView> results = new List<ImgDescView>();

            foreach (var item in imgJsonCollection)
            {
                if (item != null)
                {
                    results.Add(ToView(item));
                }
            }
            return results;
        }
    }
}
