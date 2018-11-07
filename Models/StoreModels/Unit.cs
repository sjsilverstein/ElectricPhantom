using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectricPhantom.Models;

namespace ElectricPhantom.Models
{
    public  class Unit: BaseEntity {
        
        [Key]
        public int UnitId { get;set; }

        public int Inventory { get; set; }

        public int SizeId { get; set; }
        public Size Size { get; set; }
        
        public int ItemId { get; set; }
        public Item Item { get; set; }

        public List<OrderUnitList> OrderUnitList { get; set; } 

    }
    
}