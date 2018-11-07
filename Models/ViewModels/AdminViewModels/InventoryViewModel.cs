using System.ComponentModel.DataAnnotations;

namespace ElectricPhantom.Models {
    public class InventoryViewModel {

        [Required]
        public int Inventory { get; set; }

        [Required]
        public int SizeId { get; set; }
        
        [Required]
        public int ItemId { get; set; }

    }
}