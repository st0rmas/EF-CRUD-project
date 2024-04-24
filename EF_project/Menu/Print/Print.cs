using EF_project.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EF_project.Menu.Print;

public class Print {
    public void Start() {
        Console.Clear();
        Console.WriteLine("What do you want to print?");
        Console.WriteLine("[1].Clients");
        Console.WriteLine("[2].Tours");
        Console.Write(">>> ");
        int choice = Convert.ToInt32(Console.ReadLine());
        if (choice == 1) {
            ShowClients();
        }
        else {
            ShowTours();
        }

        MainMenu menu = new MainMenu();
        menu.Start();
    }

    private void ShowClients() {
        Console.WriteLine("Clients list");
        using (ApplicationContext db = new ApplicationContext()) {
            var clients = db.Clients.Include(cl => cl.Passport).ToList();
            foreach (var client in clients) {
                Console.WriteLine($"[{client.Id}] {client.FirstName} {client.SecondName}({client.Phone}) - {client.Passport.BirthDay}");
            }
        }
    }
    private void ShowTours() {
        Console.WriteLine("Tours list");
        using (ApplicationContext db = new ApplicationContext()) {
            var tours = db.Tours.Include(t=>t.City).Include(t=>t.Agency).ToList();
            foreach (var tour in tours) {
                Console.WriteLine($"[{tour.Id}] {tour.City.Name} - {tour.DepartureTime} ({tour.Agency.Name})");
            }
        }
    }
}