using System.ComponentModel.DataAnnotations;

namespace ProjectX.Data.Entities;

public class User
{
    [Key] 
    public Guid UserId { get; set; }
    
    [Required]  
    [StringLength(100)]  
    public string UserName { get; set; }

    [Required]  
    [EmailAddress]  
    [StringLength(100)]  
    public string Email { get; set; }

    [Required]  
    public string PasswordHash { get; set; }

    [Required]  
    public string PasswordSalt { get; set; }

    public bool IsEmailVerified { get; set; }
    public DateTime? LastLogin { get; set; }

    public bool IsDeleted { get; set; }
    public virtual List<UserRole> UserRoles { get; set; } = new();
    public virtual List<YearStatistic> YearStatistics { get; set; } = new();
}