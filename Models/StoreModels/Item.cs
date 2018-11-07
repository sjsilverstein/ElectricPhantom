using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectricPhantom.Models;

namespace ElectricPhantom.Models
{
    public  class Item: BaseEntity {
        
        [Key]
        public int ItemId { get;set; }

        public string ItemName { get;set; }    
        
        public string Description { get;set; }         
        
        public float Price{ get;set; }

        public int CatagoryId { get; set; }
        public Catagory ItemCatagory { get; set; }

        public List<Unit> Units { get; set; }

    }
}