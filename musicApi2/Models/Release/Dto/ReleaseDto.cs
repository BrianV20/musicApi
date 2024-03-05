namespace musicApi2.Models.Release.Dto
{
    public class ReleaseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Cover { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public int? ArtistId { get; set; }
        public int Type { get; set; }

        public string Genres { get; set; } = null;
    }
}
