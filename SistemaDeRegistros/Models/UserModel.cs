using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace SistemaDeRegistros.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="CPF é Obrigatório")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Nome é Obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Email é Obrigatório")]
        [EmailAddress]
        public string Email { get; set; }
        public Guid Id { get; set; }


        public bool ValidarCPF()
        {
            // Remove caracteres não numéricos
            CPF = Regex.Replace(CPF, "[^0-9]", "");

            // Verifica se o CPF tem 11 dígitos
            if (CPF.Length != 11 || CPF.All(c => c == CPF[0]))
            {
                return false;
            }

            // Cálculo dos dígitos verificadores
            int[] cpfArray = new int[11];
            for (int i = 0; i < 11; i++)
            {
                cpfArray[i] = int.Parse(CPF[i].ToString());
            }

            // Cálculo do primeiro dígito verificador
            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                sum += cpfArray[i] * (10 - i);
            }
            int firstDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);

            // Cálculo do segundo dígito verificador
            sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += cpfArray[i] * (11 - i);
            }
            int secondDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);

            // Verifica se os dígitos verificadores estão corretos
            return cpfArray[9] == firstDigit && cpfArray[10] == secondDigit;
        }
    }
}
