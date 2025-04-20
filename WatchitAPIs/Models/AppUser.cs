using Microsoft.AspNetCore.Identity;

namespace WatchitAPIs.Models;

public class AppUser : IdentityUser
{
    public int SubscriptionId { get; set; }
    
    public Subscription Subscription { get; set; }

    public List<FavoriteMovies> UsersFavorite { get; set; }
    
    public List<UserWatchRecord>? Records { get; set; }
}