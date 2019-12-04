using System.Reflection;
using System;
namespace Able.Store.Infrastructure.Reflect
{
    public  class ReflectObject<T>:BaseReflect
    { 
       public ReflectObject()
        {
            base.Type =typeof(T);
        }
        public T V { get; set; }
       
        public object RunMethod(string methodName,params object[] paramters)
        {  

            MethodInfo method=null;

            object value = null;

            if (V == null)
                throw new ArgumentNullException("V");

            if (base.DicMethods.TryGetValue(methodName, out method))
            {
                value=method.Invoke(V, paramters);
            }
            return value;
        }
    }

}
