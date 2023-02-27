using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeBudget.API.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTimeOffset Date { get; set; }

        [Required]
        public User User { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = default!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsSettled { get; set; }
    }
}