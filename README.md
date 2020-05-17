# netcore-serilog
├── netcore 3.1
├── elasticsearch serilog
├── central logging
├── rabbitmq
├── finance multi thread lock

# RabbitMQ Docker Run
docker run -d --hostname my-rabbit --name myrabbit -e RABBITMQ_DEFAULT_USER=admin -e RABBITMQ_DEFAULT_PASS=123456 -p 5672:5672 -p 15672:15672 rabbitmq:3-management
