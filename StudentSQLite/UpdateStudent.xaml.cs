namespace StudentSQLite;

public partial class UpdateStudent : ContentPage
{
    private Student _selectedStudent;
    private DatabaseService _databaseService;

    public UpdateStudent(Student selectedStudent, DatabaseService databaseService)
    {
        InitializeComponent();
        //Pass student
        _selectedStudent = selectedStudent;

        //Pass database servicer
        _databaseService = databaseService;

        // Populate the input fields with the existing student details
        GivenNameEntry.Text = _selectedStudent.GivenName;
        FamilyNameEntry.Text = _selectedStudent.FamilyName;
        StudentNumberEntry.Text = _selectedStudent.StudentNumber;
        EnrollmentDatePicker.Date = _selectedStudent.EnrollmentDate;
    }

    private async void Update_Clicked(object sender, EventArgs e)
    {
        // Update the selected student's information
        _selectedStudent.GivenName = GivenNameEntry.Text;
        _selectedStudent.FamilyName = FamilyNameEntry.Text;
        _selectedStudent.StudentNumber = StudentNumberEntry.Text;
        _selectedStudent.EnrollmentDate = EnrollmentDatePicker.Date;

        // Call the database service to update the student
        await _databaseService.UpdateStudentAsync(_selectedStudent);

        // Navigate back to the previous page
        await Navigation.PopAsync();
    }
}