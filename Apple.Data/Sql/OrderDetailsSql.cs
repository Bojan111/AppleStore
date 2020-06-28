using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apple.Core;
using Apple.Data.Interface;

namespace Apple.Data.Sql
{
    public class OrderDetailsSql : Sql<OrderDetails>, IOrderDetails
    {
        private readonly AppleDbContext _db;

        public OrderDetailsSql(AppleDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetails orderDetails)
        {
            var orderDetailsFromDb = _db.OrderDetails.FirstOrDefault(m => m.Id == orderDetails.Id);
            _db.OrderDetails.Update(orderDetailsFromDb);

            _db.SaveChanges();

        }
    }
}
