using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_253551_Levchuk.Entities
{
    [NotMapped]
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
