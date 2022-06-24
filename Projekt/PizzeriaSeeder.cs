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
    }
}


