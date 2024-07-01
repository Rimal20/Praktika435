using Praktika.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Database
{
    private readonly SQLiteAsyncConnection _database;


    public Database(string dbPath)
    {
        _database = new SQLiteAsyncConnection(dbPath);
        
        _database.CreateTableAsync<Class>().Wait();
        InitializeDatabase().Wait();
        
       
    }
    private async Task InitializeAsync()
    {
        await _database.CreateTableAsync<Class>().ConfigureAwait(false);
    }

    public async Task<List<Class>> GetClassesAsync()
    {
        return await _database.Table<Class>().ToListAsync().ConfigureAwait(false);
    }

    public async Task<int> InsertAsync(Class item)
    {
        return await _database.InsertAsync(item).ConfigureAwait(false);
    }
    private async Task InitializeDatabase()
    {
        await _database.CreateTableAsync<Class>().ConfigureAwait(false);
        await _database.CreateTableAsync<User>().ConfigureAwait(false);

        // Создаем временную таблицу для хранения данных из старой таблицы User
        await _database.ExecuteAsync("CREATE TABLE IF NOT EXISTS UserTemp (Id INTEGER PRIMARY KEY AUTOINCREMENT, Username TEXT, Password TEXT, Role TEXT, FirstName TEXT, LastName TEXT, MiddleName TEXT)").ConfigureAwait(false);

        // Копируем данные из старой таблицы User в UserTemp
        await _database.ExecuteAsync("INSERT INTO UserTemp (Id, Username, Password, Role) SELECT Id, Username, Password, Role FROM User").ConfigureAwait(false);

        // Удаляем старую таблицу User
        await _database.ExecuteAsync("DROP TABLE IF EXISTS User").ConfigureAwait(false);

        // Переименовываем временную таблицу UserTemp в User
        await _database.ExecuteAsync("ALTER TABLE UserTemp RENAME TO User").ConfigureAwait(false);
    }

    // CRUD операции для Users
    public Task<List<User>> GetUsersAsync()
    {
        return _database.Table<User>().ToListAsync();
    }

    public Task<User> GetUserAsync(string username)
    {
        return _database.Table<User>().Where(u => u.Username == username).FirstOrDefaultAsync();
    }

    public Task<int> SaveUserAsync(User user)
    {
        if (user.Id != 0)
        {
            return _database.UpdateAsync(user);
        }
        else
        {
            return _database.InsertAsync(user);
        }
    }

    public Task<int> DeleteUserAsync(User user)
    {
        return _database.DeleteAsync(user);
    }

    // CRUD операции для Classes
    

    public Task<Class> GetClassAsync(int id)
    {
        return _database.Table<Class>().Where(c => c.Id == id).FirstOrDefaultAsync();
    }

    public Task<int> SaveClassAsync(Class classObj)
    {
        if (classObj.Id != 0)
        {
            return _database.UpdateAsync(classObj);
        }
        else
        {
            return _database.InsertAsync(classObj);
        }
    }

    public Task<int> DeleteClassAsync(Class classObj)
    {
        return _database.DeleteAsync(classObj);
    }

    public Task<int> UpdateClassAsync(Class lesson)
    {
        return _database.UpdateAsync(lesson);
    }
    public Task<List<Class>> SearchClassesBySubjectAsync(string subject)
    {
        return _database.Table<Class>().Where(c => c.Subject.Contains(subject)).ToListAsync();
    }
    public async Task<List<Class>> SearchClassesAsync(string subject)
    {
        return await _database.Table<Class>()
                         .Where(c => c.Subject.ToLower().Contains(subject.ToLower()))
                         .ToListAsync();
    }
    // Другие CRUD операции остаются без изменений
}