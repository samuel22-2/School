using Microsoft.Build.Framework;

namespace WebApplication3.models
{
    public class ChangePasswordcs
    {
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
