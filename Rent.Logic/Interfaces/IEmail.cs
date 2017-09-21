using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Business.Interfaces
{
   public interface IEmail
   {
       string ForgotPasswordEmail(Entities.User obj);
       string RentPaymentEmail(int id);
       string UserAccessRegistrationEmail(int id);
       string UserRegistrationEmail(int id);
   }
}
