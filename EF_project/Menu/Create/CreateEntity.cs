using System.IO.Pipes;
using EF_project.Configuration;
using EF_project.ConsoleWriter;
using EF_project.Entities;
using EF_project.Entity;
using Microsoft.EntityFrameworkCore;

namespace EF_project.Menu.Create;

public class CreateEntity {
    public void Start() {
        MenuWriter.CreateEntitiesMenu();
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice) {
            case 1:
                Client client = CreateClient();
                using (ApplicationContext db = new ApplicationContext()) {
                    db.Clients.Add(client);
                    db.SaveChanges();
                };
                break;
            case 2:
                Tour tour = CreateTour();
                Console.WriteLine($"{tour.DepartureTime} {tour.City.Name} {tour.CityId} {tour.Agency.Name} {tour.AgencyId}");
                // using (ApplicationContext db = new ApplicationContext()) {
                    // db.Tours.Include(c=>c.City).Include(c=>c.Agency);
                    // db.Tours.Add(tour);
                    // db.SaveChanges();
                // };
                break;
            case 3:
                Buy();
                break;
        }

        MainMenu menu = new MainMenu();
        menu.Start();
    }

    private void Buy() {
        Console.Clear();
        using (ApplicationContext db = new ApplicationContext()) {
            var clients = db.Clients.Include(cl => cl.Passport).ToList();
            var tours = db.Tours.Include(t=>t.City).Include(t=>t.Agency).ToList();
            Console.WriteLine("CLIENTS");
            foreach (var client in clients) {
                Console.WriteLine($"[{client.Id}] {client.FirstName} {client.SecondName}({client.Phone}) - {client.Passport.BirthDay}");
            }

            Console.WriteLine();
            Console.WriteLine("TOURS");
            foreach (var tour in tours) {
                Console.WriteLine($"[{tour.Id}] {tour.City.Name} - {tour.DepartureTime} ({tour.Agency.Name})");
            }
        }

        Console.Write("Enter client and tour id (x x, where x is ID): ");
        string data = Console.ReadLine();
        int clientId = Convert.ToInt32(data.Split(" ")[0]);
        int tourId = Convert.ToInt32(data.Split(" ")[1]);
        
        using (ApplicationContext db = new ApplicationContext()) {
            var client = db.Clients.FirstOrDefault(c => c.Id == clientId);
            var tour = db.Tours.FirstOrDefault(t => t.Id == tourId);
            client.Tours.Add(tour);
            tour.Clients.Add(client);
            db.SaveChanges();
        }
    }

    private Tour CreateTour() {
        Tour tour = new Tour();
        using (ApplicationContext db = new ApplicationContext()) {
            Console.Write("Enter departure time: ");
            tour.DepartureTime = DateOnly.Parse(Console.ReadLine());
            Console.Write("Enter return time: ");
            tour.ReturnTime = DateOnly.Parse(Console.ReadLine());
            Console.Write("Enter price: ");
            tour.Price = Convert.ToSingle(Console.ReadLine());
            
            Console.Write("Enter city name: ");
            string cityName = Console.ReadLine();
            var city = db.Cities.FirstOrDefault(c => c.Name == cityName);
            if (city==null) {
                Console.WriteLine("not found");
                // CreateTour();
            }
            else {
                tour.City = city;
                tour.CityId = city.Id;
            }
            
            
            Console.Write("Enter agency name: ");
            string agencyName = Console.ReadLine();
            var agency = db.Agencies.FirstOrDefault(a => a.Name == agencyName);
            if (agency==null) {
                Console.WriteLine("not found");
                // CreateTour();
            }
            else {
                tour.Agency = agency;
                tour.AgencyId = agency.Id;
            }
            db.Tours.Add(tour);

            db.SaveChanges();
        }
        return tour;
    }

    private Client CreateClient() {
        Client client = new Client();
        
        Console.Write("Enter first name: ");
        client.FirstName = Console.ReadLine();
        Console.Write("Enter second name: ");
        client.SecondName = Console.ReadLine();
        
        Console.Write("Enter phone: ");
        client.Phone = Console.ReadLine();
        
        Console.Write("Enter passport series: ");
        string series = Console.ReadLine();
        
        Console.Write("Enter birth date: ");
        DateOnly birth = DateOnly.Parse(Console.ReadLine());

        client.Passport = new Passport(series,birth);
        
        return client;
    }
    
}