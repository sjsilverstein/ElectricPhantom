using System.ComponentModel.DataAnnotations;

namespace ElectricPhantom.Models {
    public class AddToCartViewModel {

        [Required]
        public int SizeId { get; set; }
        
        [Required]
        public int ItemId { get; set; }

    }
}