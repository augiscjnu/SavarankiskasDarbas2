using SavarankiskasDarbas2.Core.Models;
using SavarankiskasDarbas2.Core.Repo;
using SavarankiskasDarbas2.Core.Services;
using System;
using System.Collections.Generic;


namespace SavarankiskasDarbas2
{

    public class Program
    {
        private static UserService _userService;

        static void Main()
        {
            string connectionString = "Server=localhost;Database=C#mokymai;Trusted_Connection=True;TrustServerCertificate=true;";
            _userService = new UserService(new UserRepository(connectionString));

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("Vartotojų valdymo sistema");
                Console.WriteLine("1. Registruoti vartotoją");
                Console.WriteLine("2. Gauti vartotoją pagal ID");
                Console.WriteLine("3. Atnaujinti vartotojo informaciją");
                Console.WriteLine("4. Pašalinti vartotoją");
                Console.WriteLine("5. Pakeisti slaptažodį");
                Console.WriteLine("6. Aktyvuoti vartotoją");
                Console.WriteLine("7. Deaktyvuoti vartotoją");
                Console.WriteLine("8. Gauti visus vartotojus");
                Console.WriteLine("9. Gauti vartotojus pagal rolę");
                Console.WriteLine("10. Išeiti");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        GetUserById();
                        break;
                    case "3":
                        UpdateUser();
                        break;
                    case "4":
                        DeleteUser();
                        break;
                    case "5":
                        ChangePassword();
                        break;
                    case "6":
                        ActivateUser();
                        break;
                    case "7":
                        DeactivateUser();
                        break;
                    case "8":
                        GetAllUsers();
                        break;
                    case "9":
                        ListUsersByRole();
                        break;
                    case "10":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Netinkamas pasirinkimas. Bandykite dar kartą.");
                        break;
                }

                if (!exit)
                {
                    Console.Write("\nSpauskite bet kurią klavišą, kad tęstumėte...");
                    Console.ReadKey();
                }
            }
        }

        static void RegisterUser()
        {
            Console.Write("Įveskite vartotojo vardą: ");
            string username = Console.ReadLine();
            Console.Write("Įveskite slaptažodį: ");
            string password = Console.ReadLine();
            Console.Write("Ar vartotojas aktyvus (true/false): ");
            bool isActive = bool.Parse(Console.ReadLine());

            Console.Write("Įveskite vartotojo tipą (Administrator / StandardUser): ");
            string role = Console.ReadLine();

            User user = role.ToLower() == "Administrator" ?
                        new Admin { Username = username, Password = password, IsActive = isActive } :
                        new StandardUser { Username = username, Password = password, IsActive = isActive };

            _userService.RegisterUser(user);
            Console.WriteLine("Vartotojas buvo sėkmingai užregistruotas.");
        }

        static void GetUserById()
        {
            Console.Write("Įveskite vartotojo ID: ");
            int id = int.Parse(Console.ReadLine());
            User user = _userService.GetUser(id);
            if (user != null)
            {
                Console.WriteLine($"ID: {user.Id}, Vardas: {user.Username}, Aktyvus: {user.IsActive}, Rolė: {user.Role}");
            }
            else
            {
                Console.WriteLine("Vartotojas nerastas.");
            }
        }

        static void UpdateUser()
        {
            Console.Write("Įveskite vartotojo ID, kurį norite atnaujinti: ");
            int id = int.Parse(Console.ReadLine());
            User user = _userService.GetUser(id);

            if (user != null)
            {
                Console.Write("Įveskite naują vartotojo vardą: ");
                user.Username = Console.ReadLine();
                Console.Write("Įveskite naują slaptažodį: ");
                user.Password = Console.ReadLine();
                Console.Write("Įveskite naują vartotojo aktyvumo būseną (true/false): ");
                user.IsActive = bool.Parse(Console.ReadLine());

                _userService.UpdateUser(user);
                Console.WriteLine("Vartotojas sėkmingai atnaujintas.");
            }
            else
            {
                Console.WriteLine("Vartotojas nerastas.");
            }
        }

        static void DeleteUser()
        {
            Console.Write("Įveskite vartotojo ID, kurį norite pašalinti: ");
            int id = int.Parse(Console.ReadLine());
            _userService.RemoveUser(id);
            Console.WriteLine("Vartotojas pašalintas.");
        }

        static void ChangePassword()
        {
            Console.Write("Įveskite vartotojo ID, kurį norite atnaujinti slaptažodį: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Įveskite naują slaptažodį: ");
            string newPassword = Console.ReadLine();
            _userService.UpdatePassword(id, newPassword);
            Console.WriteLine("Slaptažodis sėkmingai pakeistas.");
        }

        static void ActivateUser()
        {
            Console.Write("Įveskite vartotojo ID, kurį norite aktyvuoti: ");
            int id = int.Parse(Console.ReadLine());
            _userService.ActivateUser(id);
            Console.WriteLine("Vartotojas aktyvuotas.");
        }

        static void DeactivateUser()
        {
            Console.Write("Įveskite vartotojo ID, kurį norite deaktyvuoti: ");
            int id = int.Parse(Console.ReadLine());
            _userService.DeactivateUser(id);
            Console.WriteLine("Vartotojas deaktyvuotas.");
        }

        static void GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Vardas: {user.Username}, Aktyvus: {user.IsActive}, Rolė: {user.Role}");
            }
        }

        static void ListUsersByRole()
        {
            Console.Write("Įveskite rolę (Administrator / StandardUser): ");
            string role = Console.ReadLine();
            var users = _userService.ListUsersByRole(role);
            foreach (var user in users)
            {
                Console.WriteLine($"ID: {user.Id}, Vardas: {user.Username}, Aktyvus: {user.IsActive}, Rolė: {user.Role}");
            }
        }
    }
}
