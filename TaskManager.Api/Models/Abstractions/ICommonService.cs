namespace TaskManager.Api.Models
{
    public interface ICommonService<T>
    {
        bool Сreate(T dto, out int id);
        bool Update(T dto, int id);
        bool Delete(int id);
    }
}
