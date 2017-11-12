using System;

namespace Hypeticker.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Word { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
