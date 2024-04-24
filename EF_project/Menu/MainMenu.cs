using EF_project.ConsoleWriter;
using EF_project.Menu.Create;
using EF_project.Menu.Delete;
using EF_project.Menu.Update;

namespace EF_project.Menu;

public class MainMenu {
    public void Start() {
        MenuWriter.MainMenu();
        int choice = Convert.ToInt32(Console.ReadLine());
        switch (choice) {
            case 1:
                CreateEntity createEntity = new CreateEntity();
                createEntity.Start();
                break;
            case 2:
                Print.Print print = new Print.Print();
                print.Start();
                break;
            case 3:
                UpdateEntity update = new UpdateEntity();
                update.Start();
                break;
            case 4:
                DeleteEntity delete = new DeleteEntity();
                delete.Start();
                break;
            case 0:
                break;
        }
    }
}