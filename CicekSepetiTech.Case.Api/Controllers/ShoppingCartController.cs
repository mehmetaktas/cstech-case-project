using CicekSepetiTech.Case.Api.Filters;
using CicekSepetiTech.Case.Business.Services;
using CicekSepetiTech.Case.Data.DbEntity;
using CicekSepetiTech.Case.Domain.Dto;
using CicekSepetiTech.Case.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CicekSepetiTech.Case.Api.Controllers
{
    [Route("api/[controller]")]
    [ValidatorFilter]
    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartItemService _shoppingCartItemService;

        public ShoppingCartController(IShoppingCartItemService shoppingCartItemService)
        {
            _shoppingCartItemService = shoppingCartItemService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShoppingCartSave item)
        {
            var saveCartItem = await _shoppingCartItemService.SaveCartItemAsync(item);

            if (saveCartItem.Result.Status == ReturnStatus.Success)
                return Ok(saveCartItem);
            else
                return BadRequest(saveCartItem);
        }

        [HttpGet]
        [Route("List")]
        [Produces("application/json")]
        public async Task<ActionResult<List<ShoppingCartItem>>> Get() => await _shoppingCartItemService.GetAll();
    }
}