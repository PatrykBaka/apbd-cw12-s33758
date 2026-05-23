namespace cw12.DTOs;

public class GetPatiensOptional
{
    public string Pesel { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Sex { get; set; } =  string.Empty;
    public List<GetAdmissions> Admissions { get; set; } = [];
    public List<GetBedAssigments> BedAssigments { get; set; } = [];
}

public class GetAdmissions
{
    public int  Id { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; }
    public GetWard Ward { get; set; } = null!;
}

public class GetWard
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class GetBedAssigments
{
    public int  Id { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; }
    public GetBed Bed { get; set; } = null!;
}

public class GetBed
{
    public int Id { get; set; }
    public GetBedType BedType { get; set; } = null!;
    public GetRoom Room { get; set; } = null!;
}

public class GetBedType
{
    public int  Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class GetRoom
{
    public string Id { get; set; } = string.Empty;
    public bool HasTv { get; set; }
    public GetWard Ward { get; set; } = null!;
}