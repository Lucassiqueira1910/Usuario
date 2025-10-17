using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Usuario.Data;
using Usuario.Models;

namespace Usuario.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/usuario
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var usuarios = await _context.Usuario.ToListAsync();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar usuários.", error = ex.Message });
            }
        }

        // GET: api/usuario/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario == null)
                    return NotFound(new { message = "Usuário não encontrado." });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao buscar o usuário.", error = ex.Message });
            }
        }

        // POST: api/usuario
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Dados inválidos.", errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)) });

            try
            {
                await _context.Usuario.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new { message = "Erro ao salvar o usuário no banco.", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro interno inesperado.", error = ex.Message });
            }
        }

        // PUT: api/usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Models.Usuario usuarioAtualizado)
        {
            if (id != usuarioAtualizado.Id)
                return BadRequest(new { message = "ID informado não corresponde ao usuário." });

            try
            {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario == null)
                    return NotFound(new { message = "Usuário não encontrado." });

                usuario.Nome = usuarioAtualizado.Nome;
                usuario.Email = usuarioAtualizado.Email;
                usuario.Senha = usuarioAtualizado.Senha;

                _context.Usuario.Update(usuario);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Usuário atualizado com sucesso.", usuario });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Conflito de atualização. O registro foi modificado por outro processo." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao atualizar o usuário.", error = ex.Message });
            }
        }

        // DELETE: api/usuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario == null)
                    return NotFound(new { message = "Usuário não encontrado." });

                _context.Usuario.Remove(usuario);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Usuário deletado com sucesso." });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new { message = "Erro ao excluir o usuário do banco.", error = dbEx.InnerException?.Message ?? dbEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro inesperado ao excluir usuário.", error = ex.Message });
            }
        }
    }
}
