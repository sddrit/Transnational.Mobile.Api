using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransnationalLanka.Rms.Mobile.Dal.Entities
{

    public class User
    {
        [Key]
      
        public int Id { get; set; }
       
        public string UserName { get; set; }
      
        public string FullName { get; set; }
       
        public string UserType { get; set; }
       
        public bool Active { get; set; }      
               
        public byte[] PasswordHash { get; set; }
        
        public byte[] PasswordSalt { get; set; }

        public virtual List<UserRole> Roles { get; set; }
        public virtual List<UserPassword> UserPasswords { get; set; }
        
    }

    [Table("UserRole")]
    public class UserRole
    {
        [Column("userId")]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        [Column("roleId")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
    }

    [Table("Role")]
    public class Role
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("description")]

        public string Description { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }


    [Table("UserMobile")]
    public class UserMobile
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("userId")]
        public string UserName { get; set; }       

        [Column("userType")]
        public string UserType { get; set; }

        public int UserId { get { return Convert.ToInt32(UserName); } set { } }
    }

    [Table("User")]
    public class UserGeneral
    {
        [Key]     
        [Column("userId")]
        public int UserId { get; set; }

        [Column("userFullName")]
        public string FullName { get; set; }
     
        [Column("active")]
        public bool Active { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; }


    }
}
