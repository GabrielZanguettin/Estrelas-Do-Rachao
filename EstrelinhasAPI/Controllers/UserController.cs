using EstrelinhasAPI.Entidades;
using EstrelinhasAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstrelinhasAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await userService.GetUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = "Erro ao encontrar usuários", error = ex.Message });
            }
        }

        [HttpPost("users")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            try
            {
                var createdUser = await userService.CreateUser(user);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao criar usuário", error = ex.Message });
            }
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var deletedUser = await userService.DeleteUser(id);
                return Ok(deletedUser);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = "Erro ao deletar usuário", error = ex.Message });
            }
        }

        [HttpPatch("users/{id}/add-star")]
        public async Task<IActionResult> AddStar(int id)
        {
            try
            {
                var updatedUser = await userService.AddStar(id);
                if (updatedUser == null)
                    return NotFound(new { message = "Usuário não encontrado" });

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao adicionar estrela", error = ex.Message });
            }
        }
    }
}
