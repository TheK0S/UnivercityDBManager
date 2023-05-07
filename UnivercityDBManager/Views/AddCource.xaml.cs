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
    /// Логика взаимодействия для AddCource.xaml
    /// </summary>
    public partial class AddCource : Page
    {
        public AddCource()
        {
            InitializeComponent();
        }

        private async void addCourse_btn_Click(object sender, RoutedEventArgs e)
        {
            if(courseNameField.Text?.Length > 0 && teacherNameField.Text?.Length > 0)
                await CoursesRepository.AddCourse(courseNameField.Text, teacherNameField.Text);
            else
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
