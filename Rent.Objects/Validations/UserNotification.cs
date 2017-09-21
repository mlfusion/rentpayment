using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Entities
{
    [MetadataType(typeof(UsersNotification))]
    public partial class UsersNotification
    {
    }

    public class UserNotificationVal
    {
        // private
        private bool? _email;
        private bool? _phone;

        public bool Email { get; set; }

        public bool Phone { get; set; }
    }
}
