using EF_project.Entity;

namespace EF_project.Entities;

public class City {
    public int Id { get; set; }
    public string? Name { get; set; }
    public HashSet<Tour>? Tours { get; set; } = new();
    public City() { }

    public City(string? name) {
        Name = name;
    }
}