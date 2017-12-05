using System.ComponentModel.DataAnnotations;

namespace loginRegistration.Models{
    public class RegisterUser{
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
        [Compare("Password", ErrorMessage = "Password and confirmation password must match!")]
        public string ConfirmPassword {get; set;}


    } 
    public class HomePageUsers
    {
        public RegisterUser Register {get;set;}
        // public LoginUser Login {get;set;}
    }  
}