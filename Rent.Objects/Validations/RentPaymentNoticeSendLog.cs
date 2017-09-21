using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Entities
{
    [MetadataType(typeof (RentPaymentNoticeSendLogVal))]
    public partial class RentPaymentNoticeSendLog
    {
        public System.DateTime? NoticeCreated { get; set; }
    }

    public class RentPaymentNoticeSendLogVal
    {
        
    }
}
