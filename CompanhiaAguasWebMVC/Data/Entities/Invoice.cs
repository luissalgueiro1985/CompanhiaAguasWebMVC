using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Data.Entities
{
    public class Invoice : IEntity
    {
        [Display(Name = "Id Fatura")]
        public int Id { get ; set ; }

        [Display(Name = "Cliente")]
        public  Client Client { get; set; }

        [Display(Name = "Data")]
        public DateTime DateTime { get; set; }


        [Display(Name = "Quantidade Consumo")]
        public Consumption Consumption { get; set; }


        [Display(Name = "User")]
        public User User { get; set; }


        [Display(Name = "Subtotal")]
        public decimal SubTotal { get; set; }


        [Display(Name = "Taxa Iva")]
        public decimal Tax { get; set; }


        [Display(Name = "Total a pagar")]
        public decimal Total { get; set; }


        [Display(Name = "Pagamento")]
        public bool IsPaid { get; set; }
    }
}
