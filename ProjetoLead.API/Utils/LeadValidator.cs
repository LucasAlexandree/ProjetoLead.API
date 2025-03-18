using System.Text.RegularExpressions;

public static class LeadValidator
{
    public static bool ValidarCNPJ(string cnpj)
    {
        cnpj = Regex.Replace(cnpj, "[^0-9]", ""); 

        if (cnpj.Length != 14) return false;

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

        return cnpj.EndsWith(digito);
    }
}
