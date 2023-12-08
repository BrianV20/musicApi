using musicApi.Models.Release;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicApi2.Models.ReleasesGenres
{
    public class ReleasesGenres
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ReleaseId { get; set; }

        [Required]
        public int GenreId { get; set; }

        [ForeignKey("ReleaseId")]
        public virtual Release Release { get; set; }

        [ForeignKey("GenreId")]
        public virtual Genre.Genre Genre { get; set; }
    }
}
