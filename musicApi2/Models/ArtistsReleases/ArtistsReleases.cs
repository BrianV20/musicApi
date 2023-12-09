using musicApi.Models.Artist;
using musicApi.Models.Release;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace musicApi2.Models.ArtistsReleases
{
    public class ArtistsReleases
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        public int? ArtistId { get; set; }

        //[Required]
        public int? ReleaseId { get; set; }

        [ForeignKey("ArtistId")]
        //[InverseProperty("ArtistsReleases")]
        public virtual Artist Artist { get; set; } = null!;

        [ForeignKey("ReleaseId")]
        //[InverseProperty("ArtistsReleases")]
        public virtual Release Release { get; set; } = null!;
        //public object Artists { get; internal set; }
    }
}
