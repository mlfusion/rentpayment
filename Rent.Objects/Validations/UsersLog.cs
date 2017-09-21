using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Entities
{
    [MetadataType(typeof(UserLogVal))]
    public partial class UsersLog
    {
        public virtual Models.Paging Paging { get; set; }
    }

    public class UserLogVal
    {
        
    }
}
