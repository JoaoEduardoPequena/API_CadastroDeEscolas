using System.Data;

namespace Infraestructure.Infra_DbAcess
{
    public interface IDataAccess
    {
        void SetDatabase(string? db);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
        IEnumerable<T> Query<T>(string sql, object? parameters = null, CommandType? commandType = null);
        Task<int> ExecuteCommandAsync(params string[] sql);
        T QueryFirstOrDefault<T>(string sql, object? parameters = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string sql, object? parameters = null);

        Task<int> CommandAsync(string query, object o);
        Task<T> ExecuteScalarAsync<T>(string query, object? parameters=null);
        void ExecuteSpAsync(string query, DataTable dt, string tableName);
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters, CommandType type);
    }
}
