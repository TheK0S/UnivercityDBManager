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
    internal class RelationshipRepository
    {
        public static string connectionString = @"Data Source=DESKTOP-K60TA32\SQLEXPRESS; Initial Catalog=UniversityDB;Integrated Security=True; Encrypt=False";

        public static async Task AddRelationship(string courseName, string studentFirstName, string studentLastName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    await db.ExecuteAsync($"INSERT INTO Relationship (CourseName, StudentFirstName, StudentLastName) VALUES ('{courseName}', '{studentFirstName}', '{studentLastName}')");
                }
                MessageBox.Show("Привязка добавлена");
                MainWindow.dataPage.RelationshipDataGridUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Привязка не добавлена", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public static async Task RemoveRelationship(int id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    await db.ExecuteAsync($"DELETE FROM Relationship WHERE Id = {id}");
                }
                MessageBox.Show("Привязка удалена");
                MainWindow.dataPage.RelationshipDataGridUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Привязка не удалена", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public static async Task UpdateRelationship(int id, string courseName, string studentFirstName, string studentLastName)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    await db.ExecuteAsync($"UPDATE Relationship SET Name = '{courseName}', StudentFirstName = '{studentFirstName}', StudentLastName = '{studentLastName}' WHERE Id = {id}");
                }
                MessageBox.Show("Данные привязки изменены");
                MainWindow.dataPage.RelationshipDataGridUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при изменении данных привязки", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public static List<Relationship> GetAllRelationships()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    string sqlCommand = @"SELECT * FROM Relationship";
                    return db.Query<Relationship>(sqlCommand).ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка при загрузке привязок", MessageBoxButton.OK, MessageBoxImage.Error);
                return new List<Relationship>();
            }
        }
    }
}
