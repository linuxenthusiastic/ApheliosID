namespace ApheliosID.Core.Models;

/// <summary>
/// Credencial profesional (certificados de empleo, experiencia laboral)
/// </summary>
public class ProfessionalCredential : VerifiableCredential
{
    public string Company { get; private set; }
    public string Position { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public List<string> Responsibilities { get; private set; }

    public ProfessionalCredential(
        string issuer,
        string subject,
        string company,
        string position,
        DateTime startDate,
        DateTime? endDate = null,
        List<string>? responsibilities = null,
        DateTime? expiresAt = null)
        : base(issuer, subject, expiresAt)
    {
        Company = company ?? throw new ArgumentNullException(nameof(company));
        Position = position ?? throw new ArgumentNullException(nameof(position));
        StartDate = startDate;
        EndDate = endDate;
        Responsibilities = responsibilities ?? new List<string>();
    }

    public override string GetCredentialType()
    {
        return "ProfessionalCredential";
    }

    public override bool ValidateSpecificClaims()
    {
        if (string.IsNullOrWhiteSpace(Company)) return false;
        if (string.IsNullOrWhiteSpace(Position)) return false;
        if (StartDate > DateTime.UtcNow) return false;
        if (EndDate.HasValue && EndDate < StartDate) return false; 
        
        return true;
    }

    public override Dictionary<string, object> GetCredentialInfo()
    {
        var info = base.GetCredentialInfo();
        info["company"] = Company;
        info["position"] = Position;
        info["startDate"] = StartDate;
        info["endDate"] = EndDate ?? (object)"current";
        info["yearsOfExperience"] = CalculateYearsOfExperience();
        info["responsibilitiesCount"] = Responsibilities.Count;
        
        return info;
    }

    private double CalculateYearsOfExperience()
    {
        var end = EndDate ?? DateTime.UtcNow;
        return Math.Round((end - StartDate).TotalDays / 365.25, 1);
    }
}