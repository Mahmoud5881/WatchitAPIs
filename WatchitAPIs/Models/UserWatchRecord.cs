using System.ComponentModel.DataAnnotations;
using WatchitAPIs.Models;

namespace WatchitAPIs.Models;

public class UserWatchRecord
{
    public AppUser User { get; set; }
    
    public string UserId { get; set; }
    
    public Movie Movie { get; set; }
    
    public int MovieId { get; set; }
    
    public DateTime DateOfWatching { get; set; }
    
    [Range(1,5)]
    public int Rating { get; set; }
}