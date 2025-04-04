using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class SupplierToAddDto
    {
        public int ? Id { get; set; }
        public string Name { get; set; }

        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; }
        public string Agent { get; set; }
        public ProductDto[] Product { get; set; }
    }
}
