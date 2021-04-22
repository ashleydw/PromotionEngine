using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PromotionEngine.Domain.Models;
using PromotionEngine.Domain.Models.Promotions;
using PromotionEngine.Domain.Services;

namespace PromotionEngine.Services
{
    /// <summary>
    /// Cart service that calculates the cart totals, including promotions.
    /// </summary>
    public class CartService : ICartService
    {
        private readonly IPromotionService promoService;
        
        // Coded for the demo, likely loaded from the DB.
        private static IEnumerable<Product> ProductList() =>
            new List<Product>()
            {
                new("A", 50),
                new("B", 30),
                new("C", 20),
                new("D", 15),
            };

        public CartService(IPromotionService promoService)
        {
            this.promoService = promoService;
        }

        public async Task<double> TotalPriceAsync(Cart cart)
        {
            // faked lookup, in reality a DB call or similar to get the products lists and prices.
            var productsBySku = ProductList().ToDictionary(p => p.Sku, p => p);
            
            var promosForCart = await GetPromotionsForCartAsync(cart);
            var totalPrice = 0d;
            
            // process the promos, keeping track of the sku's processed
            var processedSkus = new List<string>(cart.ProductSkuToCountInCart.Count);
            foreach (var promo in promosForCart)
            {
                processedSkus.AddRange(promo.ProductSkus);
                var sku = promo.ProductSkus.First(); // only need the first for pair promos
                totalPrice += promo.ApplyPromotion(productsBySku[sku], cart.ProductSkuToCountInCart[sku]);
            }

            // process all non-promo products
            var unprocessedSkus = cart.ProductSkuToCountInCart.Where(sc => !processedSkus.Contains(sc.Key));
            foreach (var (sku, total) in unprocessedSkus)
            {
                totalPrice += productsBySku[sku].Price * total;
            }

            return totalPrice;
        }

        private async Task<IEnumerable<Promotion>> GetPromotionsForCartAsync(Cart cart)
        {
            // assuming fetch by product sku to database
            var promosForCartProducts = await this.promoService.GetPromotionsForCartProductsAsync(cart);
            
            // filter out some promos that require more than 1 product
            var validPromosForCart = promosForCartProducts.Where(p => p.AppliesToCart(cart));

            return validPromosForCart;
        }
    }
}