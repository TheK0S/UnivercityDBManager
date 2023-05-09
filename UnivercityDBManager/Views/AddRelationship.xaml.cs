using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UnivercityDBManager.Model;


namespace UnivercityDBManager.Views
{
    /// <summary>
    /// Логика взаимодействия для AddRelationship.xaml
    /// </summary>
    public partial class AddRelationship : Page
    {
        List<Courses> courses;
        List<Students> students;
        List<string> courseNames = new List<string>();
        List<string> studentNames = new List<string>();

        public AddRelationship()
        {
            InitializeComponent();

            courses = CoursesRepository.GetAllCourses();
            students = StudentRepository.GetAllStudents();

            foreach (var course in courses)
            {
                courseNames.Add(course.Name);
            }

            foreach (var student in students)
            {
                studentNames.Add(student.FirstName + " " + student.LastName);
            }

            courseComboBox.ItemsSource = courseNames;
            studentComboBox.ItemsSource = studentNames;
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (courseComboBox.SelectedItem != null && studentComboBox.SelectedItem != null)
            {                
                Courses course = courses.FirstOrDefault(c => c.Name == courseComboBox.SelectedItem.ToString());
                Students student = students.FirstOrDefault(s => s.FirstName + " " + s.LastName == studentComboBox.SelectedItem.ToString());

                if (course != null && student != null)
                    await RelationshipRepository.AddRelationship(course.Name, student.FirstName, student.LastName);
            }                
            else
            {
                MessageBox.Show("Выбраны не все поля", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }                
        }
    }
}
