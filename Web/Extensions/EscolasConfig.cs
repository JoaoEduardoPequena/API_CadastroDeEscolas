using Domain.Domain_Escolas.Interfaces;
using Persistence.Repositories.Escolas;


namespace Web.Extensions
{
    public  static class EscolasConfig
    {
        public static void EscolaConfig(this IServiceCollection services)
        {
            services.AddScoped<IRepoEscolas, RepoEscolas>();
        }
    }
}
