


using System.ComponentModel.DataAnnotations;

namespace WebApplication3.models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }



    }
}
