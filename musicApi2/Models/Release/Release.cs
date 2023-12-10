using musicApi2.Models.ArtistsReleases;
using musicApi2.Models.ReleasesGenres;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicApi2.Models.Release
{
    public class Release
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "El título es requerido")]
        public string Title { get; set; } = null!;

        [MaxLength(100)]
        [Required(ErrorMessage = "La portada es requerida")]
        public string Cover { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de lanzamiento es requerida")]
        public string ReleaseDate { get; set; } = null!;

        [ForeignKey("ArtistId")]
        public virtual Artist.Artist Artist { get; set; } = null!;

        //[Required]
        public int? ArtistId { get; set; }

        [Required]
        public int Type { get; set; }

        public virtual ICollection<ArtistsReleases.ArtistsReleases> ArtistsReleases { get; set; } = null!;

        public virtual ICollection<ReleasesGenres.ReleasesGenres> ReleasesGenres { get; set; } = null!;
    }
}
