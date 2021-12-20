using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
