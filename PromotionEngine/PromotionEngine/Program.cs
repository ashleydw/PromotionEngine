using System;
using System.Threading.Tasks;
using PromotionEngine.Domain.Models;
using PromotionEngine.Services;

namespace PromotionEngine
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var promoService = new PromotionService();
            var cartService = new CartService(promoService);
            Console.WriteLine("Promotion Engine.");
            Console.WriteLine("(alternatively run the unit tests for the given scenarios)");

            while (true)
            {
                Console.WriteLine("Enter your combo of products as SKU:count,SKU:count...");
                
                var input = Console.ReadLine();
                if (input is null)
                {
                    return;
                }

                var cart = new Cart();
                foreach (var chunk in input.Split(','))
                {
                    var split = chunk.Split(':');
                    if (split.Length == 2)
                    {
                        var sku = split[0];
                        if (int.TryParse(split[1], out var count))
                        {
                            cart.ProductSkuToCountInCart[sku] = count;
                        }
                    }
                }
                
                var price = await cartService.TotalPriceAsync(cart);
                Console.WriteLine($"Total price: {price}");
            }
        }
    }
}