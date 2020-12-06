using CicekSepetiTech.Case.Domain.Dto;
using CicekSepetiTech.Case.Domain.Model;
using System.Threading.Tasks;

namespace CicekSepetiTech.Case.Business.Services
{
    public interface IProductService 
    {
        Task<ReturnModel<ValidProductDto>> ValidForSale(int productId);
    }
}