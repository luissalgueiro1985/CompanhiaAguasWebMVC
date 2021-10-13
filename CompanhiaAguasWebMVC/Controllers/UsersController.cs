using CompanhiaAguasWebMVC.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanhiaAguasWebMVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {

        private readonly IUserHelper _userHelper;

        public UsersController(IUserHelper userHelper)
        {
            _userHelper = userHelper;
        }

        public IActionResult Index()
        {
            return View(_userHelper.GetAllUsers());
        }
    }
}
