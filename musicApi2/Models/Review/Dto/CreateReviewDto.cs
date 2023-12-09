namespace musicApi2.Models.Review.Dto
{
    public class CreateReviewDto
    {
        public string ReviewText { get; set; } = null!;

        public int ReleaseId { get; set; }

        public int UserId { get; set; }
    }
}
