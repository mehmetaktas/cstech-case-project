using CicekSepetiTech.Case.Domain.Dto;
using CicekSepetiTech.Case.Domain.Validator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace CicekSepetiTech.Case.Tests
{
    [TestClass]
    public class ControllerValidationTests
    {
        private ShoppingCartSaveValidator Validator { get; }

        public ControllerValidationTests()
        {
            Validator = new ShoppingCartSaveValidator();
        }

        [TestMethod]
        public void Not_Allow_Empty_ProductId()
        {
            // Given
            var shoppingCartItemModel = new ShoppingCartSave
            {
                CustomerInfo = new CustomerInfo
                {
                    CustomerCode = "ABC"
                },
                Quantity = 1,
                DeliveryDateTime = DateTime.Now
            };

            // When
            var result = Validator.Validate(shoppingCartItemModel);

            // Assertions
            Assert.IsTrue(result.Errors.Any(x => x.PropertyName == "ProductId"));
        }

        [TestMethod]
        public void Not_Allow_Empty_Quantity()
        {
            // Given
            var shoppingCartItemModel = new ShoppingCartSave
            {
                CustomerInfo = new CustomerInfo
                {
                    CustomerCode = "ABC"
                },
                ProductId = 1,
                DeliveryDateTime = DateTime.Now
            };

            // When
            var result = Validator.Validate(shoppingCartItemModel);

            // Assertions
            Assert.IsTrue(result.Errors.Any(x => x.PropertyName == "Quantity"));
        }

        [TestMethod]
        public void Allow_Empty_DeliveryDateTime()
        {
            // Given
            var shoppingCartItemModel = new ShoppingCartSave
            {
                CustomerInfo = new CustomerInfo
                {
                    CustomerCode = "ABC"
                },
                ProductId = 1
            };

            // When
            var result = Validator.Validate(shoppingCartItemModel);

            // Assertions
            Assert.IsFalse(result.Errors.Any(x => x.PropertyName == "DeliveryDateTime"));
        }

        [TestMethod]
        public void CustomerInfo_Both_Cannot_Be_Full()
        {
            // Given
            var shoppingCartItemModel = new ShoppingCartSave
            {
                CustomerInfo = new CustomerInfo
                {
                    CustomerCode = "ABC",
                    CustomerId = 1
                },
                Quantity = 1,
                DeliveryDateTime = DateTime.Now
            };

            // When
            var result = Validator.Validate(shoppingCartItemModel);

            // Assertion
            string[] proprtyNames = { "CustomerInfo.CustomerCode", "CustomerInfo.CustomerId" };
            Assert.IsTrue(result.Errors.Any(x => proprtyNames.Contains(x.PropertyName)));
        }

        [TestMethod]
        public void CustomerCode_May_Be_Empty()
        {
            // Given
            var shoppingCartItemModel = new ShoppingCartSave
            {
                CustomerInfo = new CustomerInfo
                {
                    CustomerId = 1
                },
                Quantity = 1,
                DeliveryDateTime = DateTime.Now,
                ProductId = 1
            };

            // When
            var result = Validator.Validate(shoppingCartItemModel);

            // Assertions
            Assert.IsTrue(!result.Errors.Any());
        }

        [TestMethod]
        public void CustomerId_May_Be_Empty()
        {
            // Given
            var shoppingCartItemModel = new ShoppingCartSave
            {
                CustomerInfo = new CustomerInfo
                {
                    CustomerCode = "A"
                },
                Quantity = 1,
                DeliveryDateTime = DateTime.Now,
                ProductId = 1
            };

            // When
            var result = Validator.Validate(shoppingCartItemModel);

            // Assertions
            Assert.IsTrue(!result.Errors.Any());
        }
    }
}