using WatchitAPIs.DTOs;
using WatchitAPIs.Models;
using WatchitAPIs.Services_Contract;
using WatchitAPIs.Repository;
using WatchitAPIs.EFCore;
using Microsoft.EntityFrameworkCore;

namespace WatchitAPIs.Services
{
    public class SubscriptionService : GenericRepository<Subscription> , ISubscriptionService
    {
        private readonly AppDbContext context;

        public SubscriptionService(AppDbContext context) : base(context)
        {
            this.context = context;
        }
        

        public Subscription CreateSub(RegisterDTO newUser)
        {
            var subType = newUser.SubscriptionType;
            var subStartDate = DateTime.Now;
            var subEndDate = newUser.SubscriptionType switch
            {
                "Basic" => DateTime.Now.AddDays(15),
                "Standard" => DateTime.Now.AddMonths(1),
                "Premium" => DateTime.Now.AddMonths(2),
                _ => DateTime.Now
            };
            return new Subscription()
            {
                Type = subType,
                StartDate = subStartDate,
                EndDate = subEndDate
            };
        }
    }
}
