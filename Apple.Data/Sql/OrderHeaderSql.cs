using Apple.Core;
using Apple.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apple.Data.Sql
{
    public class OrderHeaderSql : Sql<OrderHeader>, IOrderHeader
    {
        private readonly AppleDbContext _db;

        public OrderHeaderSql(AppleDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader orderHeader)
        {
            var orderHeaderDb = _db.OrderHeaders.FirstOrDefault(m => m.Id == orderHeader.Id);
            _db.OrderHeaders.Update(orderHeaderDb);

            _db.SaveChanges();

        }
    }
}
