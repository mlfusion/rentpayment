using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Entities
{
    [MetadataType(typeof (UserVal))]
    public partial class User
    {
        public virtual UsersRole UsersRole { get; set; }
        public virtual UsersPassword UsersPassword { get; set; }
        public virtual UsersNotification UsersNotification { get; set; }
        public virtual RentPayment RentPayment { get; set; }
        public virtual Models.Paging Paging { get; set; }

        [Required(ErrorMessage = "Role field is required.")]
        public virtual int RoleId { get; set; }
        
        [Required(ErrorMessage = "Manager field is required.")]
        public int? ManagerId { get; set; }
       
        public virtual string ManagerName { get; set; }
        public virtual string Password { get; set; }
        public bool EmailNotification { get; set; }
        public bool PhoneNotification { get; set; }
    }

    public class UniqueEmail : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success; ;

            Rent.Entities.RentEntities context = new RentEntities();
            var userEmailValue = value.ToString();
            var count = context.Users.Count(x => x.Email == userEmailValue && x.Active);
            
            return count != 0 ? new ValidationResult("Email address already exist.") : ValidationResult.Success;
        }
    }

    public class UserVal
    {
        // private
        private string _email;

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        //[UniqueEmail]
        public string Email
        {
            get { return _email.ToLower(); }
            set { _email = value.ToLower(); }
        }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
    }
}
