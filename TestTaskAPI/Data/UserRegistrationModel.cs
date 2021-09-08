using System.ComponentModel.DataAnnotations;

namespace TestTaskAPI.Data
{
    public class UserRegistrationModel
    {

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Login { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [StringLength(255)]
        [Compare((nameof(Password)), ErrorMessage = "Passwords do not match")]
        public string PasswordConfirmation { get; set; }
    }
}
