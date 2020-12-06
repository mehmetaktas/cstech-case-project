using CicekSepetiTech.Case.Api.Controllers;
using CicekSepetiTech.Case.Business.Services;
using CicekSepetiTech.Case.Data.DbEntity;
using CicekSepetiTech.Case.Data.Repositories.Base;
using CicekSepetiTech.Case.Domain.Dto;
using CicekSepetiTech.Case.Domain.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace CicekSepetiTech.Case.Tests
{
    [TestClass]
    public class ShoppingCartControllerTests
    {
        [TestMethod]
        public async Task Customer_Null_BadRequest()
        {
            // Given
            var returnModel = new ReturnModel<int>();
            var model = new ShoppingCartSave
            {
                CustomerInfo = new CustomerInfo
                {
                    CustomerId = 1
                }
            };

            var productServiceMock = new Mock<IProductService>();
            var shoppingCartItemRepositoryMock = new Mock<IRepository<ShoppingCartItem>>();

            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(x => x.CheckCustomer(model.CustomerInfo.CustomerId.Value)).Returns(Task.FromResult(returnModel));

            var service = new ShoppingCartItemService(productServiceMock.Object, customerServiceMock.Object, shoppingCartItemRepositoryMock.Object);
            var controller = new ShoppingCartController(service);

            // When
            var result = await controller.Post(model);

            // Assertions
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

        }

        [TestMethod]
        public async Task Product_Not_Valid_BadRequest()
        {
            // Given
            var returnModel = new ReturnModel<ValidProductDto>();
            var validReturnModel = new ReturnModel<int>() { Result = new ReturnResult { Status = ReturnStatus.Success } };
            var model = new ShoppingCartSave
            {
                ProductId = 1,
                CustomerInfo = new CustomerInfo
                {
                    CustomerId = 1
                }
            };

            var shoppingCartItemRepositoryMock = new Mock<IRepository<ShoppingCartItem>>();

            var customerServiceMock = new Mock<ICustomerService>();
            customerServiceMock.Setup(x => x.CheckCustomer(model.CustomerInfo.CustomerId.Value)).Returns(Task.FromResult(validReturnModel));

            var productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(x => x.ValidForSale(model.ProductId)).Returns(Task.FromResult(returnModel));

            var service = new ShoppingCartItemService(productServiceMock.Object, customerServiceMock.Object, shoppingCartItemRepositoryMock.Object);
            var controller = new ShoppingCartController(service);

            // When
            var result = await controller.Post(model);

            // Assertions
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));

        }

        [TestMethod]
        public async Task SaveCartItem_Error_BadRequest()
        {
            // Given
            var returnModel = new ReturnModel<ShoppingCartItem>();
            var model = new ShoppingCartSave
            {
                ProductId = 1
            };

            var shoppingCartItemServiceMock = new Mock<IShoppingCartItemService>();
            shoppingCartItemServiceMock.Setup(x => x.SaveCartItemAsync(model)).Returns(Task.FromResult(returnModel));

            var controller = new ShoppingCartController(shoppingCartItemServiceMock.Object);

            // When
            var result = await controller.Post(model);

            // Assertions
            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task Return_Ok()
        {
            // Given
            var cartItemReturnModel = new ReturnModel<ShoppingCartItem>() { Result = new ReturnResult { Status = ReturnStatus.Success } };
            var model = new ShoppingCartSave
            {
                ProductId = 1
            };

            var shoppingCartItemServiceMock = new Mock<IShoppingCartItemService>();
            shoppingCartItemServiceMock.Setup(x => x.SaveCartItemAsync(model)).Returns(Task.FromResult(cartItemReturnModel));

            var controller = new ShoppingCartController(shoppingCartItemServiceMock.Object);

            // When
            var result = await controller.Post(model);

            // Assertions
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
        }
    }
}