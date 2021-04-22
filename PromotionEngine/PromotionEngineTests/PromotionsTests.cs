using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Domain.Models;
using PromotionEngine.Domain.Models.Promotions;
using Xunit;

namespace PromotionEngineTests
{
    public class PromotionsTests
    {
        [Theory]
        [InlineData("A", true)]
        [InlineData("B", false)]
        public void TestBuyNPromotionAppliesToCart(string sku, bool expectedApplies)
        {
            // Arrange
            var promo = new BuyNPromotion("A", 2, 10);
            var cart = new Cart()
            {
                ProductSkuToCountInCart = new Dictionary<string, int>()
                {
                    {sku, 10},
                },
            };

            // Act
            var applies = promo.AppliesToCart(cart);

            // Assert
            Assert.Equal(expectedApplies, applies);
        }
        
        [Theory]
        [InlineData(3, 30)]
        [InlineData(7, 25)]
        [InlineData(5, 5)]
        public void TestBuyNPromotionAppliesPromotion(int count, int expectedPrice)
        {
            // Arrange
            var promo = new BuyNPromotion("A", 5, 5);
            var product = new Product("A", 10);

            // Act
            var actualPrice = promo.ApplyPromotion(product, count);

            // Assert
            Assert.Equal(expectedPrice, actualPrice);
        }
        
        [Theory]
        [InlineData(new[] { "A", "B" }, new[] { "A", "B" }, true)]
        [InlineData(new[] { "A", "B" }, new[] { "C", "D" }, false)]
        public void TestBuyPairPromotionAppliesToCart(string[] cartSkus, string[] promoPair, bool expectedApplies)
        {
            // Arrange
            var promo = new BuyPairPromotion(promoPair.First(), promoPair.Last(), 10);
            var cart = new Cart()
            {
                ProductSkuToCountInCart = new Dictionary<string, int>(),
            };

            foreach (var sku in cartSkus)
            {
                cart.ProductSkuToCountInCart[sku] = 10;
            }

            // Act
            var applies = promo.AppliesToCart(cart);

            // Assert
            Assert.Equal(expectedApplies, applies);
        }
        
        [Fact]
        public void TestBuyPairPromotionAppliesPromotion()
        {
            // Arrange
            var promo = new BuyPairPromotion("A", "B", 5);
            var cart = new Cart()
            {
                ProductSkuToCountInCart = new Dictionary<string, int>()
                {
                    { "A", 1 },
                    { "B", 1 }
                }
            };
            var product = new Product("A", 10);

            // Act
            var actualPrice = promo.ApplyPromotion(product, 1);

            // Assert
            Assert.Equal(5, actualPrice);
        }
    }
}