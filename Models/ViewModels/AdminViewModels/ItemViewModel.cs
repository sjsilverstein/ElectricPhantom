using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace ElectricPhantom.Models {
    public class ItemViewModel {

        [Required]
        public string ItemName { get;set; }    
        
        [Required]
        public string Description { get;set; }         
        
        [Required]
        public float Price{ get;set; }

        [Required]
        public IFormFile MyImage { set; get; }
        
        [Required]
        public int CatagoryId { get; set; }

    }
}