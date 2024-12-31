using ControleDeGastosAPI.Models;
using ControleDeGastosAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET api/user/{email}
        [HttpGet("{email}")]
        public async Task<IActionResult> GetAsyncByEmail([FromRoute] string email)
        {
            try
            {
                var user = await _userRepository.GetAsyncByEmail(email);

                if (user == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao buscar usuário: {ex.Message}" });
            }
        }

        // POST api/user/login
        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] string[] credentials)
        {
            try
            {
                if (credentials == null || credentials.Length != 2)
                {
                    return BadRequest(new { message = "Email e senha são obrigatórios." });
                }

                var email = credentials[0];
                var password = credentials[1];

                var user = await _userRepository.AuthenticateAsync(email, password);

                if (user == null)
                {
                    return Unauthorized(new { message = "Email ou senha inválidos." });
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao autenticar usuário: {ex.Message}" });
            }
        }

        // POST api/user
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest(new { message = "Dados do usuário são obrigatórios." });
                }

                var createdUser = await _userRepository.CreateAsync(user);
                return CreatedAtAction(nameof(GetAsyncByEmail), new { email = createdUser.Email }, createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao criar usuário: {ex.Message}" });
            }
        }

        // PUT api/user/{UUID}
        [HttpPut("{UUID}")]
        public async Task<IActionResult> UpdateAsync([FromBody] User user, [FromRoute] string UUID)
        {
            try
            {
                var updatedUser = await _userRepository.UpdateAsync(user, UUID);

                if (updatedUser == null)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao atualizar usuário: {ex.Message}" });
            }
        }

        // DELETE api/user/{UUID}
        [HttpDelete("{UUID}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string UUID)
        {
            try
            {
                var isDeleted = await _userRepository.DeleteAsync(UUID);

                if (!isDeleted)
                {
                    return NotFound(new { message = "Usuário não encontrado." });
                }

                return NoContent(); // Status 204 - Sucesso sem conteúdo
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro ao excluir usuário: {ex.Message}" });
            }
        }
    }
}
