using System.Linq;

namespace PromotionEngine.Domain.Models.Promotions
{
    /// <summary>
    /// Buy two products together promotion.
    /// </summary>
    public class BuyPairPromotion : Promotion
    {
        public BuyPairPromotion(string productSku, string pairProductSku, double discountPrice)
            : base(new[] { productSku, pairProductSku }, discountPrice)
        {
        }

        public override bool AppliesToCart(Cart cart) =>
            this.ProductSkus.All(p => cart.ProductSkuToCountInCart.ContainsKey(p));

        public override double ApplyPromotion(Product product, int count)
        {
            return this.DiscountPrice;
        }
    }
}