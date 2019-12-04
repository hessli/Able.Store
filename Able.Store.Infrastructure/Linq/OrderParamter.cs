
namespace System.Linq
{
   public  class OrderParamter
    {
        public OrderParamter(string filedName,bool isDesc)
        {
             FiledName = filedName;

            IsDesc = isDesc;
        }
         public string FiledName { get; private set; }
         public bool IsDesc { get; private set; }
    }
}
