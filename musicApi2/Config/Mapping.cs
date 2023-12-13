using AutoMapper;
using musicApi2.Models.Artist;
using musicApi2.Models.Release;
using musicApi2.Models.Artist.Dto;
using musicApi2.Models.Genre;
using musicApi2.Models.Genre.Dto;
using musicApi2.Models.Rating;
using musicApi2.Models.Rating.Dto;
using musicApi2.Models.Release.Dto;
using musicApi2.Models.Review;
using musicApi2.Models.Review.Dto;
using musicApi2.Models.User;
using musicApi2.Models.User.Dto;

namespace musicApi2.Config
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            // User
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            //CreateMap<User, UsersDto>().ReverseMap();

            // Review
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<Review, CreateReviewDto>().ReverseMap();
            CreateMap<Review, UpdateReviewDto>().ReverseMap();
            //CreateMap<Review, ReviewsDto>().ReverseMap();

            // Release
            CreateMap<Release, ReleaseDto>().ReverseMap();
            CreateMap<Release, CreateReleaseDto>().ReverseMap();
            CreateMap<Release, UpdateReleaseDto>().ReverseMap();

            // Rating
            CreateMap<Rating, RatingDto>().ReverseMap();
            CreateMap<Rating, CreateRatingDto>().ReverseMap();
            CreateMap<Rating, UpdateRatingDto>().ReverseMap();

            // Genre
            CreateMap<Genre, GenreDto>().ReverseMap();
            CreateMap<Genre, CreateGenreDto>().ReverseMap();
            CreateMap<Genre, UpdateGenreDto>().ReverseMap();

            // Artist
            CreateMap<Artist, ArtistDto>().ReverseMap();
            CreateMap<Artist, CreateArtistDto>().ReverseMap();
            CreateMap<Artist, UpdateArtistDto>().ReverseMap();
        }
    }
}
