using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete;

public class User : IEntity,ITenancy
{
    public User()
    {
        if (UserId == 0)
        {
            RecordDate = DateTime.Now;
        }
        UpdateContactDate = DateTime.Now;
    }    
    public int UserId { get; set; }
    public int CompanyId { get; set; }
    public int TenantId { get; set; }    
    public long CitizenId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string RefreshToken { get; set; }
    public string MobilePhones { get; set; }
    public bool Status { get; set; }
    public DateTime BirthDate { get; set; }
    public int Gender { get; set; }
    public DateTime RecordDate { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
    public DateTime UpdateContactDate { get; set; }

    /// <summary>
    /// This is required when encoding token. Not in db. The default is Person.
    /// </summary>
    [NotMapped]
    public string AuthenticationProviderType { get; set; } = "Person";

    public byte[] PasswordSalt { get; set; }
    public byte[] PasswordHash { get; set; }

    public bool UpdateMobilePhone(string mobilePhone)
    {
        if (mobilePhone == MobilePhones)
        {
            return false;
        }

        MobilePhones = mobilePhone;
        return true;
    }
}
