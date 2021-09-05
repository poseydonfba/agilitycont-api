using System.Collections.Generic;

namespace AgilityCont.DataAccess
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public IEnumerable<T> GetAllPaged(int limit, int offset)
        {
            var tableName = typeof(T).Name;
            // assuming here you want the newest rows first, and column name is "created_date"
            // may also wish to specify the exact columns needed, rather than *
            //var query = "SELECT * FROM @TableName ORDER BY created_date DESC Limit @Limit Offset @Offset";
            //var results = Connection.Query<T>(query, new { Limit = limit, Offset = offset });
            //return results;
            return null;
        }
    }
}
