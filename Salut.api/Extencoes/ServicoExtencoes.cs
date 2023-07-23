using Microsoft.EntityFrameworkCore;
using Salut.business;
using Salut.infra;
using Salut.models;

namespace Salut.api.Extencoes
{
    public static class ServicoExtencoes
    {
        public static void ConfigurarServicos(this IServiceCollection services, IConfiguration configuration)
        {           
            services.AddDbContext<EntityContext>(item => item.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICadastroNotaFiscalBuziness, CadastroNotaFiscalBuziness>();
            services.AddHttpClient();
        }

    }
}
