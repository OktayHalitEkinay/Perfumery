using Entities.Concrete;
using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;

namespace DataAccess.Concrete
{
    public class OrderDetailDal : EfEntityRepositoryBase<OrderDetail>, IOrderDetailDal
    {
        public OrderDetailDal(PerfumeryContext perfumeryContext) : base(perfumeryContext)
        {
        }
    }
}
