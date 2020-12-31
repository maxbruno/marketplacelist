using System.IO.Compression;
using MarketplaceList.API.Extensions;
using MarketplaceList.API.Filters;
using MarketplaceList.API.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using MarketplaceList.Domain.Interfaces.Notifications;
using MarketplaceList.Infra.Context;
using MarketplaceList.Domain.Interfaces.UoW;
using MarketplaceList.Infra.UoW;
using Microsoft.EntityFrameworkCore;
using MarketplaceList.Domain.Notifications;
using MarketplaceList.Domain.Interfaces.Repository;
using MarketplaceList.Infra.Repository;
using MarketplaceList.Infra.Services;
using System.Net.Http.Headers;
using System;
using MarketplaceList.Domain.Interfaces.Services;
using System.Net;
using Polly;

namespace MarketplaceList.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMvc(options =>
            {
                options.Filters.Add<DomainNotificationFilter>();
                options.EnableEndpointRouting = false;
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            });
            services.Configure<GzipCompressionProviderOptions>(x => x.Level = CompressionLevel.Optimal);
            services.AddResponseCompression(x =>
            {
                x.Providers.Add<GzipCompressionProvider>();
            });

            RegisterHttpClient(services);

            services.AddOpenApiDocument(document =>
                {
                    document.DocumentName = "v1";
                    document.Version = "v1";
                    document.Title = "marketplace list API";
                    document.Description = "marketplace list API";
                });

            this.RegisterServices(services);
            services.AddAutoMapper(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseResponseCompression();

            if (!env.IsProduction())
            {
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseLogMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterHttpClient(IServiceCollection services)
        {
            services.AddHttpClient<ITacoService, TacoService>((s, c) =>
            {
                c.BaseAddress = new Uri(Configuration["API:Taco"]);
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.OrResult(response =>
                    (int)response.StatusCode == (int)HttpStatusCode.InternalServerError)
              .WaitAndRetryAsync(3, retry =>
                   TimeSpan.FromSeconds(Math.Pow(2, retry)) +
                   TimeSpan.FromMilliseconds(new Random(9876).Next(0, 100))))
                          .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.CircuitBreakerAsync(
                               handledEventsAllowedBeforeBreaking: 3,
                               durationOfBreak: TimeSpan.FromSeconds(30)
                        ));
        }

        private void RegisterServices(IServiceCollection services)
        {
            #region Service
            services.AddScoped<IShoppingListService, ShoppingListService>();
            services.AddScoped<IItemService, ItemService>();


            #endregion

            #region Domain

            services.AddScoped<IDomainNotification, DomainNotification>();

            #endregion

            #region Infra

            services.AddDbContext<EntityContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SqliteConnectionString"), b => b.MigrationsAssembly("MarketplaceList.Infra")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IShoppingListRepository, ShoppingListRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();

            #endregion
        }
    }
}
