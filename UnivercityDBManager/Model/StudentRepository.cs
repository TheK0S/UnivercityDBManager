using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Dapper;

namespace UnivercityDBManager.Model
{
    internal class StudentRepository
    {
        public static string connectionString = @"Data Source=DESKTOP-K60TA32\SQLEXPRESS; Initial Catalog=UniversityDB;Integrated Security=True; Encrypt=False";

        public static async Task AddStudent(string firstName, string lastName, string age)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    await db.ExecuteAsync($"INSERT INTO Students (FirstName, LastName, Age) VALUES ('{firstName}', '{lastName}', {age})");
                }                
                MessageBox.Show("Студент добавлен");
                MainWindow.dataPage.StudentsDataGridUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Студент не добавлен", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task RemoveStudent(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    await db.ExecuteAsync($"DELETE FROM Students WHERE Id = {id}");
                }
                MessageBox.Show("Студент удален");
                MainWindow.dataPage.StudentsDataGridUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Студент не удален", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task UpdateStudent(int id, string firstName, string lastName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    await db.ExecuteAsync($"UPDATE Students SET FirstName = '{firstName}', LastName = '{lastName}' WHERE Id = {id}");
                }
                MessageBox.Show("Данные студента изменены");
                MainWindow.dataPage.StudentsDataGridUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при изменении данных студента", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static List<Students> GetAllStudents()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    string sqlCommand = @"SELECT * FROM Students";
                    return db.Query<Students>(sqlCommand).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при загрузке студентов", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Students>();
            }
        }
    }
}
