using System;

namespace ClassLibrary {
    
    public enum CustomerCategory
    {
        Good = 1,
        Bad = 2,
        Ugly = 3
    }

    public class OrderServices 
    {
        private readonly IDiscountDB discountDb;

        public OrderServices(IDiscountDB discountDb)
        {
            this.discountDb = discountDb;
        }

        public decimal CalculateDiscount(decimal amount, CustomerCategory customerCategory)
        {
            var discount = 0;

            if (amount > 1000)
            {
                discount = discountDb.GetDiscount(customerCategory);
            }
            return (amount*discount/100);
        }

        public void Save(Order order)
        {
            if (order.Total <= 0)
                throw new Exception("Total must be greater than zero");

            discountDb.Save(order);
        }

        public Order GetOrderById(int id)
        {
            return discountDb.GetOrderById(id);
        }
    }
}
