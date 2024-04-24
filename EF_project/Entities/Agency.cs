using EF_project.Entity;

namespace EF_project.Entities;

public class Agency {

    public int Id { get; set; }

    public string? Name { get; set; }

    public float Rating { get; set; }

    public HashSet<Tour>? Tours { get; set; } = new HashSet<Tour>();
    public Agency() { }

    public Agency(string agency, float rating) {
        Name = agency;
        Rating = rating;
    }
}