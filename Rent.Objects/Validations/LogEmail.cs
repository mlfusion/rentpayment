using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Entities
{
   public partial class LogEmail
    {
       public virtual Entities.Models.Paging Paging { get; set; }
       public virtual Entities.User User { get; set; }
    }
}
