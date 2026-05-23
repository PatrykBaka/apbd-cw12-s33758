using cw12.DTOs;

namespace cw12.Services;

public interface IDbService
{
    
    Task<List<GetPatiensOptional>> GetPatientsListAsync(string? search);
    
}