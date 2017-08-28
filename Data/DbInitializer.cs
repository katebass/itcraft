using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Travels.Models;

namespace Travels.Data
{
    public class DbInitializer
    {
        private static string getHash(string text)
        {
            // SHA512 is disposable by inheritance.
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Get the hashed string.
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        public static void Initialize(TravelContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }

            var clients = new Clients[]
            {
            new Clients{ClientName="Carson"},
            new Clients{ClientName="Meredith"},
            new Clients{ClientName="Arturo"},
            new Clients{ClientName="John"},
            new Clients{ClientName="Alice"}
            };
            foreach (Clients s in clients)
            {
                context.Clients.Add(s);
            }
            context.SaveChanges();

            var tours = new Tours[]
            {
            new Tours{TourName="Chemistry", Date = DateTime.Parse("2005-09-01")},
            new Tours{TourName="Microeconomics", Date = DateTime.Parse("2006-09-01")},
            new Tours{TourName="Macroeconomics", Date = DateTime.Parse("2006-10-01")},
            new Tours{TourName="Palma", Date = DateTime.Parse("2003-08-01")},
            new Tours{TourName="Kssda", Date = DateTime.Parse("2018-09-07")}
            };
            foreach (Tours c in tours)
            {
                context.Tours.Add(c);
            }
            context.SaveChanges();

            var excursions = new Excursion_Sights[]
            {
            new Excursion_Sights{ExcursionName="Frist"},
            new Excursion_Sights{ExcursionName="Second"},
            new Excursion_Sights{ExcursionName="Third"},
            new Excursion_Sights{ExcursionName="Fouth"},
            new Excursion_Sights{ExcursionName="Fifth"}
            };
            foreach (Excursion_Sights e in excursions)
            {
                context.Excursion_Sights.Add(e);
            }
            context.SaveChanges();

            var tourexcursion = new Tours_Excursions[]
            {
            new Tours_Excursions{ToursID=5, Excursion_SightsID=1},
            new Tours_Excursions{ToursID=5, Excursion_SightsID=1},
            new Tours_Excursions{ToursID=1, Excursion_SightsID=1},
            new Tours_Excursions{ToursID=2, Excursion_SightsID=4},
            new Tours_Excursions{ToursID=3, Excursion_SightsID=2},
            new Tours_Excursions{ToursID=4, Excursion_SightsID=3},
            new Tours_Excursions{ToursID=3, Excursion_SightsID=5},
            new Tours_Excursions{ToursID=3, Excursion_SightsID=4},
            };
            foreach (Tours_Excursions e in tourexcursion)
            {
                context.Tours_Excursions.Add(e);
            }
            context.SaveChanges();

            var tourclient = new Tours_Clients[]
            {
            new Tours_Clients{ToursID=1, ClientsID=2},
            new Tours_Clients{ToursID=2, ClientsID=1},
            new Tours_Clients{ToursID=3, ClientsID=2},
            new Tours_Clients{ToursID=3, ClientsID=3},
            new Tours_Clients{ToursID=4, ClientsID=4},
            new Tours_Clients{ToursID=5, ClientsID=5},
            new Tours_Clients{ToursID=5, ClientsID=2},
            new Tours_Clients{ToursID=1, ClientsID=4},
            new Tours_Clients{ToursID=2, ClientsID=1},
            };
            foreach (Tours_Clients e in tourclient)
            {
                context.Tours_Clients.Add(e);
            }
            context.SaveChanges();

            var user = new Users[]
            {
            new Users{Email="user1@gmail.com", Password=getHash("12345")},
            new Users{Email="user2@gmail.com", Password=getHash("12345")},
            new Users{Email="user3@gmail.com", Password=getHash("12345")},
            new Users{Email="user4@gmail.com", Password=getHash("12345")},
            };
            foreach (Users e in user)
            {
                context.Users.Add(e);
            }
            context.SaveChanges();

        }
    }
}
