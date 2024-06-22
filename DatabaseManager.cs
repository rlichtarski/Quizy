using System.Security.Cryptography;
using System.Text;
using Npgsql;

namespace quizy
{
    public static class DatabaseManager
    {
        private static string connectionString = "Host=localhost;Username=postgres;Password=root;Database=quizzes_db";

        public static void CreateTable() {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var createUsersTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS users (id SERIAL PRIMARY KEY,email VARCHAR(255) UNIQUE NOT NULL,password_hash VARCHAR(255) NOT NULL);", conn);
                createUsersTable.ExecuteNonQuery();
                var createQuizResultsTable = new NpgsqlCommand("CREATE TABLE IF NOT EXISTS quiz_results (id SERIAL PRIMARY KEY,correct_answers INTEGER,wrong_answers INTEGER,quiz_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP,user_id INTEGER REFERENCES users(id));", conn);
                createQuizResultsTable.ExecuteNonQuery();
            }
        }

        public static bool RegisterUser(string email, string password)
        {
            var passwordHash = HashPassword(password);
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand("INSERT INTO users (email, password_hash) VALUES (@Email, @PasswordHash)", conn);
                cmd.Parameters.AddWithValue("Email", email);
                cmd.Parameters.AddWithValue("PasswordHash", passwordHash);
                try
                {
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (PostgresException)
                {
                    return false; // User already exists
                }
            }
        }

        public static int LoginUser(string email, string password)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand("SELECT id, password_hash FROM users WHERE email = @Email", conn);
                cmd.Parameters.AddWithValue("Email", email);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var storedHash = reader.GetString(1);
                        if (VerifyPassword(password, storedHash))
                        {
                            return reader.GetInt32(0); // Return user ID
                        }
                    }
                }
            }
            return -1; // Invalid login
        }

        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var builder = new StringBuilder();
                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static bool VerifyPassword(string password, string storedHash)
        {
            var hash = HashPassword(password);
            return hash == storedHash;
        }

        public static void SaveQuizResult(int userId, int correctAnswers, int wrongAnswers)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                var cmd = new NpgsqlCommand("INSERT INTO quiz_results (user_id, correct_answers, wrong_answers) VALUES (@userId, @CorrectAnswers, @WrongAnswers)", conn);
                cmd.Parameters.AddWithValue("UserId", userId);
                cmd.Parameters.AddWithValue("CorrectAnswers", correctAnswers);
                cmd.Parameters.AddWithValue("WrongAnswers", wrongAnswers);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
