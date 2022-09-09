using System;
using System.Threading.Tasks;

namespace GeneratorHelper
{
    public interface ICrudService<TEntity>
    {
        Task<TEntity[]> GetAsync();

        Task<TEntity> GetById(Guid id);

        Task Post(TEntity entity);

        Task Delete(Guid id);
    }
}
