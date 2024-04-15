using AutoMapper;
using BusinessLayer.Interfaces;
using DataLayer.Repositories.Interfaces;

namespace BusinessLayer.Implementations
{
    public class BaseService<B, D> : IBaseService<B, D>
    {
        protected readonly IBaseRepository<D> _repository;
        protected readonly IMapper _mapper;

        public BaseService(IBaseRepository<D> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<B> GetByIdAsync(int id)
        {
            var content = await _repository.GetByIdAsync(id);
            return _mapper.Map<D, B>(content);
        }
        public async Task<IEnumerable<B>> GetAllAsync()
        {
            IEnumerable<D> content = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<D>, IEnumerable<B>>(content);
        }
        public async Task<bool> AddAsync(B entity)
        {
            var content = _mapper.Map<B, D>(entity);
            return await _repository.AddAsync(content);
        }
        public async Task<bool> UpdateAsync(B entity)
        {
            var content = _mapper.Map<B, D>(entity);
            return await _repository.UpdateAsync(content);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public bool IsUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri result)
                && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
        }
    }
}
