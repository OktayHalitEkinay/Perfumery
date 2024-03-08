using System.Text;
using Newtonsoft.Json;
using Business.Abstract;
using Entities.Concrete;
using Business.Constants;
using DataAccess.Abstract;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly IPerfumeService _perfumeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderManager(IOrderDal orderDal, IHttpContextAccessor httpContextAccessor, IPerfumeService perfumeService)
        {
            _orderDal = orderDal;
            _httpContextAccessor = httpContextAccessor;
            _perfumeService = perfumeService;
        }
        private ISession Session => _httpContextAccessor.HttpContext.Session;
        public IResult CreateOrder(Order order)
        {
            try
            {
                
                if (!Session.TryGetValue("UserDetail", out byte[] userDetailJson))
                {
                    return new ErrorResult(Messages.UserDetailsNotFoundInSession);
                }

                var userDetailString = Encoding.ASCII.GetString(userDetailJson);
                var userDetail = JsonConvert.DeserializeObject<UserDetail>(userDetailString);

                
                if (!Session.TryGetValue("CartItems", out byte[] cartItemsJson) || cartItemsJson == null)
                {
                    return new ErrorResult(Messages.OrderCouldNotCreatedCartIsEmpty);
                }

                var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(Encoding.ASCII.GetString(cartItemsJson)) ?? new List<CartItem>();

                if (cartItems.Count == 0)
                {
                    return new ErrorResult(Messages.CartIsEmpty);
                }
                
                var orderDetails = cartItems.Select(item => new OrderDetail
                {
                    PerfumeId = item.PerfumeId,
                    Count = item.Quantity,
                    Price = item.Quantity * (_perfumeService.GetById(item.PerfumeId)?.Data?.Price ?? 0)
                }).ToList();

                
                order.OrderDate = DateTime.Now;
                order.UserDetailId = userDetail.UserDetailId;
                order.OrderDetails = orderDetails;

                
                _orderDal.Add(order);

                return new SuccessResult(Messages.OrderCreated);
            }
            catch (Exception ex)
            {
                return new ErrorResult(Messages.GeneralOrderCretingError);
            }
        }

        public IDataResult<List<Order>> GetOrderByUserDetailId(int userDetailId)
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(order => order.UserDetailId == userDetailId), Messages.OrdersListed);
        }

        public IDataResult<List<Order>> GetAll()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetAll(), Messages.OrdersListed);
        }

        public IDataResult<Order> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
