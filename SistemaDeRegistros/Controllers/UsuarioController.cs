using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaDeRegistros.Repositorios.Interfaces;

namespace SistemaDeRegistros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Models.UserModel>>> BuscarTodosClientes()
        {
            List<Models.UserModel> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Models.UserModel>>> BuscarPorCPF(Guid id)
        {
            Models.UserModel usuario = await _usuarioRepositorio.BuscarPorId(id);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Models.UserModel>> Cadastrar([FromBody] Models.UserModel usuario)
        {
          var success = await _usuarioRepositorio.Adicionar(usuario);
            return Ok(usuario);
        }

        [HttpPut]
        public async Task<ActionResult<Models.UserModel>> Atualizar([FromBody] Models.UserModel usuario)
        {
            Models.UserModel response = await _usuarioRepositorio.Atualizar(usuario, usuario.Id);
            return Ok(usuario);
        }

        [HttpDelete]
        public async Task<ActionResult<Models.UserModel>> Deletar([FromBody] Models.UserModel usuario, Guid Id)
        {
            bool success = await _usuarioRepositorio.Deletar(usuario.Id);
            return Ok();
        }
    }
}
