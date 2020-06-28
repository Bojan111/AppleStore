using Apple.Core;
using System;
using System.Collections.Generic;
using System.Text;


namespace Apple.Data.Interface
{
    public interface IOrderDetails : ISql<OrderDetails>
    {
        void Update(OrderDetails orderDetails);
    }
}
