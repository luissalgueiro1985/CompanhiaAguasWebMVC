using CompanhiaAguasWebMVC.Data;
using CompanhiaAguasWebMVC.Data.Entities;
using CompanhiaAguasWebMVC.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Controllers
{
    
    public class InvoicesController : Controller
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IConsumptionRepository _consumptionRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;

        public InvoicesController(
            IInvoiceRepository invoiceRepository,
            IClientRepository clientRepository,
            IConsumptionRepository consumptionRepository,
            IUserHelper userHelper)
        {
            _invoiceRepository = invoiceRepository;
            _clientRepository = clientRepository;
            _consumptionRepository = consumptionRepository;
            _userHelper = userHelper;
        }
        public IActionResult Index()
        {
            return View( _invoiceRepository.GetAllInvoicesAsync());
        }

        // GET: Invoices/Edit/5

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("InvoiceNotFound");

            }

            var invoice = await _invoiceRepository.GetByIdAsync(id.Value);
            if (invoice == null)
            {
                return new NotFoundViewResult("InvoiceNotFound");

            }
            return View(invoice);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return new NotFoundViewResult("InvoiceNotFound");

            }

            if (ModelState.IsValid)
            {
                try
                {
                    invoice.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                    await _invoiceRepository.UpdateAsync(invoice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _invoiceRepository.ExistAsync(invoice.Id))
                    {
                        return new NotFoundViewResult("InvoiceNotFound");

                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        public IActionResult InvoiceNotFound()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> InvoicesCustomer()
        {
            Client client = await _clientRepository.GetClientByEmail(this.User.Identity.Name);

            return View(_invoiceRepository.GetAllByClient(client.Id));
        }
    }
}
