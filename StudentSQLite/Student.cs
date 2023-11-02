// Student.cs
using SQLite;

public class Student
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string StudentNumber { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
