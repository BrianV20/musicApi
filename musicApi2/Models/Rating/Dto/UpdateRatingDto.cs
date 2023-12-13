namespace musicApi2.Models.Rating.Dto
{
    public class UpdateRatingDto
    {
        public int RatingValue { get; set; }

        public int ReleaseId { get; set; }

        public int UserId { get; set; }
    }
}
