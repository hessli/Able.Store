using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Able.Store.IService
{
    public class MoneyFormatter : IValueConverter<decimal, string>
    {
        public string Convert(decimal sourceMember, ResolutionContext context) => sourceMember.ToString("c");
        //{
        //    return  sourceMember.ToString("c");
        //}
    }
}
