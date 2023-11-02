using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentSQLite
{
    public partial class MainPage : ContentPage
    {
        // Database service to perform CRUD operations
        private DatabaseService _databaseService;

        // List to store students
        private List<Student> _students;

        public MainPage()
        {
            InitializeComponent();

            // Initialize the database service
            _databaseService = new DatabaseService();

            // Load students from the database when the page is created
            LoadStudentsAsync();
        }

        // Event handler for adding a new student
        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            // Create a new student object with the input values
            var newStudent = new Student
            {
                GivenName = GivenNameEntry.Text,
                FamilyName = FamilyNameEntry.Text,
                StudentNumber = StudentNumberEntry.Text,
                EnrollmentDate = EnrollmentDatePicker.Date
            };

            // Add the student to the database
            await _databaseService.AddStudentAsync(newStudent);

            // Clear the input fields
            GivenNameEntry.Text = FamilyNameEntry.Text = StudentNumberEntry.Text = string.Empty;

            // Reload the students list
            LoadStudentsAsync();
        }

        // Event handler for updating an existing student
        private async void UpdateStudent_Clicked(object sender, EventArgs e)
        {
            // Check if a student is selected in the ListView
            if (StudentListView.SelectedItem != null)
            {
                // Retrieve the selected student
                var selectedStudent = (Student)StudentListView.SelectedItem;

                // Update the selected student's information
                selectedStudent.GivenName = GivenNameEntry.Text;
                selectedStudent.FamilyName = FamilyNameEntry.Text;
                selectedStudent.StudentNumber = StudentNumberEntry.Text;
                selectedStudent.EnrollmentDate = EnrollmentDatePicker.Date;

                // Update the student in the database
                await _databaseService.UpdateStudentAsync(selectedStudent);

                // Clear the input fields and reset the ListView selection
                GivenNameEntry.Text = FamilyNameEntry.Text = StudentNumberEntry.Text = string.Empty;
                StudentListView.SelectedItem = null;

                // Reload the students list
                LoadStudentsAsync();
            }
        }

        // Event handler for deleting an existing student
        private async void DeleteStudent_Clicked(object sender, EventArgs e)
        {
            // Check if a student is selected in the ListView
            if (StudentListView.SelectedItem != null)
            {
                // Retrieve the selected student
                var selectedStudent = (Student)StudentListView.SelectedItem;

                // Delete the selected student from the database
                await _databaseService.DeleteStudentAsync(selectedStudent);

                // Clear the input fields and reset the ListView selection
                GivenNameEntry.Text = FamilyNameEntry.Text = StudentNumberEntry.Text = string.Empty;
                StudentListView.SelectedItem = null;

                // Reload the students list
                LoadStudentsAsync();
            }
        }

        // Load the students from the database and update the ListView
        private async void LoadStudentsAsync()
        {
            // Retrieve the list of students from the database
            _students = await _databaseService.GetStudentsAsync();

            // Bind the list of students to the ListView
            StudentListView.ItemsSource = _students;
        }
    }
}
