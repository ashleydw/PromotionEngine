using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEngine.Domain.Models;
using PromotionEngine.Domain.Models.Promotions;
using PromotionEngine.Domain.Services;

namespace PromotionEngine.Services
{
    /// <summary>
    /// Promotions service for retrieving promotions.
    /// </summary>
    public class PromotionService : IPromotionService
    {
        // Faked data that likely comes from a DB.
        private static IList<Promotion> FakePromotions() =>
            new List<Promotion>()
            {
                new BuyNPromotion("A", 3, 130),
                new BuyNPromotion("B", 2, 45),
                new BuyPairPromotion("C", "D", 30),
            };
        
        public async Task<IEnumerable<Promotion>> GetPromotionsForCartProductsAsync(Cart cart)
        {
            // faked call and async for demo, assumed to be some DB call with a something like a
            // WHERE product.sku IN (cartSkus)
            var cartSkus = cart.ProductSkuToCountInCart.Keys;
            return await Task.FromResult(FakePromotions().Where(p => p.ProductSkus.Union(cartSkus).Any()));
        }
    }
}