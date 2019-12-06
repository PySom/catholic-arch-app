using System.Collections.Generic;
using System.Threading.Tasks;

namespace LagosArch.Services
{
    public interface IManager<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string url, bool forceRefresh = false);
    }
}
