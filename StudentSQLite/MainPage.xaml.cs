using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentSQLite
{
    public partial class MainPage : ContentPage
    {
        //Database service to perform CRUD operations
        private DatabaseService _databaseService;

        //CSV Database service to perform CRUD operations
        private DatabaseServiceCSV _databaseServiceCSV;

        private List<Student> _students;

        public MainPage()
        {
            InitializeComponent();

            //Initialize the database service
            _databaseService = new DatabaseService();

            //CSV Initialize
            _databaseServiceCSV = new DatabaseServiceCSV();

            //Load Students
            LoadStudentsAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load students from the database every time the page appears
            LoadStudentsAsync();
        }

        private async void UpdateStudent_Clicked(object sender, EventArgs e)
        {
            var selectedStudent = (Student)((Button)sender).BindingContext;
            //Added CSV -> sends both database services 
            await Navigation.PushAsync(new UpdateStudent(selectedStudent, _databaseService, _databaseServiceCSV));
        }

        private async void DeleteStudent_Clicked(object sender, EventArgs e)
        {
            var selectedStudent = (Student)((Button)sender).BindingContext;
            bool result = await DisplayAlert("Delete Student", "Are you sure you want to delete this student?", "Yes", "No");

            if (result)
            {
                //SQLiteVersion
                //await _databaseService.DeleteStudentAsync(selectedStudent);

                //CSV Version
                await _databaseServiceCSV.DeleteStudentAsync(selectedStudent);

                //await DisplayAlert("Delete Student", "You Deleted a student", "Ok");
                // Reload the students list after deletion
                LoadStudentsAsync();
            }
        }

        private async void ViewDetails_Clicked(object sender, EventArgs e)
        {
            var selectedStudent = (Student)((Button)sender).BindingContext;
            await Navigation.PushAsync(new StudentDetails(selectedStudent));
        }

        private async void LoadStudentsAsync()
        {
            try
            {
                //SQLite Version
                //_students = await _databaseService.GetStudentsAsync();
                
                //await DisplayAlert("Loading Students", "Loading Check", "Ok");
                //CSV Version
                _students = await _databaseServiceCSV.GetStudentsAsync();

                StudentListView.ItemsSource = _students;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

        // Event handler for adding a new student
        //Visual bug, Consider moving to new Add Student page.
        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            var newStudent = new Student
            {
                GivenName = GivenNameEntry.Text,
                FamilyName = FamilyNameEntry.Text,
                StudentNumber = StudentNumberEntry.Text,
                EnrollmentDate = EnrollmentDatePicker.Date
            };

            //SQLite Version
            //await _databaseService.AddStudentAsync(newStudent);

            //CSV Version
            await _databaseServiceCSV.AddStudentAsync(newStudent);

            //await DisplayAlert("Add Student","You Added a student","Ok");

            GivenNameEntry.Text = FamilyNameEntry.Text = StudentNumberEntry.Text = string.Empty;
            LoadStudentsAsync();
        }
    }
}

#region Old
//namespace StudentSQLite
//{
//    public partial class MainPage : ContentPage
//    {
//        // Database service to perform CRUD operations
//        private DatabaseService _databaseService;

//        // List to store students
//        private List<Student> _students;

//        public MainPage()
//        {
//            InitializeComponent();

//            try
//            {
//                // Initialize the database service
//                _databaseService = new DatabaseService();

//                // Load students from the database when the page is created
//                LoadStudentsAsync();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Exception: {ex}");
//            }
//        }

//        // Event handler for adding a new student
//        private async void AddStudent_Clicked(object sender, EventArgs e)
//        {
//            // Create a new student object with the input values
//            var newStudent = new Student
//            {
//                GivenName = GivenNameEntry.Text,
//                FamilyName = FamilyNameEntry.Text,
//                StudentNumber = StudentNumberEntry.Text,
//                EnrollmentDate = EnrollmentDatePicker.Date
//            };

//            // Add the student to the database
//            await _databaseService.AddStudentAsync(newStudent);

//            // Clear the input fields
//            GivenNameEntry.Text = FamilyNameEntry.Text = StudentNumberEntry.Text = string.Empty;

//            // Reload the students list
//            LoadStudentsAsync();
//        }

//        // Event handler for updating an existing student
//        private async void UpdateStudent_Clicked(object sender, EventArgs e)
//        {
//            // Check if a student is selected in the ListView
//            if (StudentListView.SelectedItem != null)
//            {
//                // Retrieve the selected student
//                var selectedStudent = (Student)StudentListView.SelectedItem;

//                // Update the selected student's information
//                selectedStudent.GivenName = GivenNameEntry.Text;
//                selectedStudent.FamilyName = FamilyNameEntry.Text;
//                selectedStudent.StudentNumber = StudentNumberEntry.Text;
//                selectedStudent.EnrollmentDate = EnrollmentDatePicker.Date;

//                // Update the student in the database
//                await _databaseService.UpdateStudentAsync(selectedStudent);

//                // Clear the input fields and reset the ListView selection
//                GivenNameEntry.Text = FamilyNameEntry.Text = StudentNumberEntry.Text = string.Empty;
//                StudentListView.SelectedItem = null;

//                // Reload the students list
//                LoadStudentsAsync();
//            }
//        }

//        // Event handler for deleting an existing student
//        private async void DeleteStudent_Clicked(object sender, EventArgs e)
//        {
//            // Check if a student is selected in the ListView
//            if (StudentListView.SelectedItem != null)
//            {
//                // Retrieve the selected student
//                var selectedStudent = (Student)StudentListView.SelectedItem;

//                // Delete the selected student from the database
//                await _databaseService.DeleteStudentAsync(selectedStudent);

//                // Clear the input fields and reset the ListView selection
//                GivenNameEntry.Text = FamilyNameEntry.Text = StudentNumberEntry.Text = string.Empty;
//                StudentListView.SelectedItem = null;

//                // Reload the students list
//                LoadStudentsAsync();
//            }
//        }

//        // Load the students from the database and update the ListView
//        private async void LoadStudentsAsync()
//        {
//            // Retrieve the list of students from the database
//            _students = await _databaseService.GetStudentsAsync();

//            // Bind the list of students to the ListView
//            StudentListView.ItemsSource = _students;
//        }
//    }
//}
//private void OnFrameTapped(object sender, EventArgs e)
//{
//    // Handle the tapped event here
//    // You can add your logic or call a method
//    // e.g., YourClickEventHandler(sender, e);
//    if (sender is Frame frame && frame.GestureRecognizers is Student student)
//    {
//        DisplayAlert("Title", "We did it", "OK");
//    }
//}

//    < !--< StackLayout Padding = "20" >
//    < Label Text = "Student Management" FontSize = "24" HorizontalOptions = "CenterAndExpand" Margin = "0, 20" />

//    -->
//< !--Entry Fields for Student Information -->
//<!--
//    <Entry Placeholder="Given Name" x:Name = "GivenNameEntry" />
//    < Entry Placeholder = "Family Name" x: Name = "FamilyNameEntry" />
//    < Entry Placeholder = "Student Number" x: Name = "StudentNumberEntry" />
//    < DatePicker x: Name = "EnrollmentDatePicker" Date = "{Binding EnrollmentDate}" />

//    -->
//< !--Buttons for CRUD Operations -->
//< !--
//    < StackLayout Orientation = "Horizontal" HorizontalOptions = "CenterAndExpand" >
//        < Button Text = "Add Student" Clicked = "AddStudent_Clicked" />
//        < Button Text = "Update Student" Clicked = "UpdateStudent_Clicked" />
//        < Button Text = "Delete Student" Clicked = "DeleteStudent_Clicked" />
//    </ StackLayout >

//    -->
//< !--Student List View-- >
//< !--
//    < ListView x:Name = "StudentListView" VerticalOptions = "FillAndExpand" >
//        < ListView.ItemTemplate >
//            < DataTemplate >
//                < TextCell Text = "{Binding GivenName}" Detail = "{Binding FamilyName}" />
//            </ DataTemplate >
//        </ ListView.ItemTemplate >
//    </ ListView >


//    < Grid >
//        < Grid.GestureRecognizers >
//            < TapGestureRecognizer Tapped = "OnFrameTapped" />
//        </ Grid.GestureRecognizers >


//            < Frame BackgroundColor = "Transparent" BorderColor = "DarkSlateGrey" Padding = "10" WidthRequest = "300" HeightRequest = "55" >
//                < StackLayout >
//                    < Label Text = "HellO" MinimumWidthRequest = "500" />
//                    < Label Text = "World" />
//                </ StackLayout >
//            </ Frame >

//    </ Grid >
//</ StackLayout > -->
#endregion