using EF_project.Entity;

namespace EF_project.Entities;

public class Passport {
    public int Id { get; set; }
    public int ClientId { get; set; }
    public string? Series { get; set; }  
    public DateOnly BirthDay { get; set; }
    public Client Client { get; set; }
    public Passport() { }

    public Passport(string? series, DateOnly birthDay) {
        Series = series;
        BirthDay = birthDay;
    }

}