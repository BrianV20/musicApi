using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicApi2.Models.Review
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(300)]
        public string ReviewText { get; set; } = null!;

        [Required]
        public int ReleaseId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("ReleaseId")]
        public virtual Release.Release Release { get; set; } = null!;

        [ForeignKey("UserId")]
        public virtual User.User User { get; set; } = null!;
    }
}
