namespace SistemaDeRegistros.Models
{
    public class UserModel
    {
        public int CPF { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public Guid Id { get; set; }
    }
}
