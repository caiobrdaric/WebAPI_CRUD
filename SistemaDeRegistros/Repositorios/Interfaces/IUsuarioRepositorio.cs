namespace SistemaDeRegistros.Repositorios.Interfaces
{
    public interface IUsuarioRepositorio
    {

        Task<List<Models.UserModel>> BuscarTodosUsuarios();
        
        Task<Models.UserModel> BuscarPorCPF(int id);
        
        Task<Models.UserModel> Adicionar(Models.UserModel usuario);
        
        Task<Models.UserModel> Atualizar(Models.UserModel usuario, int id);
        
        Task<bool> Apagar(int id);
    }   
}
