namespace ApheliosID.Core.Models;

public class AcademicCredential : VerifiableCredential
{
    public string Degree { get; private set; }
    public string Institution { get; private set; }
    public string FieldOfStudy { get; private set; }
    public DateTime GraduationDate { get; private set; }
    public decimal? GPA { get; private set; }

    public AcademicCredential(
        string issuer,
        string subject,
        string degree,
        string institution,
        string fieldOfStudy,
        DateTime graduationDate,
        decimal? gpa = null,
        DateTime? expiresAt = null)
        : base(issuer, subject, expiresAt)
    {
        Degree = degree ?? throw new ArgumentNullException(nameof(degree));
        Institution = institution ?? throw new ArgumentNullException(nameof(institution));
        FieldOfStudy = fieldOfStudy ?? throw new ArgumentNullException(nameof(fieldOfStudy));
        GraduationDate = graduationDate;
        GPA = gpa;
    }

    public override string GetCredentialType()
    {
        return "AcademicCredential";
    }

    public override bool ValidateSpecificClaims()
    {
        if (string.IsNullOrWhiteSpace(Degree)) return false;
        if (string.IsNullOrWhiteSpace(Institution)) return false;
        if (GraduationDate > DateTime.UtcNow) return false;
        if (GPA.HasValue && (GPA < 0 || GPA > 4.0m)) return false;
        
        return true;
    }

    public override Dictionary<string, object> GetCredentialInfo()
    {
        var info = base.GetCredentialInfo();
        info["degree"] = Degree;
        info["institution"] = Institution;
        info["fieldOfStudy"] = FieldOfStudy;
        info["graduationDate"] = GraduationDate;
        if (GPA.HasValue) info["gpa"] = GPA.Value;
        
        return info;
    }
}