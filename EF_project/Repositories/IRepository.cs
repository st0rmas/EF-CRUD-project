namespace EF_project.Repositories;

public interface IRepository<T> {
    List<T> FindAll();
    T FindById(int id);
    void Save(T entity);
    void Delete(int id);
}