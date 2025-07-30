using System.Collections.Generic;

namespace CarRental.MainFolder.Repositories
{
    public interface IDataRepository<T>
    {
        bool AddData(T data);
        bool RemoveData(T data);
        bool ChangeData(int id, T newData);
        List<T> GetData();
       

    }
}
