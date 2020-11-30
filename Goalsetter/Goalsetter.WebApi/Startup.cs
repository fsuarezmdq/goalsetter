using Goalsetter.AppServices;
using Goalsetter.AppServices.Rentals;
using Goalsetter.DataAccess;
using Goalsetter.DataAccess.Extensions;
using Goalsetter.DataAccess.Repositories;
using Goalsetter.WebApi.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Goalsetter.WebApi
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
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Goalsetter.WebApi", Version = "v1" });
            });

            services.AddDbContext<AppContext>(options => 
                options.UseSqlServer(Configuration["ConnectionString"], x => x.MigrationsAssembly("Goalsetter.DataAccess"))
            );

            services.AddSingleton<Messages>();
            services.AddSingleton<IRentalService, RentalService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            services.AddHandlers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Goalsetter.WebApi v1"));
            }
           
            app.MigrateDatabase();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandler>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            
        }
    }
}
