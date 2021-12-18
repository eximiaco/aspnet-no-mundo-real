using AplicacaoEscolas.WebApi.Hosting.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AplicacaoEscolas.WebApi.Hosting.Extensions;
using AplicacaoEscolas.WebApi.Hosting.Filtros;
using AplicacaoEscolas.WebApi.Infraestrutura;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

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
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            });
            services.AddScoped<TurmasRepositorio>();
            services.AddScoped<AlunosRepositorio>();
            services.AddScoped<MatriculasRepositorio>();
            services.AddDapper();
            services.AddDbContext<EscolasDbContext>(
                o =>
                {
                    o.UseSqlServer("name=ConnectionStrings:Escolas");
                });

            services.Configure<MatriculasOptions>(Configuration.GetSection(MatriculasOptions.Matriculas));

            var hcBuilder = services.AddHealthChecks();
            hcBuilder.AddCheck("self", () => HealthCheckResult.Healthy());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            // Write streamlined request completion events, instead of the more verbose ones from the framework.
            // To use the default framework request logging instead, remove this line and set the "Microsoft"
            // level in appsettings.json to "Information".
            app.UseSerilogRequestLogging();
           
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}
