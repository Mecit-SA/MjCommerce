using MjCommerce.Shared.Models.Base;
using System.Linq;

namespace MjCommerce.Shared.Filters.Interfaces
{
    public interface IFilter<T> where T : EntityBase, new()
    {
        IQueryable<T> Build(IQueryable<T> initialSet, bool applyPaging);
    }
}