using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rent.Entities.Models
{
   public class Paging
    {
       public int CurrentPage { get; set; }
       public int PageSize { get; set; }
       public int TotalCount { get; set; }
       public string SortColumn { get; set; }
       public string SortOrder { get; set; }
       public string Search { get; set; }
       public int ActionId { get; set; }
       public int Id { get; set; }
    }
}
