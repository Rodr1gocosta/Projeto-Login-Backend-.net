using Sistema_Login.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Sistema_Login.Services;
using FluentResults;
using Sistema_Login.Data.Requests;

namespace Sistema_Login.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private CadastroService _cadastroService;

        public CadastroController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public IActionResult CadastrarUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = _cadastroService.CadastrarUsuario(createDto);
            if (resultado.IsFailed)
            {
                return StatusCode(500);
            }
            else
            {
                return Ok(resultado.Successes);
            }
        }

        [HttpGet("/ativa")]
        public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest request)
        {
            Result resultado = _cadastroService.AtivaContaUsuario(request);
            if (resultado.IsFailed)
            {
                return StatusCode(500);
            }
            else
            {
                return Ok(resultado.Successes);
            }
        }

    }
}
