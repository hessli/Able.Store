using Able.Store.Infrastructure.Domain;

namespace Able.Store.Adminstrator.Model.BasketsDomain
{
    public interface IBasketRepository:IRepository<Basket>
    {
        Basket GetBasket(int userid);

        Basket GetBasket(int userid, int[] skuIds);
        void Commit(Basket basket);
    }
}
