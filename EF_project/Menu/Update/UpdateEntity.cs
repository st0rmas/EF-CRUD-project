using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using EF_project.Configuration;
using EF_project.ConsoleWriter;
using EF_project.Entities;
using EF_project.Entity;
using Microsoft.EntityFrameworkCore;

namespace EF_project.Menu.Update;

public class UpdateEntity {
    public void Start() {
        MenuWriter.UpdateEntity();
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice) {
            case 1:
                // getProperties(new Client());
                Client client = new Client();
                using (ApplicationContext db = new ApplicationContext()) {
                    var clients = db.Clients.Include(c=>c.Passport).ToList();
                    foreach (var cl in clients) {
                        Console.WriteLine($"[{cl.Id}] {cl.FirstName} {cl.SecondName} {cl.Passport.BirthDay}");
                    }

                    Console.Write("Enter client id: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    client = clients.FirstOrDefault(cl => cl.Id == id);
                }
                Console.WriteLine();
                UpdateClient(client);
                break;
            case 2:
                Tour tour = new Tour();
                using (ApplicationContext db = new ApplicationContext()) {
                    var tours = db.Tours.Include(c=>c.Agency).Include(c=>c.City).ToList();
                    foreach (var cl in tours) {
                        Console.WriteLine($"[{cl.Id}] {cl.City.Name} {cl.Agency.Name}");
                    }

                    Console.Write("Enter tour id: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    tour = tours.FirstOrDefault(cl => cl.Id == id);
                }
                UpdateTour(tour);
                break;
            default:
                Console.WriteLine("invalid operation");
                break;
        }

        MainMenu menu = new MainMenu();
        menu.Start();
    }

    private void UpdateTour(Tour tour) {
        Console.WriteLine($"{tour.DepartureTime} {tour.Agency.Name}");
        Console.WriteLine("Choose a property to update: ");
        Console.WriteLine("[1]. Departure time");
        Console.WriteLine("[2]. Price");
        Console.WriteLine("[3]. City");
        Console.WriteLine("[4]. Agency");
        int choice = Convert.ToInt32(Console.ReadLine());
        
            switch (choice) {
                case 1:
                    Console.WriteLine("Enter new value: ");
                    tour.DepartureTime = DateOnly.Parse(Console.ReadLine());
                    break;
                case 2:
                    Console.WriteLine("Enter new value: ");
                    tour.Price = Convert.ToSingle(Console.ReadLine());
                    break;
                case 3:
                    
                    using (ApplicationContext db = new ApplicationContext()) {
                        Console.WriteLine("Enter new city name:");
                        string cityName = Console.ReadLine();
                        var selectedCity = db.Cities.FirstOrDefault(c => c.Name == cityName);
                        Console.WriteLine(selectedCity.Id);
                        if (selectedCity != null) {
                            tour.City = selectedCity;
                            tour.CityId = selectedCity.Id;
                            Console.WriteLine("OK");
                        }

                        Console.WriteLine(tour.CityId);
                        Console.WriteLine(tour.City.Name);
                        db.Tours.Update(tour);
                        db.SaveChanges();
                    }

                    break;
                    
                case 4:
                    using (ApplicationContext db = new ApplicationContext()) {
                        Console.WriteLine("Enter new agency name:");
                        string agencyName = Console.ReadLine();
                        var agency = db.Agencies.FirstOrDefault(c => c.Name == agencyName);
                        if (agency != null) {
                            tour.Agency = agency;
                            tour.AgencyId = agency.Id;
                            Console.WriteLine("OK");
                        }

                        db.Tours.Update(tour) ;
                        db.SaveChanges();
                    }
                    break;
                default:
                    Console.WriteLine("invalid operation");
                    break;
                
            }

            
        }
    
    private void UpdateClient(Client client) {
        Console.WriteLine("Choose a property to update: ");
        Console.WriteLine("[1]. First name");
        Console.WriteLine("[2]. Second name");
        Console.WriteLine("[3]. Phone");
        Console.WriteLine("[4]. Passport");
        int choice = Convert.ToInt32(Console.ReadLine());
        using (ApplicationContext db = new ApplicationContext()) {
            switch (choice) {
                case 1:
                    Console.WriteLine("Enter new value: ");
                    client.FirstName = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("Enter new value: ");
                    client.SecondName = Console.ReadLine();
                    break;
                case 3:
                    Console.WriteLine("Enter new value: ");
                    client.Phone = Console.ReadLine();
                    break;
                case 4:
                    Console.Write("Series: ");
                    string series = Console.ReadLine();
                    client.Passport = new Passport() {
                        Id = client.Passport.Id,
                        ClientId = client.Id,
                        Series = series,
                        BirthDay = client.Passport.BirthDay
                    };
                    break;
                default:
                    Console.WriteLine("invalid operation");
                    break;
            }

            db.Clients.Update(client);
            db.SaveChanges();
        }
    }

    private void getProperties(object entity) {
        Type type = entity.GetType();
        int counter = 0;
        foreach (MemberInfo member in type.GetMembers(BindingFlags.DeclaredOnly |
                                                      BindingFlags.Instance |
                                                      BindingFlags.Public)) {
            if (member.MemberType==MemberTypes.Property && member.Name!="Id") {
                Console.WriteLine($"[{++counter}] {member.Name}");
            }
        }
    }
}