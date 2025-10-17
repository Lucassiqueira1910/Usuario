using Usuario.UsuarioDTOs;
using Usuario.Interfaces;
using Usuario.Models;
using Usuario.UsuarioDTOs;

namespace Usuario.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.Usuario>> ListarTodosAsync() =>
            await _repository.GetAllAsync();

        public async Task<Models.Usuario?> BuscarPorIdAsync(int id) =>
            await _repository.GetByIdAsync(id);

        public async Task<Models.Usuario> CadastrarAsync(UsuarioDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Nome))
                throw new Exception("Nome não pode estar vazio.");

            var usuario = new Models.Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };

            return await _repository.AddAsync(usuario);
        }

        public async Task<Models.Usuario> AtualizarAsync(int id, UsuarioDTO dto)
        {
            var usuarioExistente = await _repository.GetByIdAsync(id)
                                   ?? throw new Exception("Usuário não encontrado.");

            usuarioExistente.Nome = dto.Nome;
            usuarioExistente.Email = dto.Email;
            usuarioExistente.Senha = dto.Senha;

            return await _repository.UpdateAsync(usuarioExistente);
        }

        public async Task<bool> DeletarAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}