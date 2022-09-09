using GeneratorHelper;

namespace Blazortastic.Data
{
    public class CrudServiceTestobjectje : ICrudService<Testobjectje>
    {
        public List<Testobjectje> Testobjectjes { get; set; } = new()
        {
            new Testobjectje{ Name = "Victor" },
            new Testobjectje{ Name = "And" },
            new Testobjectje{ Name = "André" },
            new Testobjectje{ Name = "Doing" },
            new Testobjectje{ Name = "Epic" },
            new Testobjectje{ Name = "Blazor" },
            new Testobjectje{ Name = "Sh💩t" },
        };

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Testobjectje[]> GetAsync()
        {
            await ApplyFakeSlowApiEffect();
            return Testobjectjes.ToArray();
        }

        public Task<Testobjectje> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Post(Testobjectje entity)
        {
            Testobjectjes.Add(entity);
            return Task.CompletedTask;
        }

        Task ApplyFakeSlowApiEffect()
        {
            return Task.Delay(1000);
        }
    }
}
