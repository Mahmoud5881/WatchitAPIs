using System.ComponentModel.DataAnnotations;

namespace WatchitAPIs.DTOs
{
    public class RegisterDTO
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string Email { get; set; }
        
        public string SubscriptionType {get; set;}
    }
}
