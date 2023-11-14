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

    // Clear or truncate the SQLite database
    public void ClearDatabase()
    {
        try
        {
            _database.DropTableAsync<Student>();
            _database.CreateTableAsync<Student>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error clearing database: {ex.Message}");
        }
    }

    #region C R U D Operations
    //C
    public async Task AddStudentAsync(Student student)
    {
        await _database.InsertAsync(student);
    }

    //R
    public async Task<List<Student>> GetStudentsAsync()
    {
        return await _database.Table<Student>().ToListAsync(); //SELECT * FROM STUDENT(TABLE)
    }

    //U
    public async Task UpdateStudentAsync(Student student)
    {
        await _database.UpdateAsync(student);
    }

    //D
    public async Task DeleteStudentAsync(Student student)
    {
        await _database.DeleteAsync(student);
    }
    #endregion

}
