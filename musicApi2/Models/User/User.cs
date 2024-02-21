using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicApi2.Models.User
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(3)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(100)]
        //[MinLength(8)]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(50)]
        [MinLength(3)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Img { get; set; } = null!;

        [Required]
        [StringLength(50)]
        //[MinLength(3)]
        public string Gender { get; set; } = null!;

        public string FavoriteReleases { get; set; } = null;

        public string LikedReleases { get; set; } = null;
    }
}
