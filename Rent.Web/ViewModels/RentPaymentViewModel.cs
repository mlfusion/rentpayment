using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rent.Entities;

namespace Rent.Web.ViewModels
{
    public class RentPaymentViewModel
    {
        public int RentPaymentId { get; set; }
        public double Payment { get; set; }
        public System.DateTime PaymentDate { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Modifed { get; set; }
        public bool Active { get; set; }
        public int Uid { get; set; }

        public virtual Entities.Models.Paging Paging { get; set; }
        public virtual User User { get; set; }

    }
}