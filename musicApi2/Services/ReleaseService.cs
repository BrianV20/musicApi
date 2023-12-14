using System.Linq.Expressions;
using musicApi2.Models.Release.Dto;
using musicApi2.Models.Release;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace musicApi2.Services
{
    public interface IReleaseService
    {
        Task<IEnumerable<ReleaseDto>> GetAll(Expression<Func<Release, bool>>? filter = null);


        Task<ReleaseDto> GetOne(Expression<Func<Release, bool>>? filter = null);


        Task<ReleaseDto> Add(CreateReleaseDto createReleaseDto);


        Task<ReleaseDto> Update(int id, UpdateReleaseDto updateReleaseDto);


        Task Delete(int id);


        Task Save();
    }
    public class ReleaseService : IReleaseService
    {
        private readonly musicApiContext _context;
        private readonly IMapper _mapper;

        public ReleaseService(musicApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReleaseDto> Add(CreateReleaseDto createReleaseDto)
        {
            var releaseToAdd = _mapper.Map<Release>(createReleaseDto);
            await _context.Releases.AddAsync(releaseToAdd);
            await Save();
            return _mapper.Map<ReleaseDto>(releaseToAdd);
        }

        public async Task Delete(int id)
        {
            var releaseToDelete = await _context.Releases.FirstOrDefaultAsync(r => r.Id == id);
            _context.Releases.Remove(releaseToDelete);
            await Save();
        }

        public async Task<IEnumerable<ReleaseDto>> GetAll(Expression<Func<Release, bool>>? filter = null)
        {
            var releases = _context.Releases.AsQueryable();
            if(filter != null)
            {
                releases = releases.Where(filter);
            }
            return _mapper.Map<IEnumerable<ReleaseDto>>(releases);
        }

        public async Task<ReleaseDto> GetOne(Expression<Func<Release, bool>>? filter = null)
        {
            if(filter != null)
            {
                var release = await _context.Releases.FirstOrDefaultAsync(filter);
                return _mapper.Map<ReleaseDto>(release);
            }
            return null;
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ReleaseDto> Update(int id, UpdateReleaseDto updateReleaseDto)
        {
            var releaseToUpdate = await _context.Releases.FirstOrDefaultAsync(r => r.Id == id);
            if(releaseToUpdate == null)
            {
                var release = _mapper.Map(updateReleaseDto, releaseToUpdate);
                _context.Releases.Update(release);
                await Save();
                return _mapper.Map<ReleaseDto>(release);
            }
            return null;
        }
    }
}
