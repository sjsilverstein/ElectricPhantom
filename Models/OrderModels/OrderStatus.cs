using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectricPhantom.Models;

namespace ElectricPhantom.Models
{
    public  class OrderStatus: BaseEntity {
        
        [Key]
        public int OrderStatusId { get; set; }

        public string Status { get; set; }
    }
    
}