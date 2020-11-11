using System;
using ConsumidorService.Consumers;
using GreenPipes;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Helpers;

namespace ConsumidorService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RabbitmqConfig>(Configuration.GetSection("Rabbitmq"));
            services.AddMassTransit(x =>
            {
                var configSections = Configuration.GetSection("Rabbitmq");
                var host = configSections["Host"];
                var queueName = configSections["QueueName"];
                var endPoint = configSections["EndPoint"];
                var userName = configSections["UserName"];
                var password = configSections["Password"];
                var virtualHost = configSections["VirtualHost"];
                var port = Convert.ToUInt16(configSections["Port"]);
                
                x.AddConsumer<TicketConsumer>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseHealthCheck(provider);
                    
                    cfg.Host(new Uri("rabbitmq://" + host), host =>
                    {
                        host.Username(userName);
                        host.Password(password);
                    });
                    cfg.ReceiveEndpoint(endPoint, ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<TicketConsumer>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}