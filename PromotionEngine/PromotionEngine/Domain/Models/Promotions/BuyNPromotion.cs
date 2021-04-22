using System;
using System.Linq;

namespace PromotionEngine.Domain.Models.Promotions
{
    /// <summary>
    /// A promotion that applies when N items are in the cart.
    /// Can be applied more than once per cart (e.g. N=2, CartSize=5, = 2 applies)
    /// </summary>
    public class BuyNPromotion : Promotion
    {
        public int GroupTotal { get; set; }

        public BuyNPromotion(string productSku, int groupTotal, double discountPrice)
            : base(new[] { productSku }, discountPrice)
        {
            this.GroupTotal = groupTotal;
        }

        public override bool AppliesToCart(Cart cart) => cart.ProductSkuToCountInCart.ContainsKey(this.ProductSkus.First());

        public override double ApplyPromotion(Product product, int count)
        {
            if (count < this.GroupTotal)
            {
                return count * product.Price;
            }

            var totalGroupsInCount = Math.Floor((double)count / this.GroupTotal);
            var leftOvers = count % this.GroupTotal;
            return (this.DiscountPrice * totalGroupsInCount) + (leftOvers * product.Price);
        }
    }
}