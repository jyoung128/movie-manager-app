using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace JRYMovieManagerApplication
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.Title = "Movie Manager";
            about.Info = "This application was built as a proof of concept for using ADO.net to interface with an SQL Server database and perform all of the basic CRUD operations (create, read, update, and delete). The user is able to add, remove, and update information about any films of their choosing to the database.";
            about.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string connectionString = GetConnectionString();
            List<Movie> movies = new List<Movie>();
            string sqlCommand = "SELECT Id, Title, Year, Director, Genre, RottenTomatoesScore, TotalEarned FROM Movies ORDER BY Title";
            string[] genres = { "","Animation", "Action", "Comedy", "Drama", "Horror", "Mystery", "Romance", "Science Fiction", "Western" };

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Add each movie from the DB to the movies list
                                var movie = new Movie();
                                movie.Title = reader.GetString(1);
                                movie.Year = reader.GetInt32(2);
                                movie.Director = reader.GetString(3);
                                int genreNumber = reader.GetInt32(4);
                                movie.Genre = genres[genreNumber];

                                //Check for null values when allowed by the DB
                                if (!reader.IsDBNull(5))
                                {
                                    movie.RottenTomatoesScore = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    movie.TotalEarned = reader.GetDecimal(6);
                                }

                                movies.Add(movie);
                            }
                        }
                       
                        connection.Close();

                        dgvMovies.DataSource = movies;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection failed. Error {ex.Message}");
            
            }
        }

        private static string GetConnectionString()
        {
            string server = "coursemaster1.csbchotp6tva.us-east-2.rds.amazonaws.com";
            string database = "CSCI1630";
            string username = "rw1630";
            string password = "Project!";
            string port = "1433";

            return $"Data Source={server},{port};Initial Catalog={database};User ID={username};Password={password};";
        }

        private void mnuRefresh_Click(object sender, EventArgs e)
        {
            string connectionString = GetConnectionString();
            List<Movie> movies = new List<Movie>();
            string sqlCommand = "SELECT Id, Title, Year, Director, Genre, RottenTomatoesScore, TotalEarned FROM Movies ORDER BY Title";
            string[] genres = { "", "Animation", "Action", "Comedy", "Drama", "Horror", "Mystery", "Romance", "Science Fiction", "Western" };

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //Add each movie from the DB to the movies list
                                var movie = new Movie();
                                movie.Title = reader.GetString(1);
                                movie.Year = reader.GetInt32(2);
                                movie.Director = reader.GetString(3);
                                int genreNumber = reader.GetInt32(4);
                                movie.Genre = genres[genreNumber];

                                //Check for null values when allowed by the DB
                                if (!reader.IsDBNull(5))
                                {
                                    movie.RottenTomatoesScore = reader.GetInt32(5);
                                }

                                if (!reader.IsDBNull(6))
                                {
                                    movie.TotalEarned = reader.GetDecimal(6);
                                }

                                movies.Add(movie);
                            }
                        }

                        connection.Close();

                        dgvMovies.DataSource = movies;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database connection failed. Error {ex.Message}");

            }
        }
    }
}

