using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public interface IClassRepository
    {
        Task<List<Class>> GetClassesAsync();
        Task<Class> GetClassAsync(int id);
        Task AddClassAsync(Class classObj);
        Task UpdateClassAsync(Class classObj);
        Task DeleteClassAsync(int id);
    }
}
