using Domain.Costumers;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MySolution.Application.Services;
using System;
using static Domain.Costumers.Client;

namespace MySolution.WebAPi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public  IMongoClient _client { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddApplication();
            services.AddInfra(Configuration);
            services.AddHttpContextAccessor();
            services.AddSingleton<ClientService>();

            services.AddScoped<IValidator<Client>, ClientValidator>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MediatR Web Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MediatR Web Api v1"));


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
