﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_Commerce.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        [NotMapped] 
        public string FullName=>FirstName+" "+LastName;
    }
}
