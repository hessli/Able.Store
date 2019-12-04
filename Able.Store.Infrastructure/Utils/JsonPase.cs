using Newtonsoft.Json;
using System.Collections.Generic;

namespace Able.Store.Infrastructure.Utils
{
   public class JsonPase
    {
        public static string Serialize<T>(T t)
        {
            return JsonConvert.SerializeObject(t);
        }
        public static T[] Serialize<T>(IList<string> ts)
        {
            T[] items = new T[ts.Count];

            for (var i = 0; i < ts.Count; i++)
            {

                items[i] = Deserialize<T>(ts[i]);
            }
            return items;
        }
        public static T Deserialize<T>(string value)
        {
            
            return JsonConvert.DeserializeObject<T>(value);
        }
        public static IList<T> Deserialize<T>(string[]  items)
        {
            IList<T> r = new List<T>();

            foreach (var item in items)
            {
                r.Add(Deserialize<T>(item));
            }
            return r;
        }
    }
}
