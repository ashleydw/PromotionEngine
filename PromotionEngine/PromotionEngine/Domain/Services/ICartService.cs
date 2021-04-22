using System.Threading.Tasks;
using PromotionEngine.Domain.Models;

namespace PromotionEngine.Domain.Services
{
    public interface ICartService
    {
        public Task<double> TotalPriceAsync(Cart cart);
    }
}