using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Ado.NET_Homework1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<string> authorsList { get; set; }
        public List<string> categoriesList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;


            readAuthors();
            readCategories();

        }
        public void readCategories()
        {

            string connectionString = @"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                List<string> categories = new List<string>();

                string query = "SELECT * FROM Categories";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    categories.Add(reader[1].ToString());

                }

                categoriesList = new List<string>(categories);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public void readAuthors()
        {

            string connectionString = @"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                List<string> authors = new List<string>();

                string query = "SELECT * FROM Authors";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = sqlCommand.ExecuteReader();

                while (reader.Read())
                {
                    authors.Add(reader[1].ToString());

                }

                authorsList = new List<string>(authors);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        private void SearchBook(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();


                string query = $"SELECT * FROM Books WHERE Name = @name";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);


                sqlCommand.Parameters.AddWithValue("@name", searchTextBox.Text);

                SqlDataReader reader = sqlCommand.ExecuteReader();



                int count = 0;
                while (reader.Read())
                {
                    

                    MessageBox.Show($"ID: {reader[0]}\nName: {reader[1]}\nPages: {reader["Pages"]}\nQuantity: {reader["Quantity"]}\nComment: {reader["Comment"]}", "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                    count++;
                }

                if (count == 0)
                    MessageBox.Show("Bu ada sahib kitab kitabxanada movcud deyildir", "INFROMATION", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        private void UpdateBook(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();


                string query = $"UPDATE Books SET Name = @newName WHERE Name = @oldName";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);


                sqlCommand.Parameters.AddWithValue("@oldName", nameTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@newName", updateTextBox.Text);

                int num = sqlCommand.ExecuteNonQuery();

                if (num > 0)
                {
                    MessageBox.Show("Kitabin kohne adi yenisiyle evez olundu", "INFROMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Bu ada sahib kitab kitabxanada movcud deyildir", "INFROMATION", MessageBoxButton.OK, MessageBoxImage.Information);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        private void AddBook(object sender, RoutedEventArgs e)
        {

            string connectionString = @"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();
                string query = "INSERT INTO Books(Id,Name,Quantity,Pages,YearPress,Id_Themes,Id_Press,Id_Category,Id_Author,Comment) VALUES(@id_,@name_,@quantity_,@pages_,@yearPress_,@idT,@idP,@idC,@idA,@comment_)";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@id_", idTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@name_", nameTextBox2.Text);
                sqlCommand.Parameters.AddWithValue("@quantity_", quantityTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@pages_", pagesTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@yearPress_", yearPressTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@idT", idThemesTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@idP", idPressTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@idC", idCategoryTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@idA", idAuthorTextBox.Text);
                sqlCommand.Parameters.AddWithValue("@comment_", commentTextBox.Text);



                int num = sqlCommand.ExecuteNonQuery();

                if (num > 0)
                {
                    MessageBox.Show("Yeni kitab kitabxanaya elave olundu", "INFROMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Yeni kitab kitabxanaya elave oluna bilmedi hansisa deyerde sehv buraxmisiniz", "INFROMATION", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        private void DeleteBook(object sender, RoutedEventArgs e)
        {
            string connectionString = @"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                sqlConnection.Open();

                string query = $"DELETE FROM Books WHERE Name = @nameBook";

                SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
                sqlCommand.Parameters.AddWithValue("@nameBook", deleteTextBox.Text);

                int num = sqlCommand.ExecuteNonQuery();

                if (num > 0)
                {
                    MessageBox.Show("Kitab kitabxanadan silindi", "INFROMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Bu ada sahib kitab kitabxanada movcud deyildir", "INFROMATION", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
