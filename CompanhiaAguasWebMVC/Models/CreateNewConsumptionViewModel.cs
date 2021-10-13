using CompanhiaAguasWebMVC.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Models
{
    public class CreateNewConsumptionViewModel : Consumption
    {
       
       

        
        public IEnumerable<SelectListItem> Clients { get; set; }

        
        

    }
}
