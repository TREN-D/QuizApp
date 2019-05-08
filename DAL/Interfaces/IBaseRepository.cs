using System.Threading;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBaseRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
