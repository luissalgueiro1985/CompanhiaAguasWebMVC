using CompanhiaAguasWebMVC.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class LastInvoiceController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public LastInvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetLastInvoice()
        {
            return Ok( await _invoiceRepository.GetLastInvoice());
        }
    }
}
