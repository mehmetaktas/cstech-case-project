using CicekSepetiTech.Case.Data.DbEntity;
using CicekSepetiTech.Case.Data.Repositories.Base;
using CicekSepetiTech.Case.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CicekSepetiTech.Case.Business.Services
{
    public class CustomerService :  ICustomerService
    {
        IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ReturnModel<int>> CheckCustomer(int customerId)
        {
            var model = new ReturnModel<int>();

            bool hasCustomer = await _customerRepository.TableNoTracking.AnyAsync(x => x.Id == customerId);
            if (!hasCustomer)
            {
                model.Result.Message = "Müşteri bulunamadı!";
                return model;
            }

            model.Data = customerId;
            model.Result.Status = ReturnStatus.Success;
            return model;
        }
    }
}