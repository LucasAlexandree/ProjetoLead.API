using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class LeadDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O CNPJ é obrigatório.")]
    [CustomValidation(typeof(LeadDto), nameof(ValidarCNPJ))]
    public string Cnpj { get; set; }

    [Required(ErrorMessage = "A Razão Social é obrigatória.")]
    public string RazaoSocial { get; set; }

    [Required(ErrorMessage = "O CEP é obrigatório.")]
    public string Cep { get; set; }

    [Required(ErrorMessage = "O Endereço é obrigatório.")]
    public string Endereco { get; set; }

    [Required(ErrorMessage = "O Número do Endereço é obrigatório.")]
    public string Numero { get; set; }

    public string Complemento { get; set; }

    [Required(ErrorMessage = "O Bairro é obrigatório.")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "A Cidade é obrigatória.")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "O Estado é obrigatório.")]
    public string Estado { get; set; }

    public static ValidationResult ValidarCNPJ(string cnpj, ValidationContext context)
    {
        if (string.IsNullOrEmpty(cnpj))
        {
            return new ValidationResult("O CNPJ é obrigatório.");
        }

        cnpj = Regex.Replace(cnpj, "[^0-9]", "");

        if (cnpj.Length != 14)
        {
            return new ValidationResult("CNPJ inválido.");
        }

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int soma = 0, resto;
        string digito, tempCnpj;

        tempCnpj = cnpj.Substring(0, 12);
        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        resto = soma % 11;
        resto = (resto < 2) ? 0 : 11 - resto;
        digito = resto.ToString();
        tempCnpj += digito;

        soma = 0;
        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        resto = (resto < 2) ? 0 : 11 - resto;
        digito += resto.ToString();

        if (!cnpj.EndsWith(digito))
        {
            return new ValidationResult("CNPJ inválido.");
        }

        return ValidationResult.Success;
    }
}
