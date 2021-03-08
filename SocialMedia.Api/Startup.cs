using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Filters;
using SocialMedia.Infrastructure.Repositories;
using System;

namespace SocialMedia.Api
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Ac� tomo el profile que cre� en la capa infrastructure y lo agrego a la api.

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options => {
                    //options.SuppressModelStateInvalidFilter = true; // invalido la validaci�n del modelo por default, la valido con un filtro.
                });

            services.AddDbContext<SocialMediaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SocialMedia")
                )
            );

            // Resolvemos la implementaci�n de la dependencia
            services.AddTransient<IPostRepository, PostRepository>();
            //services.AddTransient<IPostRepository, PostMongoRepository>(); // Ejemplo de c�mo cambiar de DB con Inyecci�n de dependencias.

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>(); // agrego filtro de manera global, para todos los controllers
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
