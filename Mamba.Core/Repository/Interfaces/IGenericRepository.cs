using MambaManyToManyCrud.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mamba.Core.Repository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        public DbSet<TEntity> Table { get; }
        Task<int> CommitAsync();
        Task<TEntity> GetByIdAsync(Expression<Func<TEntity,bool>>?expression,params string[]? includes);
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity,bool>>?expression,params string[]? includes);
        Task CreateAsync(TEntity entity);
        public void Delete(TEntity entity);
        IQueryable<TEntity> GetQueryable();
    }
}
