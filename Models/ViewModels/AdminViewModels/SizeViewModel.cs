using System.ComponentModel.DataAnnotations;

namespace ElectricPhantom.Models {
    public class SizeViewModel {
        [Required]
        public string SizeName { get; set; } 
    }
}