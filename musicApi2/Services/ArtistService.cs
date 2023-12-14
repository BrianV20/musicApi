using AutoMapper;
using Microsoft.EntityFrameworkCore;
using musicApi2.Models.Artist;
using musicApi2.Models.Artist.Dto;
using System.Linq.Expressions;

namespace musicApi2.Services
{
    public interface IArtistInterface
    {
        Task<IEnumerable<ArtistDto>> GetAll(Expression<Func<Artist, bool>>? filter = null);


        Task<ArtistDto> GetOne(Expression<Func<Artist, bool>>? filter = null);


        Task<ArtistDto> Add(CreateArtistDto createArtistDto);


        Task<ArtistDto> Update(int id, UpdateArtistDto updateArtistDto);


        Task Delete(int id);

        Task Save();
    }

    public class ArtistService : IArtistInterface
    {
        private readonly musicApiContext _context;
        private readonly IMapper _mapper;

        public ArtistService(musicApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<ArtistDto> Add(CreateArtistDto createArtistDto) 
        {
            var artistToAdd = _mapper.Map<Artist>(createArtistDto);
            await _context.Artists.AddAsync(artistToAdd);
            await Save();
            return _mapper.Map<ArtistDto>(artistToAdd);
        }


        public async Task Delete(int id) 
        {
            var artistToDelete = _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            _context.Artists.Remove(await artistToDelete);
            await Save();
        }


        public async Task<ArtistDto> GetOne(Expression<Func<Artist, bool>>? filter = null)
        {
            if(filter != null)
            {
                var artist = await _context.Artists.FirstOrDefaultAsync(filter);
                return _mapper.Map<ArtistDto>(artist);
            }
            return null;
        }


        public async Task<IEnumerable<ArtistDto>> GetAll(Expression<Func<Artist, bool>>? filter = null)
        {
            var artists = _context.Artists.AsQueryable();
            if(filter != null)
            {
                artists = artists.Where(filter);
            }
            return _mapper.Map<IEnumerable<ArtistDto>>(artists);
        }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<ArtistDto> Update(int id, UpdateArtistDto updateArtistDto) 
        {
            var artistToUpdate = await _context.Artists.FirstOrDefaultAsync(a => a.Id == id);
            if(artistToUpdate != null)
            {
                var artist = _mapper.Map(updateArtistDto, artistToUpdate);
                _context.Artists.Update(artist);
                await Save();
                return _mapper.Map<ArtistDto>(artist);
            }
            return null;
        }
    }
}
