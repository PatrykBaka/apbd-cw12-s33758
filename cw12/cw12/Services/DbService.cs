using cw12.Data;
using cw12.DTOs;
using cw12.Exceptions;
using cw12.Models;
using Microsoft.EntityFrameworkCore;

namespace cw12.Services;

public class DbService : IDbService
{
    private readonly MasterContext _dbContext;

    public DbService(MasterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GetPatiensOptional>> GetPatientsListAsync(string? search)
    {
        
        var query = _dbContext.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => EF.Functions.Like(p.FirstName, $"%{search}%")
                                           || EF.Functions.Like(p.LastName, $"%{search}%"));
        }
        
        var result = await query.Select(p => new GetPatiensOptional
            {
                Pesel = p.Pesel,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Age = p.Age,
                Sex = p.Sex ? "Male" : "Female",
                Admissions = p.Admissions.Select(a => new GetAdmissions
                {
                    Id = a.Id,
                    AdmissionDate = a.AdmissionDate,
                    DischargeDate =  a.DischargeDate,
                    Ward = new GetWard
                    {
                        Id = a.Ward.Id,
                        Name = a.Ward.Name,
                        Description = a.Ward.Description,
                    }
                }).ToList(),
                BedAssigments = p.BedAssignments.Select(b => new GetBedAssigments
                {
                    Id = b.Id,
                    From = b.From,
                    To = b.To,
                    Bed = new GetBed
                    {
                        Id = b.Bed.Id,
                        BedType = new GetBedType
                        {
                            Id = b.Bed.BedType.Id,
                            Name = b.Bed.BedType.Name,
                            Description = b.Bed.BedType.Description
                        },
                        Room = new GetRoom
                        {
                            Id = b.Bed.Room.Id,
                            HasTv =  b.Bed.Room.HasTv,
                            Ward = new GetWard
                            {
                                Id = b.Bed.Room.Ward.Id,
                                Name = b.Bed.Room.Ward.Name,
                                Description = b.Bed.Room.Ward.Description
                            }
                        }
                    }
                }).ToList()
            }).ToListAsync();
        
        return result;
    }
    
}