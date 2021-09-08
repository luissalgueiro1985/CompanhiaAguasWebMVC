using System.ComponentModel.DataAnnotations;

namespace CompanhiaAguasWebMVC.Data.Entities
{
    public class Client : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string LastName { get; set; }


        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string MidlleName { get; set; }


        [Display(Name = "Full Name")]
        public string FullName => $"{FirstName} {MidlleName} {LastName}";

        [Required]
        [MaxLength(150, ErrorMessage = "The field {0} only can contain {1} characters lenght.")]
        public string Address { get; set; }

        [Required]
        [Range(18,135)]
        public int Age { get; set; }


        [Required]
        public int CitizenCard { get; set; }


        [Required]
        public int FiscalNumber { get; set; }


        public bool IsActive { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }


        public User User { get; set; }
    }
}
