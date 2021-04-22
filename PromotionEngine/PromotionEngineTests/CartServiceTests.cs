using System.Collections.Generic;
using System.Threading.Tasks;
using PromotionEngine.Domain.Models;
using PromotionEngine.Services;
using Xunit;

namespace PromotionEngineTests
{
    public class CartServiceTests
    {
        private readonly CartService cartSut;

        public CartServiceTests()
        {
            // mocked data is inside the CartService, in a real world unit test, it would have to be setup here too
            var promoService = new PromotionService();
            this.cartSut = new CartService(promoService);
        }

        // Scenario A
        [Fact]
        public async Task NoPromosApply_ExpectsOriginal()
        {
            // Arrange
            var cart = new Cart()
            {
                ProductSkuToCountInCart = new Dictionary<string, int>()
                {
                    { "A", 1 },
                    { "B", 1 },
                    { "C", 1 },
                },
            };

            const double expectedPrice = 100;

            // Act
            var total = await this.cartSut.TotalPriceAsync(cart);

            // Assert
            Assert.Equal(expectedPrice, total);
        }
        
        // Scenario B
        [Fact]
        public async Task TestBuyNForPrice_ExpectsDiscount()
        {
            // Arrange
            var cart = new Cart()
            {
                ProductSkuToCountInCart = new Dictionary<string, int>()
                {
                    { "A", 5 },
                    { "B", 5 },
                    { "C", 1 },
                },
            };

            const double expectedPrice = 370;

            // Act
            var total = await this.cartSut.TotalPriceAsync(cart);

            // Assert
            Assert.Equal(expectedPrice, total);
        }
        
        // Scenario C
        [Fact]
        public async Task TestBuyPairForPrice_ExpectsDiscount()
        {
            // Arrange
            var cart = new Cart()
            {
                ProductSkuToCountInCart = new Dictionary<string, int>()
                {
                    { "A", 3 },
                    { "B", 5 },
                    { "C", 1 },
                    { "D", 1 },
                },
            };

            const double expectedPrice = 280;

            // Act
            var total = await this.cartSut.TotalPriceAsync(cart);

            // Assert
            Assert.Equal(expectedPrice, total);
        }
    }
}