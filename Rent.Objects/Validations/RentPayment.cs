using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Entities
{
    [MetadataType(typeof (RentPaymentValidations))]
    public partial class RentPayment
    {
        public Models.Paging Paging { get; set; }
        public RentPaymentNoticeSendLog RentPaymentNoticeSendLog { get; set; }
        public virtual int PaymentCount { get; set; }
       // public User Users { get; set; }
    }

    public class RentPaymentValidations
    {
        [DataType(DataType.Currency)]
        public double Payment { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime? PaymentDate { get; set; }

        [DataType(DataType.Date)]
        public System.DateTime Created { get; set; }

        [Required(ErrorMessage="The Username field is required.")]
        public int Uid { get; set; }
    }
}
