using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBudget.API.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(70)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(128)]
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        
        [Required]
        public bool IsContributor { get; set; }
    }
}