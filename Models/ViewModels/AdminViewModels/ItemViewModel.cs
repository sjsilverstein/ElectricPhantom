using System.ComponentModel.DataAnnotations;

namespace ElectricPhantom.Models {
    public class ItemViewModel {

        [Required]
        public string ItemName { get;set; }    
        
        [Required]
        public string Description { get;set; }         
        
        [Required]
        public float Price{ get;set; }
        
        [Required]
        public int CatagoryId { get; set; }

    }
}