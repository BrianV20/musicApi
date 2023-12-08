using musicApi.Models.Release;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicApi2.Models.Rating
{
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Range(minimum: 0, maximum: 5)]
        public int RatingValue { get; set; }

        [Required]
        public int ReleaseId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("ReleaseId")]
        public virtual Release Release { get; set; }

        [ForeignKey("UserId")]
        public virtual User.User User { get; set; }
    }
}
