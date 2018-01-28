using System;

namespace Hypeticker.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Company { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int User { get; set; }
        public OrderType OrderType { get; set; }

        public override string ToString()
        {
            var type = Enum.GetName(typeof(OrderType), this.OrderType);
            return $"Order to {type.ToLower()} {Quantity} shares of {Company.ToUpper()} at {Price}";
        }
    }
}
