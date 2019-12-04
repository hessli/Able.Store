using Able.Store.Infrastructure.Domain.Business;
namespace Able.Store.Model.OrdersDomain
{
    /// <summary>
    /// 通过redis自增量来产生单号
    /// </summary>
    public class OrderGenerateNo : BaseGenerateNo
    {
        private string _stuffix;
        public OrderGenerateNo(string stuffix)
        {
            _stuffix = stuffix;
        }
        private string prefix = "S";

        public override string Generate()
        {
           var firstPart=  base.GetFirstPartSequence(prefix);

           var no=  base.GetSequence(firstPart, _stuffix);

           return no;
        }
    }
}
