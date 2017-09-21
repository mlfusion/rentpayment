using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using User = Rent.Entities.User;

namespace Rent.Web.Models
{
    public class UserModels
    {
        [MetadataType(typeof(UserMetaData))]
        public partial class User
        {
        }

       public class UserMetaData
        {
            [Remote("IsUserExists", "User", "Admin", ErrorMessage = "User Name already in use")]
            public string Email { get; set; }
        }
    }
}