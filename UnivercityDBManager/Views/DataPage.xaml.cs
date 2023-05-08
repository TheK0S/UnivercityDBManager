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
using System.Windows.Media.Animation;
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
            if (studentsTabItem.IsSelected)
            {
                if(studentsDataGrid.ItemsSource != null && searchField.Text != null)
                {
                    Students student = studentsDataGrid.Items.Cast<Students>().FirstOrDefault(
                        st => st.FirstName.ToLower().Contains(searchField.Text.ToLower())
                        || st.LastName.ToLower().Contains(searchField.Text.ToLower()));

                    if (student != null)
                    {
                        studentsDataGrid.ScrollIntoView(student);
                        studentsDataGrid.SelectedItem = student;
                    }
                    else
                        MessageBox.Show($"Студент содержащий \"{searchField.Text}\" в имени или фамилии не найден");
                }                
            }
            else if (coursesTabItem.IsSelected)
            {
                if(coursesDataGrid.ItemsSource != null && searchField.Text != null)
                {
                    Courses course = coursesDataGrid.Items.Cast<Courses>().FirstOrDefault(
                        c => c.Name.ToLower().Contains(searchField.Text.ToLower()) 
                        || c.TeacherName.ToLower().Contains(searchField.Text.ToLower()));

                    if(course != null)
                    {
                        coursesDataGrid.ScrollIntoView(course);
                        coursesDataGrid.SelectedItem = course;
                    }
                    else
                        MessageBox.Show($"Курс или преподаватель содержащий \"{searchField.Text}\" не найден");
                }
            }
            else if (relationshipsTabItem.IsSelected)
            {
                if(relationshipDataGrid.ItemsSource == null && searchField.Text != null)
                {
                    Relationship rel = relationshipDataGrid.Items.Cast<Relationship>().FirstOrDefault(
                        r => r.CourseName.ToLower().Contains(searchField.Text.ToLower())
                        || r.StudentFirstName.ToLower().Contains(searchField.Text.ToLower())
                        || r.StudentLastName.ToLower().Contains(searchField.Text.ToLower()));

                    if(rel != null)
                    {
                        relationshipDataGrid.ScrollIntoView(rel);
                        relationshipDataGrid.SelectedItem = rel;
                    }                        
                    else
                        MessageBox.Show($"Фамилия или имя студента, название курса содержащие \"{searchField.Text}\" не найдены");
                }
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.GoBack();
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

        private void filterFieldStudents_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(filterFieldStudents.Text != null)
            {
                if(firstNameFilterStudents.IsChecked == true)
                    studentsDataGrid.ItemsSource = from Students student in studentsDataGrid.ItemsSource
                                               where student.FirstName.ToLower().Contains(filterFieldStudents.Text.ToLower())
                                               select student;

                else if(lastNameFilterStudents.IsChecked == true)
                    studentsDataGrid.ItemsSource = from Students student in studentsDataGrid.ItemsSource
                                                   where student.LastName.ToLower().Contains(filterFieldStudents.Text.ToLower())
                                                   select student;

                else if(ageFilterStudents.IsChecked == true)
                    studentsDataGrid.ItemsSource = from Students student in studentsDataGrid.ItemsSource
                                                   where student.Age.ToString().Contains(filterFieldStudents.Text)
                                                   select student;
            }            
            else
            {
                studentsDataGrid.ItemsSource = StudentRepository.GetAllStudents();
            }
        }

        private void isHowFilter_Checked(object sender, RoutedEventArgs e)
        {
            studentFilterField.BeginAnimation(StackPanel.HeightProperty, new DoubleAnimation(0, 30, TimeSpan.FromSeconds(0.5)));
            coursesFilterField.BeginAnimation(StackPanel.HeightProperty, new DoubleAnimation(0, 30, TimeSpan.FromSeconds(0.5)));
            relationshipFilterField.BeginAnimation(StackPanel.HeightProperty, new DoubleAnimation(0, 30, TimeSpan.FromSeconds(0.5)));
        }

        private void isHowFilter_Unchecked(object sender, RoutedEventArgs e)
        {
            studentFilterField.BeginAnimation(StackPanel.HeightProperty, new DoubleAnimation(30, 0, TimeSpan.FromSeconds(0.5)));
            coursesFilterField.BeginAnimation(StackPanel.HeightProperty, new DoubleAnimation(30, 0, TimeSpan.FromSeconds(0.5)));
            relationshipFilterField.BeginAnimation(StackPanel.HeightProperty, new DoubleAnimation(30, 0, TimeSpan.FromSeconds(0.5)));
            filterFieldStudents.Text = null;
            filterFieldCourses.Text = null;
            filterFieldRelationship.Text = null;
        }

        private void filterFieldCourses_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (filterFieldCourses.Text != null)
            {
                if (courceNameFilterCourses.IsChecked == true)
                    coursesDataGrid.ItemsSource = from Courses cource in coursesDataGrid.ItemsSource
                                                   where cource.Name.ToLower().Contains(filterFieldCourses.Text.ToLower())
                                                   select cource;

                else if (teacherFilterCourses.IsChecked == true)
                    coursesDataGrid.ItemsSource = from Courses cource in coursesDataGrid.ItemsSource
                                                   where cource.TeacherName.ToLower().Contains(filterFieldCourses.Text.ToLower())
                                                   select cource;                
            }
            else
            {
                coursesDataGrid.ItemsSource = CoursesRepository.GetAllCourses();
            }
        }

        private void filterFieldRelationship_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (filterFieldRelationship.Text != null)
            {
                if (courceNameFilterRelationship.IsChecked == true)
                    relationshipDataGrid.ItemsSource = from Relationship rel in relationshipDataGrid.ItemsSource
                                                  where rel.CourseName.ToLower().Contains(filterFieldRelationship.Text.ToLower())
                                                  select rel;

                else if (firstNameFilterRelationship.IsChecked == true)
                    relationshipDataGrid.ItemsSource = from Relationship rel in relationshipDataGrid.ItemsSource
                                                  where rel.StudentFirstName.ToLower().Contains(filterFieldRelationship.Text.ToLower())
                                                  select rel;
                else if (lastNameFilterRelationship.IsChecked == true)
                    relationshipDataGrid.ItemsSource = from Relationship rel in relationshipDataGrid.ItemsSource
                                                  where rel.StudentLastName.ToLower().Contains(filterFieldRelationship.Text.ToLower())
                                                  select rel;
            }
            else
            {
                relationshipDataGrid.ItemsSource = RelationshipRepository.GetAllRelationships();
            }
        }
    }
}
