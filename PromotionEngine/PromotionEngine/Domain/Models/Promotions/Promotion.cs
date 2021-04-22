using System.Collections.Generic;

namespace PromotionEngine.Domain.Models.Promotions
{
    public abstract class Promotion
    {
        public IEnumerable<string> ProductSkus { get; set; } = default!;
        
        public double DiscountPrice { get; set; }
        
        public Promotion(IEnumerable<string> productSkus, double discountPrice)
        {
            this.ProductSkus = productSkus;
            this.DiscountPrice = discountPrice;
        }

        public abstract bool AppliesToCart(Cart cart);

        public abstract double ApplyPromotion(Product product, int count);
    }
}