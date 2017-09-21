using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rent.Entities;

namespace Rent.Entities
{
    [MetadataType(typeof(UsersPasswordVal))]
    public partial class UsersPassword
    {
        [Required]
        [Display(Name = "Confirm Password")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match")]
        public string ConfirmPassword { get; set; }
    }

    public class UsersPasswordVal
    {
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Required]
        //[Display(Name = "Name")]
        //public string Name { get; set; }
    }
}
