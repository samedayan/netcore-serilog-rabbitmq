using Elk.Serilog.RabbitMq.Client.Common.ProjectConst;
using Elk.Serilog.RabbitMq.Client.Model;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;

namespace Elk.Serilog.RabbitMq.Client.Service
{
    public class Service : IService
    {
        private readonly IOptions<ProjectSettings> _settings;
        public List<Customer> Customers;

        public Service(IOptions<ProjectSettings> settings)
        {
            _settings = settings;
            Customers = new List<Customer>();
            Customers = GetSampleData();
        }

        public List<Customer> GetSampleData()
        {
            Customers.Add(new Customer
            {
                Id = 1,
                Name = "Sample1",
                Price = 66.7M
            });

            Customers.Add(new Customer
            {
                Id = 11,
                Name = "Sample11",
                Price = 71M
            });

            Customers.Add(new Customer
            {
                Id = 71,
                Name = "Sample1",
                Price = 1050.25M
            });

            return Customers;
        }

        public Customer GetCustomerInfoWithCustomerId(int customerId)
        {
            return Customers.FirstOrDefault(s => s.Id == customerId);
        }
    }
}
