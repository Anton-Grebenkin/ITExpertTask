using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ITExpertTask.Data
{
    public interface IDataContextProvider
    {
        DataContext GetContext();
    }

    public class DataContextProvider : IDataContextProvider
    {

        private readonly DbContextOptions<DataContext> _options;
        public DataContextProvider(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            _options = optionsBuilder
                   .UseSqlServer(connectionString)
                   .Options;
        }

        public DataContext GetContext()
        {
            return new DataContext(_options);
        }
    }
}
