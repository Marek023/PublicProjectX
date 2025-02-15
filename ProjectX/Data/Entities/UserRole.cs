using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjectX.Data.Entities;
[PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRole
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    [Required] 
    public DateTime AssignedAt { get; set; }

    [ForeignKey(nameof(UserId))] 
    public virtual User User { get; set; }

    [ForeignKey(nameof(RoleId))] 
    public virtual Role Role { get; set; }
    
    public bool IsDeleted { get; set; }
}