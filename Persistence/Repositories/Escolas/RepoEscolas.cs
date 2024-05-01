using Domain.Domain_Escolas.Entities;
using Domain.Domain_Escolas.Interfaces;
using Domain.Domain_Escolas.Models;
using Infraestructure.Infra_DbAcess;
using Persistence.Helpers;

namespace Persistence.Repositories.Escolas
{
    public class RepoEscolas : IRepoEscolas
    {
        private readonly IDataAccess _bd;
        public RepoEscolas(IDataAccess bd) 
        {
            _bd = bd;
        }
        public async Task<int> CriarEscola(TbEscolas dto)
        {
            var query = SqlQueryGen.GenerateInsertSql(dto, "TbEscolas");
            var result = await _bd.ExecuteCommandAsync (query);
            return result;
        }

        public async Task<int> EditarEscola(TbEscolas dto,int id)
        {
            var query = SqlQueryGen.GenerateUpdateSql(dto, "TbEscolas", "id='"+id+"' ");
            var result = await _bd.ExecuteCommandAsync(query);
            return result;
        }

        public async Task<int> EliminarEscola(int id)
        {
            var query = SqlQueryGen.GenerateDeleteSql("TbEscolas", "id='" +id + "' ");
            var result = await _bd.ExecuteCommandAsync(query);
            return result;
        }

        public async Task<EscolasViewModel?> ListarEscolaPeloId(int id)
        {
            var result = await _bd.QueryAsync<EscolasViewModel>($@"SELECT * FROM TbEscolas with(nolock) where id='{id}' ");
            return result.FirstOrDefault();
        }

        public async Task<List<EscolasViewModel?>> ListarTodasEscola()
        {
            var result = await _bd.QueryAsync<EscolasViewModel>(@"SELECT * FROM TbEscolas with(nolock)");
            return result.ToList();
        }
    }
}
