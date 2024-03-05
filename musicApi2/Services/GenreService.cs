using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using musicApi2.Models.Genre;
using musicApi2.Models.Genre.Dto;
using musicApi2.Models.Release;
using musicApi2.Models.Release.Dto;

namespace musicApi2.Services
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreDto>> GetAll(Expression<Func<Genre, bool>>? filter = null);


        Task<GenreDto> GetOne(Expression<Func<Genre, bool>>? filter = null);


        Task<GenreDto> Add(CreateGenreDto createGenreDto);


        Task<GenreDto> Update(int id, UpdateGenreDto updateGenreDto);

        Task<ReleaseDto> updateGenresOfRelease(int releaseId, string genresIds);

        Task<ReleaseDto> clearGenresOfRelease(int releaseId);

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
            if(genreToUpdate != null)
            {
                var genre = _mapper.Map(updateGenreDto, genreToUpdate);
                _context.Genres.Update(genreToUpdate);
                await Save();
                return _mapper.Map<GenreDto>(genre);
            }
            return null;
        }

        public async Task<ReleaseDto> updateGenresOfRelease(int releaseId, string genresIds)
        {
            var releaseToUpdate = await _context.Releases.FirstOrDefaultAsync(r => r.Id == releaseId);
            if (releaseToUpdate != null)
            {
                var genres = genresIds.Split(',').Select(s => s.Trim());
                var existingGenres = releaseToUpdate.Genres.Split(',').Select(s => s.Trim());

                foreach (var genreId in genres)
                {
                    var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == int.Parse(genreId));
                    if (genre != null)
                    {
                        if (!existingGenres.Contains(genreId))
                        {
                            if (releaseToUpdate.Genres == "")
                            {
                                releaseToUpdate.Genres += (genreId + ",");
                            }
                            else
                            {
                                releaseToUpdate.Genres += (" " + genreId + ",");
                            }
                        }
                    }
                }

                await Save();
                return _mapper.Map<ReleaseDto>(releaseToUpdate);
            }
            throw new Exception("El release no existe");
        }

        public async Task<ReleaseDto> clearGenresOfRelease(int releaseId)
        {
            var releaseToUpdate = await _context.Releases.FirstOrDefaultAsync(r => r.Id == releaseId);
            if(releaseToUpdate != null)
            {
                releaseToUpdate.Genres = "";
                await Save();
                return _mapper.Map<ReleaseDto>(releaseToUpdate);
            }
            throw new Exception("El release no existe");
        }

    }
}
