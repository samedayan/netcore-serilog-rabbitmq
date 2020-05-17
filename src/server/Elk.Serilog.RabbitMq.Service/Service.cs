using Elk.Serilog.RabbitMq.Common.ProjectConst;
using Elk.Serilog.RabbitMq.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elk.Serilog.RabbitMq.Service
{
    public class Service : IService
    {
        private readonly ILogger<Service> _logger;
        private readonly IOptions<ProjectSettings> _settings;
        public static List<Customer> Customers = new List<Customer>();

        public Service(ILogger<Service> logger, IOptions<ProjectSettings> settings)
        {
            _logger = logger;
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

        public void CustomerProcess(Customer customer)
        {
            var customerInfo = UpdateCustomers(customer);

            var sender = RabbitMqSender(customerInfo);

            _logger.LogInformation($"RabbitMq Send Data: {sender} - {DateTime.Now:dd.MM.yyyy}");
        }

        public Customer UpdateCustomers(Customer customer)
        {
            var data = Customers.FirstOrDefault(s => s.Id == customer.Id);

            if (data == null)
            {
                string errorMessage = $"Customer Not Found.";

                _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }

            // TODO: Update Customer Item

            return data;
        }

        public string RabbitMqSender(Customer customer)
        {
            var factory = new ConnectionFactory() { HostName = _settings.Value.RabbitMqHostName };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: customer.Name,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var data = JsonConvert.SerializeObject(customer);
                
                var body = Encoding.UTF8.GetBytes(data);

                channel.BasicPublish(exchange: "",
                    routingKey: customer.Name,
                    basicProperties: null,
                    body: body);

                return $"[x] Sent {customer.Name}";
            }
        }
    }
}
