using Usuario.Models;

namespace Usuario.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Models.Usuario>> GetAllAsync();
        Task<Models.Usuario?> GetByIdAsync(int id);
        Task<Models.Usuario> AddAsync(Models.Usuario usuario);
        Task<Models.Usuario> UpdateAsync(Models.Usuario usuario);
        Task<bool> DeleteAsync(int id);
    }
}