using EF_project.Configuration;
using EF_project.Entity;
using Microsoft.EntityFrameworkCore;

namespace EF_project.Repositories;

public class ClientRepository : IRepository<Client> {

    private ApplicationContext _db;
    public DbSet<Client> Clients { get; set; }
    
    public ClientRepository(ApplicationContext db) {
        _db = db;
    }

    public List<Client> FindAll() {
        return _db.Clients.ToList();
    }

    public Client FindById(int id) {
        return _db.Clients.Find(id);
    }

    public void Save(Client entity) {
        throw new NotImplementedException();
    }

    public void Delete(int id) {
        throw new NotImplementedException();
    }
}