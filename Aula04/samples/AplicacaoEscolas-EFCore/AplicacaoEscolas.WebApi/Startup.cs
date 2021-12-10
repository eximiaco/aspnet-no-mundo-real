using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AplicacaoEscolas.WebApi.Hosting.Extensions;
using AplicacaoEscolas.WebApi.Infraestrutura;
using Microsoft.EntityFrameworkCore;

namespace AplicacaoEscolas.WebApi
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
            services.AddScoped<TurmasRepositorio>();
            services.AddScoped<AlunosRepositorio>();
            services.AddDapper();
            services.AddDbContext<EscolasDbContext>(
                o =>
                {
                    o.UseSqlServer("name=ConnectionStrings:Escolas");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
