namespace musicApi2.Models.Rating.Dto
{
    public class UpdateRatingDto
    {
        public string RatingValue { get; set; }

        public int ReleaseId { get; set; }

        public int UserId { get; set; }
    }
}
