using System.ComponentModel.DataAnnotations;

namespace ProjectX.Data.Entities;

public class Role
{
        [Key] 
        public Guid RoleId { get; set; }  

        [Required]  
        [MaxLength(100)]  
        public string Name { get; set; }
        
        [MaxLength(255)]  
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
}