using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGLabAPI.Application.Interfaces.Repositories.Common
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> Get(object id);
        Task<IEnumerable<T>> GetAll();
        Task<T> Insert(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
