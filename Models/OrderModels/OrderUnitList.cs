using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectricPhantom.Models;

namespace ElectricPhantom.Models
{
    public  class OrderUnitList: BaseEntity {
        
        [Key]
        public int OrderUnitListId { get;set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
        
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

    }
    
}