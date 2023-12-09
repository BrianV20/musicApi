namespace musicApi2.Models.User.Dto
{
    public class UsersDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Img { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
