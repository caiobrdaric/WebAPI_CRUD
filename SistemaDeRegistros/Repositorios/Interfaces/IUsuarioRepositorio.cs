namespace SistemaDeRegistros.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {

        Task<List<Models.UserModel>> BuscarTodosUsuarios();
        
        Task<Models.UserModel> BuscarPorId(Guid id);
        
        Task<Models.UserModel> Adicionar(Models.UserModel usuario);
        
        Task<Models.UserModel> Atualizar(Models.UserModel usuario, Guid id);
        
        Task<bool> Deletar(Guid id);
    }   
}
