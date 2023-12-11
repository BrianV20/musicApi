using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using musicApi2.Models.Genre;
using musicApi2.Models.Genre.Dto;

namespace musicApi2.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAll(Expression<Func<Genre, bool>>? filter = null);


        Task<GenreDto> GetOne(Expression<Func<Genre, bool>>? filter = null);


        Task<GenreDto> Add(CreateGenreDto createGenreDto);


        Task<GenreDto> Update(int id, UpdateGenreDto updateGenreDto);


        Task Delete(int id);


        Task Save();
    }
    public class GenreService : IGenreService
    {
        private readonly musicApiContext _context;
        private readonly IMapper _mapper;

        public GenreService(musicApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<GenreDto> Add(CreateGenreDto createGenreDto)
        {
            var genre = _mapper.Map<Genre>(createGenreDto);
            await _context.Genres.AddAsync(genre);
            await Save();
            return _mapper.Map<GenreDto>(genre);
        }

        public async Task Delete(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            _context.Genres.Remove(genre);
            await Save();
        }

        public async Task<IEnumerable<GenreDto>> GetAll(Expression<Func<Genre, bool>>? filter = null)
        {
            var genres = _context.Genres.AsQueryable();
            if(filter != null)
            {
                genres = genres.Where(filter);
            }
            return _mapper.Map<IEnumerable<GenreDto>>(genres);
        }

        public async Task<GenreDto> GetOne(Expression<Func<Genre, bool>>? filter = null)
        {
            if(filter != null)
            {
                var genre = await _context.Genres.FirstOrDefaultAsync(filter);
                return _mapper.Map<GenreDto>(genre);
            }
            return null;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<GenreDto> Update(int id, UpdateGenreDto updateGenreDto)
        {
            var genreToUpdate = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
            var genre = _mapper.Map(updateGenreDto, genreToUpdate);
            _context.Genres.Update(genreToUpdate);
            await Save();
            return _mapper.Map<GenreDto>(genre);
        }
    }
}
