namespace PromotionEngine.Domain.Models
{
    /// <summary>
    /// Product entity.
    /// </summary>
    public class Product
    {
        public string Sku { get; set; }
        
        public double Price { get; set; }

        public Product(string sku, double price)
        {
            this.Sku = sku;
            this.Price = price;
        }
    }
}