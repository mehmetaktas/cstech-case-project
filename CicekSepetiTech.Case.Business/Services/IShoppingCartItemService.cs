using CicekSepetiTech.Case.Data.DbEntity;
using CicekSepetiTech.Case.Domain.Dto;
using CicekSepetiTech.Case.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CicekSepetiTech.Case.Business.Services
{
    public interface IShoppingCartItemService
    {
        Task<ReturnModel<ShoppingCartItem>> SaveCartItemAsync(ShoppingCartSave item);
        Task<List<ShoppingCartItem>> GetAll();
    }
}