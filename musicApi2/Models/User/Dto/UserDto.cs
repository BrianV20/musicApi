namespace musicApi2.Models.User.Dto
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Img { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string FavoriteReleases { get; set; } = null!;

        public string LikedReleases { get; set; } = null!;
    }
}
