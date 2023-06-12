using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;

namespace MyLeasing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user =
                await _userHelper.GetUserByEmailAsync("ldvssr@icloud.com");

            if (user == null)
            {
                user = new User
                {
                    FirstName = "Licínio",
                    LastName = "Rosário",
                    Email = "ldvssr@icloud.com",
                    UserName = "ldvssr@icloud.com",
                    PhoneNumber = "919999999",
                    Document = "12321321"
                };

                var results = await _userHelper.AddUserAsync(user, "123456");

                if (results != IdentityResult.Success)
                    throw new InvalidOperationException(
                        "Could not create the user in seeder");
            }

            if (!_context.Owners.Any())
            {
                AddOwner("Sophia", "Martins", "Rua Flores", user);
                AddOwner("Gabriel", "Rocha", "Avenida das Palmeiras", user);
                AddOwner("Isabella", "Ferreira", "Rua dos Pássaros", user);
                AddOwner("Arthur", "Gomes", "Avenida Central", user);
                AddOwner("Mariana", "Carvalho", "Rua das Rosas", user);
                AddOwner("Davi", "Almeida", "Avenida dos Sonhos", user);
                AddOwner("Lívia", "Oliveira", "Rua das Estrelas", user);
                AddOwner("Pedro", "Santana", "Avenida das Acácias", user);
                AddOwner("Lara", "Mendes", "Rua dos Ipês", user);
                AddOwner("Enzo", "Barros", "Avenida das Oliveiras", user);

                await _context.SaveChangesAsync();
            }

            if (!_context.Lessees.Any())
            {
                AddLessee("Olivia", "Anderson", "111 Maple Street", user);
                AddLessee("James", "Taylor", "222 Oak Avenue", user);
                AddLessee("Ava", "Walker", "333 Elm Lane", user);
                AddLessee("Liam", "Harris", "444 Pine Road", user);
                AddLessee("Emma", "Clark", "555 Cedar Drive", user);

                await _context.SaveChangesAsync();
            }
        }

        private void AddLessee(string firstName, string lastName,
            string address, User user)
        {
            _context.Lessees.Add(new Lessee
            {
                Document = _random.Next(999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(999999999),
                CellPhone = _random.Next(999999999),
                Address = address,
                User = user
            });
        }

        private void AddOwner(string firstName, string lastName, string address,
            User user)
        {
            _context.Owners.Add(new Owner
            {
                Document = _random.Next(999999).ToString(),
                FirstName = firstName,
                LastName = lastName,
                FixedPhone = _random.Next(999999999),
                CellPhone = _random.Next(999999999),
                Address = address,
                User = user
            });
        }
    }
}