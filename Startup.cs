using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nettbutikk.Data;
using Nettbutikk.Data.Services;
using Nettbutikk.Factories;
using Nettbutikk.Models;
using Nettbutikk.State;
using Nettbutikk.Workers;
using System;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using Nettbutikk.SignalR;

namespace Nettbutikk
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void CreateRoles(IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();

            var roleManager = provider.GetRequiredService<RoleManager<RoleEntity>>();

            string[] roleNames = { ApplicationRoles.Admin, ApplicationRoles.Customer };

            IdentityResult identityResult;
            foreach (var role in roleNames)
            {
                var roleExists = roleManager.RoleExistsAsync(role).Result;
                if (!roleExists)
                {
                    var roleEntity = new RoleEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = role,
                        Code = role.Equals(ApplicationRoles.Admin) ? ApplicationRoles.AdminRoleCode : ApplicationRoles.CustomerRoleCode
                    };

                    identityResult = roleManager.CreateAsync(roleEntity).Result;

                    if (!identityResult.Succeeded)
                        throw new Exception("Failed adding role to database.");
                }
            }
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WebStoreContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("WebstoreConnectionString")));

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("WebstoreConnectionString")));

            services.AddIdentity<UserEntity, RoleEntity>()
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            //dependencies, services
            services.AddTransient<ILogger<Order>, Logger<Order>>();
            services.AddTransient<DtoMapperService>();
            services.AddTransient<IdentityService>();
            services.AddTransient<OrderService>();
            services.AddTransient<ProductService>();
            services.AddTransient<UserContextService>();
            services.AddTransient<DiscountService>();
            services.AddTransient<PartialDeliveryService>();
            services.AddSingleton<PartialDeliveryHubUserManagerSingleton>();

            //factories
            services.AddSingleton<DeleteUserReceiptFactory>();
            services.AddSingleton<OrderFactory>();
            services.AddSingleton<UserFactory>();
            services.AddSingleton<ProductOrderRelationFactory>();
            services.AddSingleton<OrderReceiptFactory>();
            services.AddSingleton<ProductFactory>();
            services.AddSingleton<EventFactory>();
            services.AddSingleton<PartialDeliveryFactory>();

            //hosted services
            services.AddHostedService<WebstoreProductDelivery>();
            services.AddHostedService<EventListener>();
            //--------------dependencies-------------

            services.AddControllers();
            services.AddSignalR(cfg => cfg.EnableDetailedErrors = true);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Nettbutikk", Version = "v1" });
            });

            CreateRoles(services); //adds roles to the database by using the role manager
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Nettbutikk v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PartialDeliveryHub>("/hubs/partialDelivery");
                endpoints.MapHub<CustomerServiceHub>("/hubs/customerService");
            });
        }
    }
}
