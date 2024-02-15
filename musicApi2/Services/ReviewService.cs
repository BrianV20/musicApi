using AutoMapper;
using Microsoft.EntityFrameworkCore;
using musicApi2.Models.Review;
using musicApi2.Models.Review.Dto;
using System.Linq.Expressions;

namespace musicApi2.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetAll(Expression<Func<Review, bool>>? filter = null);


        Task<ReviewDto> GetOne(Expression<Func<Review, bool>>? filter = null);


        Task<ReviewDto> Add(int userId, int releaseId, string reviewText);


        Task<ReviewDto> Update(int id, UpdateReviewDto updateReviewDto);


        Task Delete(int id);


        Task Save();
    }
    public class ReviewService : IReviewService
    {
        private readonly musicApiContext _context;
        private readonly IMapper _mapper;

        public ReviewService(musicApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ReviewDto> Add(int userId, int releaseId, string reviewText)
        {
            if(await _context.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.ReleaseId == releaseId) != null) {
                throw new Exception("Ya existe un review con tal id.");
            }
            var review = new Review
            {
                UserId = userId,
                ReleaseId = releaseId,
                ReviewText = reviewText
            };
            //var review = _mapper.Map<Review>(createReviewDto);
            _context.Reviews.Add(review);
            await Save();
            return _mapper.Map<ReviewDto>(review);
        }


        public async Task Delete(int id)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            _context.Reviews.Remove(review);
            await Save();
        }


        public async Task<IEnumerable<ReviewDto>> GetAll(Expression<Func<Review, bool>>? filter = null)
        {
            var reviews = _context.Reviews.AsQueryable();
            if(filter != null)
            {
                reviews = reviews.Where(filter);
            }
            //reviews = reviews.ToListAsync();
            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }


        public async Task<ReviewDto> GetOne(Expression<Func<Review, bool>>? filter = null)
        {
            if(filter != null)
            {
                var review = await _context.Reviews.FirstOrDefaultAsync(filter);
                return _mapper.Map<ReviewDto>(review);
            }
            return null;
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<ReviewDto> Update(int id, UpdateReviewDto updateReviewDto)
        {
            var reviewToUpdate = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if(reviewToUpdate == null)
            {
                var review = _mapper.Map(updateReviewDto, reviewToUpdate);
                _context.Reviews.Update(review);
                await Save();
                return _mapper.Map<ReviewDto>(review);
            }
            return null;
        }
    }
}
