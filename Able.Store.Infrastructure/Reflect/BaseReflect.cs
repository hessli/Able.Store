using System;
using System.Collections.Generic;
using System.Reflection;

namespace Able.Store.Infrastructure.Reflect
{
    public abstract class BaseReflect
    {


        public BaseReflect()
        {

        }
        public System.Type Type { get; protected set; }
        public bool IsPrimitive
        {
            get
            {

                return Type.IsPrimitive;
            }
        }

        public bool IsString
        {
            get {
                 return Type == typeof(string);
            }
        }
        /// <summary>
        /// 所有的属性
        /// </summary>
        /// <returns></returns>
        private PropertyInfo[] _propertyInfo = null;
        public PropertyInfo[] PropertyInfos
        {
            get
            {

                if (_propertyInfo == null)
                {
                    _propertyInfo = Type.GetProperties();
                }
                return _propertyInfo;
            }
        }

        private Dictionary<string, MethodInfo> _dicMethods ;

        public Dictionary<string,MethodInfo> DicMethods {

            get {
                if (_dicMethods == null)
                {
                    _dicMethods = new Dictionary<string, MethodInfo>();

                    foreach (var item in this.Methods)
                    {
                        _dicMethods.Add(item.Name, item);
                    }
                }
                return _dicMethods;
            }
        }

        private MethodInfo[] _methods;
        /// <summary>
        /// 所有方法名
        /// </summary>
        public MethodInfo[] Methods
        {

            get
            {
                if (_methods == null)
                {
                    _methods= Type.GetMethods();
                }
                return _methods;
            }
        }
        /// <summary>
        /// 所有属性名
        /// </summary>
        private IList<string> _propertyNames = null;
        public IList<string> PropertyName
        {
            get
            {
                if (_propertyNames == null)
                {
                    _propertyNames = new List<string>();

                    foreach (var item in _propertyInfo)
                    {
                        _propertyNames.Add(item.Name);
                    }
                }
                return _propertyNames;
            }
        }
    }
}
