using musicApi2.Models.ArtistsReleases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicApi.Models.Release
{
    public class Release
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "El título es requerido")]
        public string Title { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "La portada es requerida")]
        public string Cover { get; set; }

        [Required(ErrorMessage = "La fecha de lanzamiento es requerida")]
        public string ReleaseDate { get; set; }

        [ForeignKey("ArtistId")]
        public virtual Artist.Artist Artist { get; set; }

        //[Required]
        public int? ArtistId { get; set; }

        [Required]
        public int Type { get; set; }

        public virtual ICollection<ArtistsReleases> ArtistsReleases { get; set; }
    }
}
