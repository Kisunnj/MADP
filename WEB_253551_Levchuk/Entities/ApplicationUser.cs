using Microsoft.AspNetCore.Identity;

namespace WEB_253551_Levchuk.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public byte[]? Image { get; set; }
        public string? ContentType { get; set; }
        public void InitializeArray(int length)
        {
            Image = new byte[length];
        }
    }
}
