using SavarankiskasDarbas2.Core.Models;
using SavarankiskasDarbas2.Core.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavarankiskasDarbas2.Core.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void RegisterUser(User user)
        {
            _userRepository.AddUser(user);
        }

        public User GetUser(int id)
        {
            return _userRepository.GetUserById(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public void RemoveUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public void UpdatePassword(int id, string newPassword)
        {
            _userRepository.ChangePassword(id, newPassword);
        }

        public void ActivateUser(int id)
        {
            _userRepository.SetUserStatus(id, true);
        }

        public void DeactivateUser(int id)
        {
            _userRepository.SetUserStatus(id, false);
        }

        public List<User> ListUsersByRole(string role)
        {
            return _userRepository.GetUsersByRole(role);
        }
    }
}