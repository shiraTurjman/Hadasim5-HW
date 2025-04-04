using Dal.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Entity
{
    
    public class OrderEntity
    {
        
        [Key]
        [Required]
        public int Id { get; set; }
        
        public DateTime? OrderDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [ForeignKey("Products")]
        public int ProductId { get; set; }
        public ProductEntity Product { get; set; }

        [Required]
        [ForeignKey("Status")]
        public int StatusId { get; set; }
        public StatusEntity Status { get; set; }

    }
}
