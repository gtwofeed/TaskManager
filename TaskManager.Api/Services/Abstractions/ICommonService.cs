namespace TaskManager.Api.Services.Abstractions
{
    public interface ICommonService<T>
    {
        bool Сreate(T dto, out int id);
        T? Get(int id);
        bool Update(T dto, int id);
        bool Delete(int id);
    }
}
