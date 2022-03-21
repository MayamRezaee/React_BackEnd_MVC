using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseEntities.Models
{
    public class ApplicationUSer :IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        //-----------------------------------------------------
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
