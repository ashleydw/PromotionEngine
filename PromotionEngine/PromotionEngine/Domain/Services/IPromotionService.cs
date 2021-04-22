using System.Collections.Generic;
using System.Threading.Tasks;
using PromotionEngine.Domain.Models;
using PromotionEngine.Domain.Models.Promotions;

namespace PromotionEngine.Domain.Services
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetPromotionsForCartProductsAsync(Cart cart);
    }
}