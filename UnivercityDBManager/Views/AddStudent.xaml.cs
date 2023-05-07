using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddStudent.xaml
    /// </summary>
    public partial class AddStudent : Page
    {
        public AddStudent()
        {
            InitializeComponent();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if(firstNameField.Text?.Length > 0 && lastNameField.Text?.Length > 0 && ageField.Text?.Length > 0)
                await StudentRepository.AddStudent(firstNameField.Text, lastNameField.Text, ageField.Text);
            else
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ageField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(ageField.Text, @"^[0-9]+$"))
            {
                ageField.Text = "";
                MessageBox.Show("Поле принимает только цифры", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
