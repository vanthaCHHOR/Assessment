using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OkooneAssessment.Models
{
    public class AccountModel
    {
        [Required]
        public string user_name { get; set; }

        [Required]
        public string password { get; set; }
    }
}