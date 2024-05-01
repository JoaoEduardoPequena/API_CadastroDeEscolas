using System.Reflection;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Persistence.Helpers;

class SqlQueryGen
{
    public static string CreateUpdateStatementWithParameters<T>(T obj, string tableName,string condition)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        StringBuilder values = new StringBuilder();
        values.AppendJoin(",", properties.Select(p => $"{p.Name}=@{p.Name}"));

        return $"UPDATE {tableName} SET {values} WHERE {condition};";
    }
    public static string CreateInsertStatementWithParameters<T>(T obj, string tableName)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        StringBuilder values = new StringBuilder();
        values.AppendJoin(",", properties.Select(p => $"@{p.Name}"));

        string parameters = values.ToString();

        return $"INSERT INTO {tableName} ({values.Replace("@", "")}) VALUES ({parameters});";
    }

    public static string GenerateInsertSql<T>(T obj, string tableName)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        StringBuilder columns = new StringBuilder();
        StringBuilder values = new StringBuilder();

        foreach (PropertyInfo prop in properties)
        {
            
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                continue;
            if (prop.PropertyType.IsArray)
                continue;
            
                string columnName = prop.Name;
                object value = prop.GetValue(obj);

                if (columns.Length > 0)
                {
                    columns.Append(", ");
                    values.Append(", ");
                }

                columns.Append(columnName);

                if (value == null)
                {
                    values.Append("NULL");
                }
                else if (prop.PropertyType == typeof(DateTime?) ||prop.PropertyType == typeof(DateTime) )
                {
                    values.Append($"'{((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ss")}'");
                }
                else
                {
                    values.Append($"'{value}'");
                }
        }

        string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values});";
        return sql;
    }

        public static string GenerateUpdateSql<T>(T obj, string tableName, string condition)
    {
        Type type = typeof(T);
        PropertyInfo[] properties = type.GetProperties();

        StringBuilder setValues = new StringBuilder();

        foreach (PropertyInfo prop in properties)
        {
            if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                continue;
            if (prop.PropertyType.IsArray)
                continue;
       
            string columnName = prop.Name;
            object? value = prop.GetValue(obj);

            if (setValues.Length > 0)
            {
                setValues.Append(", ");
            }

            if (value == null)
            {
                setValues.Append($"{columnName} = NULL");
            }
            else if (prop.PropertyType == typeof(DateTime?) ||prop.PropertyType == typeof(DateTime) )

            {
                setValues.Append($"{columnName} = '{((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss")}'");
            }
            else
            {
                setValues.Append($"{columnName} = '{value}'");
            }
           
        }

        string sql = $"UPDATE {tableName} SET {setValues} WHERE {condition};";
        return sql;
    }
    
    public static string GenerateSelectSql(string tableName, string condition)
    {
        string sql = $"SELECT * FROM {tableName} WHERE {condition};";
        return sql;
    }
    
        public static string GeneratePagedSelectSql(string tableName, string condition, int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            string orderByColumn = "Id";
         
            if(condition.Trim().IsNullOrEmpty())
                return  $"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {orderByColumn}) AS RowNum, * FROM {tableName} ) AS PagedResults WHERE RowNum > {skip} AND RowNum <= {skip + pageSize};";
            
           string  sql = $"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {orderByColumn}) AS RowNum, * FROM {tableName} WHERE {condition}) AS PagedResults WHERE RowNum > {skip} AND RowNum <= {skip + pageSize};";
           
            return  sql;
        }
        
        public static string GenerateInClauseSelectSql<T>(string tableName, string columnName, List<T> values)
        {
            if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(columnName) || values == null || !values.Any())
            {
                throw new ArgumentException("Par?metros inv?lidos.");
            }

            string inValues = string.Join(", ", values.Select(v => $"'{v}'"));

            string sql = $"SELECT * FROM {tableName} WHERE {columnName} IN ({inValues});";

            return sql;
        }
    public static string GenerateDeleteSql(string tableName, string condition)
    {
        string sql = $"DELETE FROM {tableName} WHERE {condition};";
        return sql;
    }



}
