using WatchitAPIs.DTOs;
using WatchitAPIs.Models;
using WatchitAPIs.Repository_Contract;

namespace WatchitAPIs.Services_Contract
{
    public interface ISubscriptionService : IGenericRepository<Subscription>
    {
        Subscription CreateSub(RegisterDTO newUser);
    }
}
