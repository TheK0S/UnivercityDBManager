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
    internal class CoursesRepository
    {
        public static string connectionString = @"Data Source=DESKTOP-K60TA32\SQLEXPRESS; Initial Catalog=UniversityDB;Integrated Security=True; Encrypt=False";

        public static async Task AddCourse(string courseName, string teacherName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    await db.ExecuteAsync($"INSERT INTO Courses (Name, TeacherName) VALUES ('{courseName}', '{teacherName}')");
                }
                MessageBox.Show("Курс добавлен");
                MainWindow.dataPage.CoursesDataGridUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Курс не добавлен", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task RemoveCourse(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    await db.ExecuteAsync($"DELETE FROM Courses WHERE Id = {id}");
                }
                MessageBox.Show("Курс удален");
                MainWindow.dataPage.CoursesDataGridUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Курс не удален", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static async Task UpdateCourse(int id, string courseName, string teacherName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    await db.ExecuteAsync($"UPDATE Courses SET Name = '{courseName}', TeacherName = '{teacherName}' WHERE Id = {id}");
                }
                MessageBox.Show("Данные курса изменены");
                MainWindow.dataPage.CoursesDataGridUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при изменении данных курса", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static List<Courses> GetAllCourses()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    string sqlCommand = @"SELECT * FROM Courses";
                    return db.Query<Courses>(sqlCommand).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при загрузке курсов", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Courses>();
            }
        }
    }
}
