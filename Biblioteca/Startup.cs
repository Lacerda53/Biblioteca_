using Biblioteca._Infra;
using Biblioteca._Repositorio;
using Biblioteca._Repositorio.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Biblioteca
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDB, MSSQL>();
            services.AddTransient<ILivroRepositorio, LivroRepositorio>();
            services.AddTransient<IEmprestimoRepositorio, EmprestimoRepositorio>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMvc(routes => routes.MapRoute("default", "{controller=Livro}/{action=Index}/{id?}"));
        }
    }
}
