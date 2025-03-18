using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoLead.API.Data;
using ProjetoLead.API.Models;

[Route("api/[controller]")]
[ApiController]
public class LeadController : ControllerBase
{
    private readonly ProjetoLeadDbContext _context;
    private readonly ViaCepService _viaCepService;
    public LeadController(ProjetoLeadDbContext context, ViaCepService viaCepService) 
    {
        _context = context;
        _viaCepService = viaCepService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Lead>>> GetLeads()
    {
        return await _context.CadastroLead.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Lead>> GetLead(int id)
    {
        var lead = await _context.CadastroLead.FindAsync(id);
        if (lead == null)
            return NotFound();

        return lead;
    }

    [HttpPost]
    public async Task<ActionResult<Lead>> PostLead(Lead lead)
    {
        
        if (!LeadValidator.ValidarCNPJ(lead.Cnpj))
            return BadRequest("CNPJ inválido.");

        
        if (string.IsNullOrWhiteSpace(lead.RazaoSocial))
            return BadRequest("A Razão Social é obrigatória.");

        if (string.IsNullOrWhiteSpace(lead.Cep))
            return BadRequest("O CEP é obrigatório.");

        if (string.IsNullOrWhiteSpace(lead.Numero))
            return BadRequest("O Número do Endereço é obrigatório.");

        
        var endereco = await _viaCepService.BuscarEnderecoPorCep(lead.Cep);

        if (endereco != null)
        {
            lead.Endereco = endereco.Logradouro;
            lead.Bairro = endereco.Bairro;
            lead.Cidade = endereco.Localidade;
            lead.Estado = endereco.Uf;
        }
        else
        {
            
            if (string.IsNullOrWhiteSpace(lead.Endereco) ||
                string.IsNullOrWhiteSpace(lead.Bairro) ||
                string.IsNullOrWhiteSpace(lead.Cidade) ||
                string.IsNullOrWhiteSpace(lead.Estado))
            {
                return BadRequest("Endereço incompleto. Preencha os campos manualmente.");
            }
        }

        _context.CadastroLead.Add(lead);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetLead), new { id = lead.Id }, lead);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> PutLead(int id, Lead lead)
    {
        if (id != lead.Id)
            return BadRequest();

        _context.Entry(lead).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLead(int id)
    {
        var lead = await _context.CadastroLead.FindAsync(id);
        if (lead == null)
            return NotFound();

        _context.CadastroLead.Remove(lead);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
