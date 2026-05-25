using cw12.DTOs;
using cw12.Exceptions;
using cw12.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw12.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    
    private readonly IDbService _dbService;

    public PatientsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPatients([FromQuery] string? search)
    {
        var result = await _dbService.GetPatientsListAsync(search);
        return Ok(result);
    }

    [Route("api/patients/{pesel}/bedassignments")]
    [HttpPost]
    public async Task<IActionResult> AddBedToPatientAsync(string pesel, AddAssigmentBedRequest dto)
    {
        try
        {
            await _dbService.AddBedToPatientAsync(pesel, dto);

            return Created();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
}