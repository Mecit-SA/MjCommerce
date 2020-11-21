using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MjCommerce.Shared.Filters.Interfaces;
using MjCommerce.Shared.Models.Base;
using MjCommerce.Shared.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MjCommerce.Shared.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : EntityBase, new()
    {
        private readonly MjCommerceDbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<T> _table;

        public RepositoryBase(MjCommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _table = context.Set<T>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _table.ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(IFilter<T> filter)
        {
            return await filter.Build(_table, true).ToListAsync();
        }

        public async Task<T> Get(int id)
        {
            return await _table.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<int> Add(T entity)
        {
            var result = await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<T> Update(T entity)
        {
            var result = await _table.FirstOrDefaultAsync(e => e.Id == entity.Id);

            if (result != null)
            {
                _mapper.Map(entity, result);
                await _context.SaveChangesAsync();
            }

            return result;
        }

        public async Task<int> Delete(int id)
        {
            var result = await _table.FirstOrDefaultAsync(e => e.Id == id);

            if (result != null)
            {
                _table.Remove(result);
                await _context.SaveChangesAsync();
                return result.Id;
            }

            return -1;
        }

        public async Task<bool> Conatins(string name)
        {
            var result = await _table.FirstOrDefaultAsync(e => e.Name == name);
            return result != null;
        }
    }
}