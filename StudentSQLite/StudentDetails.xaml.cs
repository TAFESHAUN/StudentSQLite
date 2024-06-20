namespace StudentSQLite;

public partial class StudentDetails : ContentPage
{
    public StudentDetails(Student student)
    {
        InitializeComponent();
        //BindingContext = student;
        GivenName.Text = student.GivenName;
        FamilyName.Text = student.FamilyName;
        StudentNumber.Text = student.StudentNumber;
        EnrolmentDate.Text = student.EnrollmentDate.ToString();
    }


}