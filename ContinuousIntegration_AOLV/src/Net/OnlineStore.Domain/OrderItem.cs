namespace OnlineStore.Domain
{
    public class OrderItem
    {
        public Product Product { get; private set; }

        public int Quantity { get; private set; }

        /*
         * Order Item Constructor
         */
        public OrderItem(Product product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }
    }
}