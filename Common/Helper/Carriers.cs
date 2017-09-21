using System.Collections.Generic;
using System.Linq;
using Rent.Entities.Models;

namespace Rent.Common.Helper
{
   public class Carriers
    {

       public static IEnumerable<string> SelectMobileCarriers(object phone)
       {
           return MobileCarriers().Select(x => phone + x.Gateway.Replace("[num]", "")).ToList();
       }

       private static IEnumerable<Rent.Entities.Models.MobileCarriers> MobileCarriers()
       {
           List<Rent.Entities.Models.MobileCarriers> mobileCarrierss = new List<Rent.Entities.Models.MobileCarriers>();

           mobileCarrierss.Add(new MobileCarriers {Name = "Alltel", Gateway = "[num]@message.alltel.com"});
           mobileCarrierss.Add(new MobileCarriers {Name = "AT&T", Gateway = "[num]@mms.att.net"});
           mobileCarrierss.Add(new MobileCarriers {Name = "Boost", Gateway = "[num]@myboostmobile.com"});
           mobileCarrierss.Add(new MobileCarriers {Name = "Cricket", Gateway = "[num]@mms.mycricket.com"});
           mobileCarrierss.Add(new MobileCarriers {Name = "MetroPcs", Gateway = "[num]@mymetropcs.com"});
           mobileCarrierss.Add(new MobileCarriers {Name = "Rogers", Gateway = "[num]@mms.rogers.com"});
           mobileCarrierss.Add(new MobileCarriers {Name = "Simple", Gateway = "[num]@smtext.com"});
           mobileCarrierss.Add(new MobileCarriers {Name = "Sprint", Gateway = "[num]@pm.sprint.com"});
           mobileCarrierss.Add(new MobileCarriers {Name = "StringhtTalk", Gateway = "[num]@tmomail.net"});
               //[num]@mms.att.net" });
           mobileCarrierss.Add(new MobileCarriers {Name = "Tmobile", Gateway = "[num]@tmomail.com"});
           mobileCarrierss.Add(new MobileCarriers {Name = "Virgin", Gateway = "[num]@vmpix.com"});
           mobileCarrierss.Add(new MobileCarriers {Name = "Verizon", Gateway = "[num]@vtext.com"});

           return mobileCarrierss;
       }
    }
}
