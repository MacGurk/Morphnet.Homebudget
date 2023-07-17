using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeBudget.API.Models;
using HomeBudget.API.Models.User;

namespace HomeBudget.API.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = default!;

        [Required]
        [MaxLength(70)]
        public string Email { get; set; } = default!;

        [Required]
        [MaxLength(128)]
        public byte[] PasswordHash { get; set; } = default!;

        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt { get; set; } = default!;
    }
}