using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectricPhantom.Models;

namespace ElectricPhantom.Models
{
    public  class Size: BaseEntity {
        
        [Key]
        public int SizeId {get;set;}

        public string SizeName {get;set;}

        public List<Unit> Units { get; set; }    

    }
    
}