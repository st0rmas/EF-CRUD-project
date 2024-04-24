using EF_project.Entities;

namespace EF_project.Entity;

public class Tour {
    public int Id { get; set; }
    public DateOnly DepartureTime { get; set; }
    public DateOnly ReturnTime { get; set; }
    public float Price { get; set; }
    
    public int CityId { get; set; }
    public City City { get; set; }
    
    public int AgencyId { get; set; }
    public Agency Agency { get; set; }

    public ISet<Client> Clients { get; set; } = new HashSet<Client>();

}