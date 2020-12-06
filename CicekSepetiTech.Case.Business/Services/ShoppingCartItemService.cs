using CicekSepetiTech.Case.Data;
using CicekSepetiTech.Case.Data.DbEntity;
using CicekSepetiTech.Case.Data.Repositories.Base;
using CicekSepetiTech.Case.Domain.Dto;
using CicekSepetiTech.Case.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepetiTech.Case.Business.Services
{
    public class ShoppingCartItemService : IShoppingCartItemService
    {
        IRepository<ShoppingCartItem> _shoppingCartItemRepository;
        IProductService _productService;
        ICustomerService _customerService;

        public ShoppingCartItemService(IProductService productService, ICustomerService customerService, IRepository<ShoppingCartItem> shoppingCartItemRepository)
        {
            _productService = productService;
            _customerService = customerService;
            _shoppingCartItemRepository = shoppingCartItemRepository;
        }

        public async Task<List<ShoppingCartItem>> GetAll()
        {
            return await _shoppingCartItemRepository.TableNoTracking.ToListAsync();
        }

        public async Task<ReturnModel<ShoppingCartItem>> SaveCartItemAsync(ShoppingCartSave item)
        {
            var model = new ReturnModel<ShoppingCartItem>();

            if (item.CustomerInfo.CustomerId.HasValue)
            {
                var customerCheck = await _customerService.CheckCustomer(item.CustomerInfo.CustomerId.Value);
                if (customerCheck.Result.Status != ReturnStatus.Success)
                {
                    model.Result = customerCheck.Result;
                    return model;
                }
            }

            var productValid = await _productService.ValidForSale(item.ProductId);
            if (productValid.Result.Status != ReturnStatus.Success)
            {
                model.Result = productValid.Result;
                return model;
            }

            ShoppingCartItem shoppingCartItem = await _shoppingCartItemRepository.Table.FirstOrDefaultAsync(x => ((!string.IsNullOrEmpty(item.CustomerInfo.CustomerCode) && x.CustomerCode == item.CustomerInfo.CustomerCode) || ((item.CustomerInfo.CustomerId.HasValue && x.CustomerId == item.CustomerInfo.CustomerId))) && x.ProductId == item.ProductId);
            bool insert = false;

            //Ürün ilk kez sepete atılıyor.
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    CurrentPriceInclTax = productValid.Data.PriceInclTax,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    DeliveryDateTime = item.DeliveryDateTime
                };

                if (item.CustomerInfo.CustomerId.HasValue)
                    shoppingCartItem.CustomerId = item.CustomerInfo.CustomerId;
                else
                    shoppingCartItem.CustomerCode = item.CustomerInfo.CustomerCode;

                insert = true;
            }
            //Ürün zaten müşterinin sepetinde var.
            else
            {
                shoppingCartItem.CurrentPriceInclTax = productValid.Data.PriceInclTax;
                shoppingCartItem.DeliveryDateTime = item.DeliveryDateTime;
                shoppingCartItem.Quantity += item.Quantity;
                shoppingCartItem.UpdateDate = DateTime.Now;
            }

            int totalStock = productValid.Data.TotalStock;
            if (shoppingCartItem.Quantity > totalStock)
            {
                shoppingCartItem.Quantity = totalStock;
                model.Result.Message = $"Üründen maksimum {totalStock} adet satın alınabilir. Sepete eklenen ürün sayınız {totalStock} olarak güncellenmiştir.";
            }

            try
            {
                if (insert)
                    await _shoppingCartItemRepository.AddAsync(shoppingCartItem);
                else
                    await _shoppingCartItemRepository.SaveChangesAsync();

                model.Data = shoppingCartItem;
                model.Result.Status = ReturnStatus.Success;
            }
            catch
            {
                model.Result.Message = "Ürün sepete eklenirken problem meydana geldi!";
                //Log...
            }

            return model;
        }
    }
}