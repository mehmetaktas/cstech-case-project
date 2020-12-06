using CicekSepetiTech.Case.Data.DbEntity;
using CicekSepetiTech.Case.Data.Repositories.Base;
using CicekSepetiTech.Case.Domain.Dto;
using CicekSepetiTech.Case.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CicekSepetiTech.Case.Business.Services
{
    public class ProductService : IProductService
    {
        IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ReturnModel<ValidProductDto>> ValidForSale(int productId)
        {
            var model = new ReturnModel<ValidProductDto>();

            var product = await _productRepository
                                .TableNoTracking
                                .Include(x => x.ProductAvailability)
                                .FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null || product.ProductAvailability == null)
            {
                model.Result.Message = "Ürün bulunamadı!";
                return model;
            }

            if (!product.ProductAvailability.Salable || !product.ProductAvailability.Display || product.SalableQuantity == 0 || !product.Published)
            {
                model.Result.Message = "Sepete eklenmek istenen ürün satın alınabilme şartlarını karşılamıyor!";
                return model;
            }

            model.Data = new ValidProductDto { PriceInclTax = product.PriceInclTax, TotalStock = product.SalableQuantity };
            model.Result.Status = ReturnStatus.Success;
            return model;
        }
    }
}