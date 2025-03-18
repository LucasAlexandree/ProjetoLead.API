using System.Net.Http.Json;
using ProjetoLead.API.Models;

public class LeadService
{
    private readonly HttpClient _httpClient;

    public LeadService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LeadDto>> GetLeads()
    {
        var leads = await _httpClient.GetFromJsonAsync<List<Lead>>("api/Lead");

        // Converter Lead para LeadDto
        return leads.Select(l => new LeadDto
        {
            Id = l.Id,
            Cnpj = l.Cnpj,
            RazaoSocial = l.RazaoSocial,
            Cep = l.Cep,
            Endereco = l.Endereco,
            Numero = l.Numero,
            Complemento = l.Complemento,
            Bairro = l.Bairro,
            Cidade = l.Cidade,
            Estado = l.Estado
        }).ToList();
    }


    public async Task<LeadDto> GetLead(int id)
    {
        var lead = await _httpClient.GetFromJsonAsync<Lead>($"api/Lead/{id}");

        return new LeadDto
        {
            Id = lead.Id,
            Cnpj = lead.Cnpj,
            RazaoSocial = lead.RazaoSocial,
            Cep = lead.Cep,
            Endereco = lead.Endereco,
            Numero = lead.Numero,
            Complemento = lead.Complemento,
            Bairro = lead.Bairro,
            Cidade = lead.Cidade,
            Estado = lead.Estado
        };
    }

    public async Task CreateLead(LeadDto leadDto)
    {
        var response = await _httpClient.PostAsJsonAsync("api/Lead", leadDto);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao cadastrar lead: {error}");
        }
    }

    public async Task UpdateLead(LeadDto leadDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Lead/{leadDto.Id}", leadDto);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao atualizar lead: {error}");
        }
    }

    public async Task<bool> DeleteLead(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/Lead/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao excluir lead: {error}");
        }
        return response.IsSuccessStatusCode;
    }



}
