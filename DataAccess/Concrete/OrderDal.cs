using Entities.Concrete;
using DataAccess.Abstract;
using Core.DataAccess.EntityFramework;

namespace DataAccess.Concrete
{
    public class OrderDal : EfEntityRepositoryBase<Order>, IOrderDal
    {
        public OrderDal(PerfumeryContext perfumeryContext) : base(perfumeryContext)
        {
        }
    }
}
