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
    /// Логика взаимодействия для DataPage.xaml
    /// </summary>
    public partial class DataPage : Page
    {
        public DataPage()
        {
            InitializeComponent();

            studentsDataGrid.CanUserAddRows = false;
            coursesDataGrid.CanUserAddRows = false;
            relationshipDataGrid.CanUserAddRows = false;

            studentsDataGrid.ItemsSource = StudentRepository.GetAllStudents();
            coursesDataGrid.ItemsSource = CoursesRepository.GetAllCourses();
            relationshipDataGrid.ItemsSource = RelationshipRepository.GetAllRelationships();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if (studentsTabItem.IsSelected)
            {
                NavigationService.Navigate(new AddStudent());
            }
            else if (coursesTabItem.IsSelected)
            {
                NavigationService.Navigate(new AddCource());
            }
            else if (relationshipsTabItem.IsSelected)
            {
                NavigationService.Navigate(new AddRelationship());
            }
        }

        private async void update_btn_Click(object sender, RoutedEventArgs e)
        {
            if(studentsTabItem.IsSelected)
            {
                Students student = (Students)studentsDataGrid.SelectedItem;
                if (student != null)
                    await StudentRepository.UpdateStudent(student.Id, student.FirstName, student.LastName);
                else
                    MessageBox.Show("Не выбран элемент для изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(coursesTabItem.IsSelected)
            {
                Courses cource = (Courses)coursesDataGrid.SelectedItem;
                if (cource != null)
                    await CoursesRepository.UpdateCourse(cource.Id, cource.Name, cource.TeacherName);
                else
                    MessageBox.Show("Не выбран элемент для изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(relationshipsTabItem.IsSelected)
            {
                Relationship rel = (Relationship)relationshipDataGrid.SelectedItem;
                if (rel != null)
                    await RelationshipRepository.UpdateRelationship(rel.Id,rel.CourseName, rel.StudentFirstName, rel.StudentLastName);
                else
                    MessageBox.Show("Не выбран элемент для изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if (studentsTabItem.IsSelected)
            {
                Students student = (Students)studentsDataGrid.SelectedItem;
                if (student != null)
                    await StudentRepository.RemoveStudent(student.Id);
                else
                    MessageBox.Show("Не выбран элемент для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (coursesTabItem.IsSelected)
            {
                Courses cource = (Courses)coursesDataGrid.SelectedItem;
                if (cource != null)
                    await CoursesRepository.RemoveCourse(cource.Id);
                else
                    MessageBox.Show("Не выбран элемент для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (relationshipsTabItem.IsSelected)
            {
                Relationship rel = (Relationship)relationshipDataGrid.SelectedItem;
                if (rel != null)
                    await RelationshipRepository.RemoveRelationship(rel.Id);
                else
                    MessageBox.Show("Не выбран элемент для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }               

        
        private void listUpdate_btn_Click(object sender, RoutedEventArgs e)
        {
            if (studentsTabItem.IsSelected)
            {
                StudentsDataGridUpdate();
            }
            else if (coursesTabItem.IsSelected)
            {
                CoursesDataGridUpdate();
            }
            else if (relationshipsTabItem.IsSelected)
            {
                RelationshipDataGridUpdate();
            }
        }

        public void StudentsDataGridUpdate()
        {
            studentsDataGrid.ItemsSource = StudentRepository.GetAllStudents();
        }

        public void CoursesDataGridUpdate()
        {
            coursesDataGrid.ItemsSource = CoursesRepository.GetAllCourses();
        }

        public void RelationshipDataGridUpdate()
        {
            relationshipDataGrid.ItemsSource = RelationshipRepository.GetAllRelationships();
        }
    }
}
