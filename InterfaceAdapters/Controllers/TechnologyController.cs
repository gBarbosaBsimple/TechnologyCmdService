using Application.DTO;
using Application.IServices;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterfaceAdapters.Controllers;

[Route("api/technologies")]
[ApiController]
public class TechnologyController : ControllerBase
{
    private readonly ITechnologyService _technologyService;

    public TechnologyController(ITechnologyService technologyService)
    {
        _technologyService = technologyService;
    }

    // US‑XX: Como Gestor de Projeto, quero criar uma Tecnologia (descrição)
    // POST api/technologies
    [HttpPost]
    public async Task<ActionResult<TechnologyDTO>> AddTechnology([FromBody] AddTechnologyDTO dto)
    {
        var result = await _technologyService.AddTechnologyAsync(dto);
        return result.ToActionResult();
    }
}
