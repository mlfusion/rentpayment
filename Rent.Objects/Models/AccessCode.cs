using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Entities.Models
{
   public class AccessCode
    {
       [Required]
       public string Code { get; set; }
       [Required]
       public string ZipCode { get; set; }
    }
}
