using System.ComponentModel.DataAnnotations;

namespace musicApi2.Models.User.Dto
{
    public class UpdateUserDto
    {

        [StringLength(50)]
        [MinLength(3)]
        public string? Username { get; set; }

        [StringLength(100)]
        [MinLength(8)]
        public string? Password { get; set; }

        [StringLength(50)]
        [MinLength(3)]
        [EmailAddress]
        public string? Email { get; set; }

        public string? Img { get; set; }

        [StringLength(50)]
        //[MinLength(3)]
        public string? Gender { get; set; }
    }
}
