using System.Net.Http.Json;

public class ViaCepService
{
    private readonly HttpClient _httpClient;

    public ViaCepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Endereco?> BuscarEnderecoPorCep(string cep)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<Endereco>($"https://viacep.com.br/ws/{cep}/json/");
            return response?.Cep != null ? response : null;
        }
        catch
        {
            return null;
        }
    }
}

public class Endereco
{
    public string Cep { get; set; }
    public string Logradouro { get; set; }
    public string Bairro { get; set; }
    public string Localidade { get; set; }
    public string Uf { get; set; }
}
