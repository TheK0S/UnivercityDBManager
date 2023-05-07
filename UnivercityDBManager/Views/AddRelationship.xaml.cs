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
        public AddRelationship()
        {
            InitializeComponent();
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void add_btn_Click(object sender, RoutedEventArgs e)
        {
            if(courseNameField.Text?.Length > 0 && firstNameField.Text?.Length > 0 && lastNameField.Text?.Length > 0)
                await RelationshipRepository.AddRelationship(courseNameField.Text, firstNameField.Text, lastNameField.Text);
            else
                MessageBox.Show("Не все поля заполнены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
