using CompanhiaAguasWebMVC.Data;
using CompanhiaAguasWebMVC.Data.Entities;
using CompanhiaAguasWebMVC.Helpers;
using CompanhiaAguasWebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Controllers
{
    public class ConsumptionsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;
        private readonly IConsumptionRepository _consumptionRepository;
        private readonly IInvoiceRepository _invoiceRepository;

        public ConsumptionsController(
                IClientRepository clientRepository,
                IUserHelper userHelper,
                IConsumptionRepository consumptionRepository,
                IInvoiceRepository invoiceRepository)
        {
            _consumptionRepository = consumptionRepository;
            _clientRepository = clientRepository;
            _userHelper = userHelper;
            _invoiceRepository = invoiceRepository;
        }

        public IActionResult Index()
        {
            return View(_consumptionRepository.GetAll());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }

            var consumption = await _consumptionRepository.GetByIdAsync(id.Value);
            if (consumption == null)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }

            return View(consumption);
        }


        // GET: Clients/Create
        [Authorize(Roles = "Employee")]
        public IActionResult Create()
        {
            var model = new CreateNewConsumptionViewModel
            {
                Clients = _clientRepository.GetComboClients(),
            };

            return View(model);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNewConsumptionViewModel createNewConsumptionViewModel)
        {
            if (ModelState.IsValid)
            {
                createNewConsumptionViewModel.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                /*var client = await _clientRepository.GetByIdAsync(Convert.ToInt32(createNewConsumptionViewModel.ClientId));*/

                

                int consQuantity = createNewConsumptionViewModel.Quantity;

                if(consQuantity <= 5)
                {
                    createNewConsumptionViewModel.TotalToBill = (decimal)(consQuantity * 0.3);
                }
                else if(consQuantity <= 15)
                {
                    createNewConsumptionViewModel.TotalToBill = (decimal)(5 * 0.3 + (consQuantity - 5) * 0.8);
                }
                else if(consQuantity <= 25)
                {
                    createNewConsumptionViewModel.TotalToBill = (decimal)(5 * 0.3 + 10 * 0.8 + (consQuantity - 15) * 1.2);
                }
                else
                {
                    createNewConsumptionViewModel.TotalToBill = (decimal)(5 * 0.3 + 10 * 0.8 + 10 * 1.2 + (consQuantity - 25) * 1.6);
                }


                /*Consumption consumption = new Consumption
                {
                    Client = client,
                    DateTime = createNewConsumptionViewModel.DateTime,
                    Quantity = createNewConsumptionViewModel.Quantity,
                    TotalToBill = createNewConsumptionViewModel.TotalToBill,
                    User = createNewConsumptionViewModel.User
                };*/

                await _consumptionRepository.CreateAsync(createNewConsumptionViewModel);

                return RedirectToAction(nameof(Index));
            }
            return View(createNewConsumptionViewModel);
        }

        // GET: Clients/Edit/5

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }

            var consumption = await _consumptionRepository.GetByIdAsync(id.Value);
            if (consumption == null)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }
            return View(consumption);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Consumption consumption)
        {
            if (id != consumption.Id)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }

            if (ModelState.IsValid)
            {
                try
                {
                    consumption.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    int consQuantity = consumption.Quantity;

                    if (consQuantity <= 5)
                    {
                        consumption.TotalToBill = (decimal)(consQuantity * 0.3);
                    }
                    else if (consQuantity <= 15)
                    {
                        consumption.TotalToBill = (decimal)(5 * 0.3 + (consQuantity - 5) * 0.8);
                    }
                    else if (consQuantity <= 25)
                    {
                        consumption.TotalToBill = (decimal)(5 * 0.3 + 10 * 0.8 + (consQuantity - 15) * 1.2);
                    }
                    else
                    {
                        consumption.TotalToBill = (decimal)(5 * 0.3 + 10 * 0.8 + 10 * 1.2 + (consQuantity - 25) * 1.6);
                    }
                    await _consumptionRepository.UpdateAsync(consumption);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _consumptionRepository.ExistAsync(consumption.Id))
                    {
                        return new NotFoundViewResult("ConsumptionNotFound");

                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(consumption);
        }

        // GET: Clients/Delete/5

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }

            var consumption = await _consumptionRepository.GetByIdAsync(id.Value);
            if (consumption == null)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }

            return View(consumption);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumption = await _consumptionRepository.GetByIdAsync(id);
            await _consumptionRepository.DeleteAsync(consumption);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetConsumptions()
        {
            return Ok(_consumptionRepository.GetAllWithUsers());
        }



        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GenerateInvoice(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }

            var consumption = await _consumptionRepository.GetByIdAsync(id.Value);
            if (consumption == null)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }
            return View(consumption);
        }







        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateInvoice(int id, Consumption consumption)
        {
            if (id != consumption.Id)
            {
                return new NotFoundViewResult("ConsumptionNotFound");

            }

            bool existThisInvoice = await _invoiceRepository.ExistInvoiceConsumptionAsync(consumption.Id);

            if (existThisInvoice)
            {
                TempData["Message"] = "Já existe uma fatura para este consumo!!!";
                return View();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    
                    var client = await _clientRepository.GetByIdAsync(consumption.ClientId);
                    decimal tax = 6;
                    decimal subTotal = consumption.TotalToBill;
                    decimal total = subTotal + tax/100 * subTotal;

                    Invoice invoice = new Invoice
                    {
                        
                        
                        DateTime = DateTime.Now,
                        SubTotal = consumption.TotalToBill,
                        Tax = tax,
                        Total = total
                    };

                    await _invoiceRepository.CreateAsync(invoice);

                    invoice.Client = client;
                    invoice.Consumption = consumption;
                    invoice.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                    await _invoiceRepository.UpdateAsync(invoice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _consumptionRepository.ExistAsync(consumption.Id))
                    {
                        return new NotFoundViewResult("ConsumptionNotFound");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Invoices");
            }
            return View(consumption);
        }

        public IActionResult ConsumptionNotFound()
        {
            return View();
        }

        // GET: Clients/Create
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateByCustomer()
        {
            var user = this.User.Identity.Name;


            Client client = await _clientRepository.GetClientByEmail(this.User.Identity.Name);



            var model = new Consumption
            {
                ClientId = client.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateByCustomer(Consumption consumption)
        {
            if (ModelState.IsValid)
            {
                consumption.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                /*var client = await _clientRepository.GetByIdAsync(Convert.ToInt32(createNewConsumptionViewModel.ClientId));*/



                int consQuantity = consumption.Quantity;

                if (consQuantity <= 5)
                {
                    consumption.TotalToBill = (decimal)(consQuantity * 0.3);
                }
                else if (consQuantity <= 15)
                {
                    consumption.TotalToBill = (decimal)(5 * 0.3 + (consQuantity - 5) * 0.8);
                }
                else if (consQuantity <= 25)
                {
                    consumption.TotalToBill = (decimal)(5 * 0.3 + 10 * 0.8 + (consQuantity - 15) * 1.2);
                }
                else
                {
                    consumption.TotalToBill = (decimal)(5 * 0.3 + 10 * 0.8 + 10 * 1.2 + (consQuantity - 25) * 1.6);
                }


                /*Consumption consumption = new Consumption
                {
                    Client = client,
                    DateTime = createNewConsumptionViewModel.DateTime,
                    Quantity = createNewConsumptionViewModel.Quantity,
                    TotalToBill = createNewConsumptionViewModel.TotalToBill,
                    User = createNewConsumptionViewModel.User
                };*/

                await _consumptionRepository.CreateAsync(consumption);

                return RedirectToAction(nameof(ConsumptionsClient));
            }
            return View(consumption);
        }

        public async Task<IActionResult> ConsumptionsClient()
        {
            Client client = await _clientRepository.GetClientByEmail(this.User.Identity.Name);


            return View(_consumptionRepository.GetAllByClient(client.Id));
        }
    }

}

