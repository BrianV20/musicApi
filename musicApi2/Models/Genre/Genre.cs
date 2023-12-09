using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace musicApi2.Models.Genre
{
    public class Genre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Name cannot be more than 50 characters")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;
    }
}
