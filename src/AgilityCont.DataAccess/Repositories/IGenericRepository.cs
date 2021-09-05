using System.Collections.Generic;

namespace AgilityCont.DataAccess
{
    public interface IGenericRepository<T>
    {
        IEnumerable<T> GetAllPaged(int limit, int offset);
    }
}
