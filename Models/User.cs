    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

namespace loginRegistration.Models{
    public class User{
        [Key]
        public long id {get; set;}
        
        [Required]
        [Display(Name="First Name:")]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="First name can only contain letters!")]
        public string FirstName {get; set;}

        [Required]
        [Display(Name="Last Name:")]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage="Last name can only contain letters!")]
        public string LastName {get; set;}

        [Required]
        [Display(Name="Email Address:")]
        [EmailAddress]
        public string EmailAddress {get; set;}

        [Required]
        [Display(Name="Password:")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Display(Name="Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation password must match!")]
        public string ConfirmPassword {get; set;}
        public DateTime created_at{get;set;}
        public DateTime updated_at{get; set;}

    public User(){
        created_at=DateTime.Now;
        updated_at=DateTime.Now;
    }


    } 

    public class LoginUser{
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Display(Name="Email: ")]
        public string LogEmail {get; set;}

        
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        [Display(Name="Password: ")]
        public string LogPassword {get; set;}
    }
    
}