using GeneratorHelper;

namespace Blazortastic.Data
{
    public class CrudServiceTest : ICrudService<Test>
    {
        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Test[]> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Test> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Post(Test entity)
        {
            throw new NotImplementedException();
        }
    }
}
