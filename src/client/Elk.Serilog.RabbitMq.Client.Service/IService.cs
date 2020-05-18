using System.Collections.Generic;
using Elk.Serilog.RabbitMq.Client.Model;

namespace Elk.Serilog.RabbitMq.Client.Service
{
    public interface IService
    {
        List<Customer> GetSampleData();
        Customer GetCustomerInfoWithCustomerId(int customerId);
    }
}
