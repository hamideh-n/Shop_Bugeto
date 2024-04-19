using Domain.Users.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interface.DbContext
{
    public interface IDatabaseContext
    {
        int SaveChanges();
        int SaveChanges(bool acceptAllChangesOnSuccess);
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
