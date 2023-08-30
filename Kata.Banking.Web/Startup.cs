using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kata.Banking.Core.Abstractions;
using Kata.Banking.Core.Rules;
using Kata.Banking.Core.Service;
using Kata.Banking.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Kata.Banking.Web
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
            services.AddTransient<ITransactionRuleEngine, TransactionRuleService>();
            services.AddTransient<ITransactionRule, MaximumDepositRule>();
            services.AddTransient<ITransactionRule, MaximumPercentageWithdrawnRule>();
            services.AddTransient<ITransactionRule, MinimumAccountBalanceRule>();

            services.AddTransient<IManageUsers, UserService>();
            services.AddTransient<IManageBankAccounts, BankAccountService>();
            services.AddTransient<ICreateTransactions, TransactionService>();
            
            services.AddSingleton<BankRepository>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Kata.Banking.Web", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kata.Banking.Web v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}