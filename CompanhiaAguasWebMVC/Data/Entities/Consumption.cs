using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data.Entities
{
    public class Consumption : IEntity
    {
        public int Id { get ; set ; }

        [Required]
        [Display(Name = "Cliente")]
        public int ClientId { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        [Display(Name = "Data")]
        public DateTime DateTime { get; set; }


        [Required]
        [Display(Name = "Quantidade")]
        public int Quantity { get; set; }

        
        [Display(Name = "Total a faturar")]
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal TotalToBill { get; set; }

        public User User { get; set; }
    }
}
