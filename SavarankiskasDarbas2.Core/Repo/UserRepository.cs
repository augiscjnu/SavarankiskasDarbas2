using Dapper;
using Microsoft.Data.SqlClient;
using SavarankiskasDarbas2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavarankiskasDarbas2.Core.Repo
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AddUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Users (Username, Password, IsActive, Role) VALUES (@Username, @Password, @IsActive, @Role)";
                connection.Execute(query, user);
            }
        }

        public User GetUserById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Users WHERE Id = @Id";
                return connection.QuerySingleOrDefault<User>(query, new { Id = id });
            }
        }

        public List<User> GetAllUsers()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Users";
                return connection.Query<User>(query).ToList();
            }
        }

        public void UpdateUser(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Users SET Username = @Username, Password = @Password, IsActive = @IsActive WHERE Id = @Id";
                connection.Execute(query, user);
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM Users WHERE Id = @Id";
                connection.Execute(query, new { Id = id });
            }
        }

        public void ChangePassword(int id, string newPassword)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Users SET Password = @Password WHERE Id = @Id";
                connection.Execute(query, new { Id = id, Password = newPassword });
            }
        }

        public void SetUserStatus(int id, bool isActive)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE Users SET IsActive = @IsActive WHERE Id = @Id";
                connection.Execute(query, new { Id = id, IsActive = isActive });
            }
        }

        public List<User> GetUsersByRole(string role)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Users WHERE Role = @Role";
                return connection.Query<User>(query, new { Role = role }).ToList();
            }
        }
    }
}
