using System;
using System.Collections.Generic;

namespace Sum.Domain.Entity
{
    public partial class OrderSubtotals
    {
        public int OrderId { get; set; }
        public decimal? Subtotal { get; set; }
    }
}
