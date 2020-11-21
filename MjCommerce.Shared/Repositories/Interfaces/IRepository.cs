using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MjCommerce.Shared.Repositories.Interfaces
{
    public interface IRepository<T> 
        where T : EntityBase, new()
    {
        Task<IEnumerable<T>> Get();
        Task<IEnumerable<T>> Get(IFilter<T> filter);
        Task<T> Get(int id);
        Task<int> Add(T entity);
        Task<T> Update(T entity);
        Task<int> Delete(int id);

        Task<bool> Conatins(string name);
    }
}