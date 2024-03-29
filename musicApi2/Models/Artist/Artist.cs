﻿using Microsoft.EntityFrameworkCore;
using musicApi2.Models.ArtistsReleases;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicApi2.Models.Artist
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; } = null!;

        public virtual ICollection<ArtistsReleases.ArtistsReleases> ArtistsReleases { get; set; } = null!;

        [Required]
        public string Img { get; set; } = null!;
    }
}
