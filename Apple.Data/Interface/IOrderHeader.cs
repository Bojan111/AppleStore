using Apple.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Apple.Data.Interface
{
    public interface IOrderHeader : ISql<OrderHeader>
    {
        void Update(OrderHeader orderHeader);
    }
}
