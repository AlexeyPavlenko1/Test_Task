using System.ComponentModel.DataAnnotations;

namespace TestTaskAPI
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        [StringLength(255)]
        public string Login { get; set; }
        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
