using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectricPhantom.Models;

namespace ElectricPhantom.Models
{
    public  class Catagory: BaseEntity {
        
        [Key]
        public int CatagoryId { get; set; }

        public string CatagoryName { get; set; }

        List<Item> Items { get; set; }
    }
    
}