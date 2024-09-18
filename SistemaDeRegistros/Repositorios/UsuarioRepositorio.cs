using Microsoft.EntityFrameworkCore;
using SistemaDeRegistros.Data;
using SistemaDeRegistros.Models;
using SistemaDeRegistros.Repositorios.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SistemaDeRegistros.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly SistemaTarefasDBContext _dbContext;

        public UsuarioRepositorio(SistemaTarefasDBContext sistemaTarefasDBContext)
        {
            _dbContext = sistemaTarefasDBContext;
        }

        public async Task<UserModel> BuscarPorId(Guid id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<UserModel>> BuscarTodosUsuarios()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UserModel> Adicionar(UserModel usuario)
        {
           usuario.Id = Guid.NewGuid();
           await  _dbContext.Usuarios.AddAsync(usuario);
           await  _dbContext.SaveChangesAsync();
            return usuario;
        }


        public async Task<UserModel> Atualizar(UserModel usuario, Guid id)
        {
            UserModel usuarioPorId = await BuscarPorId(id);
            if(usuarioPorId == null)
            {
                throw new Exception($"Usuário para o CPF: {id} não foi localizado");
            }
            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.CPF = usuario.CPF;
            
            _dbContext.Usuarios.Update(usuarioPorId);
            await _dbContext.SaveChangesAsync();

            return usuarioPorId;
        }

        public async Task<bool> Deletar(Guid id)
        {
            UserModel usuarioPorCPF = await BuscarPorId(id);
            if (usuarioPorCPF == null)
            {
                throw new Exception($"Usuário para o CPF: {id} não foi localizado");
            }
            _dbContext.Usuarios.Remove(usuarioPorCPF);
            await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
