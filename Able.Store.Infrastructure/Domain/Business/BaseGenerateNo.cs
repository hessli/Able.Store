using System;

namespace Able.Store.Infrastructure.Domain.Business
{
    public abstract class BaseGenerateNo : IGenerateNo
    {
        protected virtual string GetFirstPartSequence(string prefix)
        {
             return string.Concat(prefix,DateTime.Now.ToString("yyMMdd"));
        }
        protected virtual int OrderLenght
        {
            get;
            set;
        } = 12;
        public abstract string Generate();

        protected virtual string GetSequence(string firstPart,string secondPart) {

            var length= firstPart.Length + secondPart.Length;
            var differ=this.OrderLenght - length;
            if (differ>0)
            {
                var temp = secondPart.PadLeft(differ,'0');
                return string.Concat(firstPart,temp,secondPart);
            }
            else return firstPart + secondPart;
        }
        
    }
}
