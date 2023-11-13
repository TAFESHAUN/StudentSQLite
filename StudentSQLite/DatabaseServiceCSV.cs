using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSQLite
{
    public class DatabaseServiceCSV
    {
        //Generate File -> App localdata
        string csvFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "studentsDB.csv");

        //Point to File in Solution -> Using @ reference
        //Update this reference to your own file system
        //string csvFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"C:\\Users\\User\\Desktop\\Solutions\\StudentSQLite\\StudentSQLite\\studentsDB.csv");

        public DatabaseServiceCSV()
        {
            SetupCsvFileAsync().Wait(); // Wait synchronously for setup to complete
        }

        private async Task SetupCsvFileAsync()//Can ignore due to .Wait call for now.
        {
            try
            {
                if (!File.Exists(csvFilePath))
                {
                    using (var writer = new StreamWriter(csvFilePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteField("Id");
                        csv.WriteField("GivenName");
                        csv.WriteField("FamilyName");
                        csv.WriteField("StudentNumber");
                        csv.WriteField("EnrollmentDate");
                        csv.NextRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting up CSV file: {ex.Message}");
            }
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            List<Student> students = new List<Student>();

            try
            {
                if (File.Exists(csvFilePath))
                {
                    await Task.Run(() =>
                    {
                        using (var reader = new StreamReader(csvFilePath))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            students = csv.GetRecords<Student>().ToList();
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading CSV file: {ex.Message}");
            }

            return students;
        }

        public async Task AddStudentAsync(Student student)
        {
            try
            {
                await Task.Run(() =>
                {
                    using (var writer = new StreamWriter(csvFilePath, true))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecord(student);
                        csv.NextRecord();
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding student to CSV file: {ex.Message}");
            }
        }

        public async Task UpdateStudentAsync(Student student)
        {
            try
            {
                if (File.Exists(csvFilePath))
                {
                    var students = await GetStudentsAsync();
                    var existingStudent = students.FirstOrDefault(s => s.Id == student.Id);

                    if (existingStudent != null)
                    {
                        students.Remove(existingStudent);
                        students.Add(student);

                        using (var writer = new StreamWriter(csvFilePath, false))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv.WriteRecords(students);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating student in CSV file: {ex.Message}");
            }
        }

        public async Task DeleteStudentAsync(Student student)
        {
            try
            {
                if (File.Exists(csvFilePath))
                {
                    var students = await GetStudentsAsync();
                    var existingStudent = students.FirstOrDefault(s => s.Id == student.Id);

                    if (existingStudent != null)
                    {
                        students.Remove(existingStudent);

                        using (var writer = new StreamWriter(csvFilePath, false))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv.WriteRecords(students);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting student from CSV file: {ex.Message}");
            }
        }
    }
}
