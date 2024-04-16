namespace LinkShortening.Business.Interfaces
{
    public interface IBaseService<B, D>
    {
        Task<B> GetByIdAsync(int id);
        Task<IEnumerable<B>> GetAllAsync();
        Task<bool> AddAsync(B entity);
        Task<bool> UpdateAsync(B entity);
        Task<bool> DeleteAsync(int id);
       
    }
}
