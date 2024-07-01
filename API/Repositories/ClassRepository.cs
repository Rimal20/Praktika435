using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;


namespace API.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly SQLiteAsyncConnection _database;

        public ClassRepository(SQLiteAsyncConnection database)
        {
            _database = database;
        }

        public async Task<List<Class>> GetClassesAsync()
        {
            return await _database.Table<Class>().ToListAsync();
        }

        public async Task<Class> GetClassAsync(int id)
        {
            return await _database.Table<Class>().Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddClassAsync(Class classObj)
        {
            await _database.InsertAsync(classObj);
        }

        public async Task UpdateClassAsync(Class classObj)
        {
            await _database.UpdateAsync(classObj);
        }

        public async Task DeleteClassAsync(int id)
        {
            var classObj = await GetClassAsync(id);
            if (classObj != null)
            {
                await _database.DeleteAsync(classObj);
            }
        }
    }
}
