namespace EF_project.ConsoleWriter;

public class MenuWriter {
    public static void MainMenu() {
        Console.WriteLine("[1]. Create entities");
        Console.WriteLine("[2]. Print entities");
        Console.WriteLine("[3]. Update entity");
        Console.WriteLine("[4]. Delete entity");
        Console.WriteLine("[0]. Exit");
        Console.Write(">>> ");
    }

    public static void CreateEntitiesMenu() {
        Console.WriteLine("Choose entity");
        Console.WriteLine("[1]. Client");
        Console.WriteLine("[2]. Tour");
        Console.WriteLine("[3]. Make an order");
        Console.Write(">>> ");
    }

    public static void DeleteEntity() {
        Console.WriteLine("Choose entity");
        Console.WriteLine("[1]. Client");
        Console.WriteLine("[2]. Tour");
        Console.WriteLine("[3]. City");
        Console.WriteLine("[4]. Agency");
        Console.Write(">>> ");
    }

    public static void UpdateEntity() {
        Console.WriteLine("Choose entity");
        Console.WriteLine("[1]. Client");
        Console.WriteLine("[2]. Tour");
        Console.Write(">>> ");
    }
}