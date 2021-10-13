using CompanhiaAguasWebMVC.Data;
using CompanhiaAguasWebMVC.Data.Entities;
using CompanhiaAguasWebMVC.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ClientsController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly IUserHelper _userHelper;

        public ClientsController(
            IClientRepository clientRepository,
            IUserHelper userHelper)
        {
            _clientRepository = clientRepository;
            _userHelper = userHelper;
        }

        // GET: Clients
        public IActionResult Index()
        {
            return View(_clientRepository.GetAll());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ClientNotFound");
            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return new NotFoundViewResult("ClientNotFound");

            }

            return View(client);
        }


        // GET: Clients/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                client.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _clientRepository.CreateAsync(client);

                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Edit/5

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ClientNotFound");

            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return new NotFoundViewResult("ClientNotFound");

            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.Id)
            {
                return new NotFoundViewResult("ClientNotFound");

            }

            if (ModelState.IsValid)
            {
                try
                {
                    client.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);

                    await _clientRepository.UpdateAsync(client);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _clientRepository.ExistAsync(client.Id))
                    {
                        return new NotFoundViewResult("ClientNotFound");

                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Clients/Delete/5

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("ClientNotFound");

            }

            var client = await _clientRepository.GetByIdAsync(id.Value);
            if (client == null)
            {
                return new NotFoundViewResult("ClientNotFound");

            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            await _clientRepository.DeleteAsync(client);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            return Ok(_clientRepository.GetAllWithUsers());
        }

        public IActionResult ClientNotFound()
        {
            return View();
        }

    }
}
