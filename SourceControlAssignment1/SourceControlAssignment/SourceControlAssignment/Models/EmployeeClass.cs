using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SourceControlAssignment.Models
{
    public class EmployeeClass
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 3)]
        [DisplayName("Enter Your Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email ID is Required")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is Required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})[-. ]?([0-9]{4})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Not a valid Phone number")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Incorrect Format, Password Must Contain Minimum eight characters, at least one letter, one number and one special character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password Not Matched")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Age is Required")]
        [Range(18, 120, ErrorMessage = "Age must be 18 Years")]
        public int Age { get; set; }
    }
}