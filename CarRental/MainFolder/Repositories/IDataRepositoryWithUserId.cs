using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.MainFolder.Repositories
{
    
        public interface IDataRepositoryWithUserId<T>
        {
            List<T> GetDataWithUserId(int userId);
        }
    
}
