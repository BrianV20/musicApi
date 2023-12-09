namespace musicApi2.Models.Review.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }

        public string ReviewText { get; set; } = null!;

        public int ReleaseId { get; set; }

        public int UserId { get; set; }
    }
}
