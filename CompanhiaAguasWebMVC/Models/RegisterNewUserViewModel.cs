using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CompanhiaAguasWebMVC.Models
{
    public class RegisterNewUserViewModel
    {
        [Required]
        [Display(Name = "Primeiro Nome")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Último Nome")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Display(Name = "Morada")]
        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string Address { get; set; }

        [Display(Name = "Telefone")]
        [MaxLength(20, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Role Name")]
        public int RoleName { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }

        /*[Display(Name = "Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country.")]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> Countries { get; set; }*/


        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
