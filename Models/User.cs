using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ElectricPhantom.Models;
namespace ElectricPhantom.Models
{
    public  class User: BaseEntity {
        
        [Key]
        public int UserId {get;set;}

        public int UserLevel {get;set;}    
        
        public string Email{get;set;}        
        
        public string FirstName{get;set;}

        public string LastName{get;set;}               
        
        public string Password{get;set;}

    }
    
}