using CicekSepetiTech.Case.Domain.Model;
using System.Threading.Tasks;

namespace CicekSepetiTech.Case.Business.Services
{
    public interface ICustomerService
    {
        Task<ReturnModel<int>> CheckCustomer(int customerId);
    }
}