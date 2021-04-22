using System.Collections.Generic;

namespace PromotionEngine.Domain.Models
{
    /// <summary>
    /// Representation of a cart.
    /// </summary>
    public class Cart
    {
        public IDictionary<string, int> ProductSkuToCountInCart { get; set; } = new Dictionary<string, int>();
    }
}