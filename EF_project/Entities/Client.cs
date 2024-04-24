using EF_project.Entities;

namespace EF_project.Entity;

public class Client {
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? Phone { get; set; }
    public Passport Passport { get; set; }
    public ISet<Tour>? Tours { get; set; } = new HashSet<Tour>();
}

