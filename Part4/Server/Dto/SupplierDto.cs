
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dto
{
    public class SupplierDto
    {
      public int? Id { get; set; }
      public string Name { get; set; }
      public string Phone { get; set; }
      public string Agent { get; set; }
      public OrderDto[] Orders { get; set; }
        // agree?:boolean;

    }
}
