namespace StudentSQLite;

public partial class UpdateStudent : ContentPage
{
    private Student _selectedStudent;
    private DatabaseService _databaseService;
    private DatabaseServiceCSV _databaseServiceCSV;

    public UpdateStudent(Student selectedStudent, DatabaseService databaseService, DatabaseServiceCSV databaseServiceCSV) //DatabaseService databaseService
    {
        InitializeComponent();
        //Pass student
        _selectedStudent = selectedStudent;

        //Pass database servicer
        //SQLite Passed
        _databaseService = databaseService;

        // Populate the input fields with the existing student details
        GivenNameEntry.Text = _selectedStudent.GivenName;
        FamilyNameEntry.Text = _selectedStudent.FamilyName;
        StudentNumberEntry.Text = _selectedStudent.StudentNumber;
        EnrollmentDatePicker.Date = _selectedStudent.EnrollmentDate;

        //CSV Version
        //CSV Passed
        _databaseServiceCSV = databaseServiceCSV;
    }

    private async void Update_Clicked(object sender, EventArgs e)
    {
        // Update the selected student's information
        _selectedStudent.GivenName = GivenNameEntry.Text;
        _selectedStudent.FamilyName = FamilyNameEntry.Text;
        _selectedStudent.StudentNumber = StudentNumberEntry.Text;
        _selectedStudent.EnrollmentDate = EnrollmentDatePicker.Date;

        //SQLite Version
        // Call the database service to update the student
        await _databaseService.UpdateStudentAsync(_selectedStudent);

        //CSV Version
        //await _databaseServiceCSV.UpdateStudentAsync(_selectedStudent);
        //await DisplayAlert("Update Student", "You Updated a student", "Ok");

        // Navigate back to the previous page
        await Navigation.PopAsync();
    }
}