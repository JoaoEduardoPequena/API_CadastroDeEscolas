using Domain.Domain_Escolas.Entities;
using Domain.Domain_Escolas.Models;

namespace Domain.Domain_Escolas.Interfaces
{
    public interface IRepoEscolas
    {
        Task<int> CriarEscola(TbEscolas dto);
        Task<int> EditarEscola(TbEscolas dto, int id);
        Task<int> EliminarEscola(int id);
        Task<EscolasViewModel?> ListarEscolaPeloId(int id);
        Task<List<EscolasViewModel?>> ListarTodasEscola();
    }
}
