using Entities.Concrete;
using Entities.Dtos.Order;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<Order>> GetAll();
        IDataResult<List<Order>> GetOrderByUserDetailId(int userDetailId);
        IResult CreateOrder(Order order);
    }
}
