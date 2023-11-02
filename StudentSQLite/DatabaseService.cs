// DatabaseService.cs
using SQLite;
using System.IO;

public class DatabaseService
{
    SQLiteAsyncConnection _database;

    public DatabaseService()
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Student.db");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Student>().Wait();
    }

    #region C R U D Operations
    public async Task AddStudentAsync(Student student)
    {
        await _database.InsertAsync(student);
    }

    public async Task<List<Student>> GetStudentsAsync()
    {
        return await _database.Table<Student>().ToListAsync();
    }
    public async Task UpdateStudentAsync(Student student)
    {
        await _database.UpdateAsync(student);
    }

    public async Task DeleteStudentAsync(Student student)
    {
        await _database.DeleteAsync(student);
    }
    #endregion

}
