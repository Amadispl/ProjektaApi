using Projekt.Entities;

namespace Projekt
{
    public class PizzeriaSeeder
    {
        private readonly PizzeriaDbContext _context;
        public PizzeriaSeeder(PizzeriaDbContext restaurantDbContext)
        {
            _context = restaurantDbContext;
        }
        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                if (!_context.Roles.Any())
                {
                    var roles = GetRoles();
                    _context.Roles.AddRange(roles);
                    _context.SaveChanges();
                }
                if (!_context.Pizzerias.Any())
                {
                    var pizzerias = GetPizzerias();
                    _context.Pizzerias.AddRange(pizzerias);
                    _context.SaveChanges();
                }
                if(!_context.Users.Any())
                {
                    var users = GetUsers();
                    _context.Users.AddRange(users);
                    _context.SaveChanges();
                }

            }
        }
        private IEnumerable<Pizzeria> GetPizzerias()
        {
            var pizzerias = new List<Pizzeria>()
            {

            new Pizzeria()
            {
                Name = "Dagrasso",
                Description = "Pyszna Pizza",
                Email = "contact@dagrasso.com",
                Number = "789789789",
                HasDelivery = true,
                Menu = new List<Menu>()
                {
                    new Menu()
                    {
                        Name = "Margaritha",
                        Description = "Ser",
                        Price = 20.00M,
                    },
                    new Menu()
                    {
                        Name = "Capriciosa",
                        Description = "Ser,Szynka,Pieczarki",
                        Price = 25.00M,
                    }
                },
                Address = new Address()
                {
                    City = "Kraków",
                    Street = "Mogilska",
                    Number = "7",
                    PostalCode = "30-528",
                }

            },
            new Pizzeria()
            {
                Name = "Pizza Hut",
                Description = "Pizza dla Ciebie",
                Email = "contact@pizzahut.com",
                Number = "120000000",
                HasDelivery = true,
                Menu = new List<Menu>()
                    {
                        new Menu()
                        {
                            Name = "Margaritha",
                            Description = "Ser",
                            Price = 22.00M,
                        },
                        new Menu()
                        {
                            Name = "Capriciosa",
                            Description = "Ser,Szynka,Pieczarki",
                            Price = 26.00M,
                        }
                    },
                Address = new Address()
                {
                    City = "Kraków",
                    Street = "Limanowskiego",
                    Number = "5",
                    PostalCode = "33-520",
                }


            }
            };

            return pizzerias;
        }
        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
           {
               new Role()
               {
                   Name="Admin"
               },
               new Role()
               {
                   Name="User"
               },
               new Role()
               {
                   Name="Manager"
               },

           };
            return roles;
        }
        private IEnumerable<User> GetUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Email="user@gmail.com",
                    FirstName="user",
                    LastName="user",
                    PasswordHash="AQAAAAEAACcQAAAAEFQwkuLIeDy8IZyqn8A+VOLA2cd29/Yf1w19OL8TtU9kB2lw7ycimjmdoBia8eMVSg==",
                    Nationality="Poland",
                    DateofBirth=DateTime.Parse("2000-10-5"),
                    RoleId=2

                },
                new User()
                {
                    Email="admin@gmail.com",
                    FirstName="admin",
                    LastName="admin",
                    PasswordHash="AQAAAAEAACcQAAAAEFQwkuLIeDy8IZyqn8A+VOLA2cd29/Yf1w19OL8TtU9kB2lw7ycimjmdoBia8eMVSg==",
                    Nationality="Poland",
                    DateofBirth=DateTime.Parse("2000-11-6"),
                    RoleId=1

                },
                new User()
                {
                    Email="manager@gmail.com",
                    FirstName="manager",
                    LastName="manager",
                    PasswordHash="AQAAAAEAACcQAAAAEFQwkuLIeDy8IZyqn8A+VOLA2cd29/Yf1w19OL8TtU9kB2lw7ycimjmdoBia8eMVSg==",
                    Nationality="Poland",
                    DateofBirth=DateTime.Parse("2000-12-7"),
                    RoleId=3

                }
            };
            return users;
        }
    }
}


