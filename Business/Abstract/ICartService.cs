using Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface ICartService
    {
        public IDataResult<List<CartItem>> GetCartItems();
        public IResult AddToCart(CartItem item);
        public IResult RemoveFromCart(int itemId); 
    }
}
