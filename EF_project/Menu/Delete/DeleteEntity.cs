using EF_project.Configuration;
using EF_project.ConsoleWriter;
using EF_project.Entity;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;

namespace EF_project.Menu.Delete;

public class DeleteEntity {
    public void Start() {
        MenuWriter.DeleteEntity();
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice) {
            case 1:
                DeleteClient();
                break;
            case 2:
                DeleteTour();
                break;
            case 3:
                DeleteCity();
                break;
            case 4:
                DeleteAgency();
                break;
            default:
                Console.WriteLine("invalid operation");
                break;
        }

        MainMenu menu = new MainMenu();
        menu.Start();
    }
    //можно ли упростить работу через наследование интерфейса (все сущности наследуются от IEntity)
    //метод DeleteAgency тогда выглядит: 
    // private void DeleteAgency(IEntity entity){
    //    switch(typeof(entity)){}  
    // } 
    private void DeleteAgency() {
        Console.WriteLine("Enter agency name: ");
        string name = Console.ReadLine();
        try {
            using (ApplicationContext db = new ApplicationContext()) {
                var agency = db.Agencies.FirstOrDefault(ag => ag.Name==name);
                if (agency != null) {
                    db.Agencies.Remove(agency);
                }

                db.SaveChanges();
            }
            
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }
    //Удаление города
    private void DeleteCity() {
        using (ApplicationContext db = new ApplicationContext()) {
            var cities = db.Cities
                
                .ToList();
            foreach (var city in cities) {
                Console.WriteLine($"[{city.Id}] - {city.Name}");
            }
        }
        Console.WriteLine("Enter city name: ");
        string cityName = Console.ReadLine();
        try {
            using (ApplicationContext db = new ApplicationContext()) {
                var city = db.Cities.FirstOrDefault(c => c.Name == cityName);
                if (city != null) {
                    db.Cities.Remove(city);
                }

                db.SaveChanges();

            }
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }
    //Удаление тура
    private void DeleteTour() {
        using (ApplicationContext db = new ApplicationContext()) {
            var tours = db.Tours
                .Include(t=>t.Agency)
                .Include(t=>t.City)
                .ToList();
            foreach (var tour in tours) {
                Console.WriteLine($"[{tour.Id}] {tour.City.Name} {tour.Agency.Name}");
            }
        }

        Console.WriteLine();
        Console.WriteLine("Enter tour id: ");
        int id  = Convert.ToInt32(Console.ReadLine());
        try {
            using (ApplicationContext db = new ApplicationContext()) {
                var tour = db.Tours.Find(id);
                if (tour != null) {
                    db.Tours.Remove(tour);
                }
                db.SaveChanges();
            }
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }

    //Удаление клиента
    private void DeleteClient() {
        using (ApplicationContext db = new ApplicationContext()) {
            var clients = db.Clients.Include(c=>c.Passport).ToList();
            foreach (var client in clients) {
                Console.WriteLine($"[{client.Id}] {client.FirstName} {client.SecondName} {client.Passport.BirthDay}");
            }
        }
        Console.WriteLine("Enter client id: ");
        int id = Convert.ToInt32(Console.ReadLine());
        try {
            using (ApplicationContext db = new ApplicationContext()) {
                var client = db.Clients.FirstOrDefault(cl => cl.Id == id);
                if (client != null) {
                    db.Clients.Remove(client);
                }
                db.SaveChanges();

            }
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }


}