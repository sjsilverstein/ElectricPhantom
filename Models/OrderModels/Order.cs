using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectricPhantom.Models;

namespace ElectricPhantom.Models
{
    public  class Order: BaseEntity {
        
        [Key]
        public int OrderId { get;set; }

        public float OrderAmount { get; set; }

        public int OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public int UserId { get; set; }
        public User Customer { get; set; }

        public List<OrderUnitList> OrderUnitList { get; set; }

    }
    
}