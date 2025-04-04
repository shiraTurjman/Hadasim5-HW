using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dal.Entity
{
    public class ProductEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } 
    
        [Required]
        public double Price { get; set; }

        [Required]
        public double MinimumQuantity { get; set; }

        [Required]
        [ForeignKey("Suppliers")]
        public int SupplierId { get; set; }
        public SupplierEntity Supplier { get; set; }

    

    }
}
