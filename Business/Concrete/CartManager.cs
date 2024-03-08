using System.Text;
using Newtonsoft.Json;
using Business.Abstract;
using Entities.Concrete;
using Business.Constants;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPerfumeService _perfumeService;
        private readonly ILogger<CartManager> _logger;

        public CartManager(IHttpContextAccessor httpContextAccessor, IPerfumeService perfumeService, ILogger<CartManager> logger)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _perfumeService = perfumeService;
            _logger = logger;
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public IDataResult<List<CartItem>> GetCartItems()
        {
            try
            {
                if (TryGetCartItemsFromSession(out var cartItems))
                {
                    var message = cartItems.Any() ? Messages.CartItemsListed : Messages.CartIsEmpty;
                    return new SuccessDataResult<List<CartItem>>(cartItems, message);
                }
                return new SuccessDataResult<List<CartItem>>(new List<CartItem>());
            }
            catch (Exception)
            {
                return new ErrorDataResult<List<CartItem>>(Messages.GetCartItemsMethodErrorOccured);
            }
        }

        public IResult AddToCart(CartItem cartItem)
        {
            _logger.LogInformation("AddToCart method called");

            if (cartItem == null)
            {
                throw new ArgumentNullException(nameof(cartItem));
            }

            var userDetail = GetUserDetailFromSession();
            if (userDetail == null)
            {
                return new ErrorResult(Messages.UserDetailsNotFoundInSession);
            }

            if (!IsPerfumeValid(cartItem))
            {
                return new ErrorResult(Messages.CartCouldNotBeUpdated);
            }

            UpdateCart(cartItem);

            LogCartUpdate(cartItem, userDetail);

            return new SuccessResult(Messages.CartUpdated);
        }

        public IResult RemoveFromCart(int perfumeId)
        {
            try
            {
                var userDetail = GetUserDetailFromSession();
                if (userDetail == null)
                {
                    return new ErrorResult(Messages.UserDetailsNotFoundInSession);
                }

                var cartItems = GetCartItems().Data;

                if (cartItems == null)
                {
                    return new ErrorResult(Messages.CartIsEmpty);
                }

                var itemToRemove = GetCartItemById(cartItems, perfumeId);

                if (itemToRemove != null)
                {
                    RemoveItemFromCart(cartItems, itemToRemove);
                    LogCartItemRemoval(perfumeId, userDetail);
                    return new SuccessResult(Messages.CartItemDeleted);
                }

                return new ErrorResult(Messages.CartItemCouldNotBeDeleted);
            }
            catch (Exception)
            {
                return new ErrorResult(Messages.GeneralRequestProcessingError);
            }
        }

        private CartItem GetCartItemById(List<CartItem> cartItems, int perfumeId)
        {
            return cartItems.FirstOrDefault(item => item.PerfumeId == perfumeId);
        }

        private void RemoveItemFromCart(List<CartItem> cartItems, CartItem itemToRemove)
        {
            cartItems.Remove(itemToRemove);
            SaveCartItems(cartItems);
        }

        private void LogCartItemRemoval(int perfumeId, UserDetail userDetail)
        {
            var time = DateTime.UtcNow;
            var perfume = _perfumeService.GetById(perfumeId).Data;
            _logger.LogInformation("CartItem removed from the cart: {@perfume} by {@userDetail} at {time}", perfume, userDetail, time);
        }


        private UserDetail GetUserDetailFromSession()
        {
            if (!Session.TryGetValue("UserDetail", out byte[] userDetailJson) || userDetailJson == null)
            {
                return null;
            }

            var userDetailString = Encoding.ASCII.GetString(userDetailJson);
            return JsonConvert.DeserializeObject<UserDetail>(userDetailString);
        }

        private bool IsPerfumeValid(CartItem cartItem)
        {
            return IsPerfumeExist(cartItem) && IsQuantityValid(cartItem);
        }

        private void UpdateCart(CartItem cartItem)
        {
            var cartItems = GetCartItems().Data ?? new List<CartItem>();
            var existingItem = cartItems.FirstOrDefault(item => item.PerfumeId == cartItem.PerfumeId);

            if (existingItem != null)
            {
                existingItem.Quantity = cartItem.Quantity;
            }
            else
            {
                cartItems.Add(cartItem);
            }

            SaveCartItems(cartItems);
        }

        private void LogCartUpdate(CartItem cartItem, UserDetail userDetail)
        {
            var time = DateTime.UtcNow;
            var perfume = _perfumeService.GetById(cartItem.PerfumeId).Data;
            _logger.LogInformation("CartItem added to cart: {@perfume} by {@userDetail} at {time}", perfume, userDetail, time);
        }


        private void SaveCartItems(List<CartItem> cartItems)
        {
            if (cartItems == null)
            {
                _logger.LogWarning("Attempted to save null cart items.");
                return;
            }

            var cartItemsJson = JsonConvert.SerializeObject(cartItems);
            Session.Set("CartItems", Encoding.ASCII.GetBytes(cartItemsJson));
        }


        private bool IsPerfumeExist(CartItem cartItem)
        {
            if (_perfumeService.GetById(cartItem.PerfumeId).Data == null)
            {
                return false;
            }
            return true;
        }

        private static bool IsQuantityValid(CartItem cartItem)
        {
            if (cartItem.Quantity == 0 || cartItem.Quantity < 0)
            {
                return false;
            }
            return true;
        }

        private bool TryGetCartItemsFromSession(out List<CartItem> cartItems)
        {
            if (Session.TryGetValue("CartItems", out byte[] cartItemsJson))
            {
                var cartItemsString = Encoding.ASCII.GetString(cartItemsJson);
                cartItems = JsonConvert.DeserializeObject<List<CartItem>>(cartItemsString) ?? new List<CartItem>();
                return true;
            }
            cartItems = null;
            return false;
        }
    }
}
