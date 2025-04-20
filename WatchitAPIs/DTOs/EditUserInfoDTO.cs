using System.ComponentModel.DataAnnotations;

namespace WatchitAPIs.DTOs
{
    public class EditUserInfoDTO
    {
        public string UserName {  get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
