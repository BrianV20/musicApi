﻿namespace musicApi2.Models.Rating.Dto
{
    public class RatingDto
    {
        public int Id { get; set; }

        public double RatingValue { get; set; }

        public int ReleaseId { get; set; }

        public int UserId { get; set; }
    }
}
