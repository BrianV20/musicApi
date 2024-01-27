using AutoMapper;
using Microsoft.EntityFrameworkCore;
using musicApi2.Models.WishList;
using musicApi2.Models.WishList.Dto;
using System.Linq.Expressions;

namespace musicApi2.Services
{
    public interface IWishListInterface
    {
        Task Create(int userId);
        Task AddRelease(int userId, int releaseId);
        Task RemoveRelease(int userId, int releaseId);
        Task Save();
        Task<WishListDto> GetOne(Expression<Func<WishList, bool>>? filter = null);

        Task<IEnumerable<WishListDto>> GetAll(Expression<Func<WishList, bool>>? filter = null);
    }
    public class WishListService : IWishListInterface
    {
        private readonly musicApiContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public WishListService(musicApiContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task AddRelease(int userId, int releaseId)
        {
            var release = _context.Releases.FirstOrDefault(r => r.Id == releaseId);
            if(release != null)
            {
                _context.WishLists.FirstOrDefault(w => w.userId == userId).releasesIds += releaseId.ToString() + ",";
                await Save();
            }
            else
            {
                throw new Exception("Release not found");
            }
        }

        public async Task Create(int userId)
        {
            var wishlist = await GetOne(w => w.userId == userId);
            if (wishlist == null)
            {
                var wishlistToAdd = new WishList
                {
                    userId = userId,
                    releasesIds = ""
                };
                _context.WishLists.Add(wishlistToAdd);
                await Save();
            }
            else
            {
                throw new Exception("Wishlist already exists for the user");
            }
        }

        public async Task RemoveRelease(int userId, int releaseId)
        {
            var wishlist = await _context.WishLists.FirstOrDefaultAsync(w => w.userId == userId);
            if(wishlist != null)
            {
                var release = _context.Releases.FirstOrDefault(r => r.Id == releaseId);
                if (release != null)
                {
                    wishlist.releasesIds = wishlist.releasesIds.Replace(release.Id.ToString() + ",", "");
                    await Save();
                }
            }
            else
            {
                throw new Exception("Release not found");
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<WishListDto> GetOne(Expression<Func<WishList, bool>>? filter = null)
        {
            if (filter != null)
            {
                var wishlist = await _context.WishLists.FirstOrDefaultAsync(filter);
                return _mapper.Map<WishListDto>(wishlist);
            }
            return null;
        }

        public async Task<IEnumerable<WishListDto>> GetAll(Expression<Func<WishList, bool>>? filter = null)
        {
            var wishlists = _context.WishLists.AsQueryable();
            if (filter != null)
            {
                wishlists = wishlists.Where(filter);
            }
            var result = await wishlists.ToListAsync();
            var wishlistsDto = _mapper.Map<IEnumerable<WishListDto>>(result);
            return wishlistsDto;
        }
    }
}
