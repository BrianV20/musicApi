using AutoMapper;
using Microsoft.EntityFrameworkCore;
using musicApi2.Models.Rating;
using musicApi2.Models.Rating.Dto;
using System.Linq.Expressions;

namespace musicApi2.Services
{
    public interface IRatingInterface
    {
        Task<IEnumerable<RatingDto>> GetAll(Expression<Func<Rating, bool>>? filter = null);


        Task<RatingDto> GetOne(Expression<Func<Rating, bool>>? filter = null);


        Task<RatingDto> Add(CreateRatingDto createRatingDto);


        Task<RatingDto> Update(int id, UpdateRatingDto updateRatingDto);


        Task Delete(int id);

        Task Save();
    }
    public class RatingService : IRatingInterface
    {
        private readonly musicApiContext _context;
        private readonly IMapper _mapper;

        public RatingService(musicApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RatingDto> Add(CreateRatingDto createRatingDto)
        {
            var ratingToAdd = _mapper.Map<Rating>(createRatingDto);
            await _context.Ratings.AddAsync(ratingToAdd);
            await Save();
            return _mapper.Map<RatingDto>(ratingToAdd);
        }

        public async Task Delete(int id)
        {
            var ratingToDelete = await _context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
            _context.Ratings.Remove(ratingToDelete);
            await Save();
        }

        public async Task<IEnumerable<RatingDto>> GetAll(Expression<Func<Rating, bool>>? filter = null)
        {
            var ratings = _context.Ratings.AsQueryable();
            if(filter != null)
            {
                ratings = ratings.Where(filter);
            }
            return _mapper.Map<IEnumerable<RatingDto>>(ratings);
        }

        public async Task<RatingDto> GetOne(Expression<Func<Rating, bool>>? filter = null)
        {
            if(filter != null)
            {
                var rating = await _context.Ratings.FirstOrDefaultAsync(filter);
                return _mapper.Map<RatingDto>(rating);
            }
            return null;

        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<RatingDto> Update(int id, UpdateRatingDto updateRatingDto)
        {
            var ratingToUpdate = await _context.Ratings.FirstOrDefaultAsync(r => r.Id == id);
            var rating = _mapper.Map(updateRatingDto, ratingToUpdate);
            _context.Ratings.Update(rating);
            await Save();
            return _mapper.Map<RatingDto>(rating);
        }
    }
}
