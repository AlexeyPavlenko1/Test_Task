using System.ComponentModel.DataAnnotations;

namespace TestTaskAPI.Controllers
{
    public class UserCredsModel
    {

        [Required]
        [StringLength(255)]
        public string Login { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}