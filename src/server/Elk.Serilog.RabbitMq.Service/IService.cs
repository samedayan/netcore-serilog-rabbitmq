using Elk.Serilog.RabbitMq.Model;
using System.Collections.Generic;

namespace Elk.Serilog.RabbitMq.Service
{
    public interface IService
    {
        List<Customer> GetSampleData();
        void CustomerProcess(Customer customer);
        Customer UpdateCustomers(Customer customer);
    }
}
