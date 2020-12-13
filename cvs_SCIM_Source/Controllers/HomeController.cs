using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using cvs_SCIM20.Okta.SCIM.Models;
using cvs_SCIM20.Connectors;

namespace cvs_SCIM20.Controllers
{
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private static ISCIMConnector _connector;
        private readonly IConfiguration _config;
        public List<SCIMUser> _scimUsers = null;
        public List<SCIMGroup> _scimGroups = null;


        public HomeController(ILogger<HomeController> logger, IConfiguration config, ISCIMConnector conn)
        {
            _connector = conn;
            _logger = logger;
            _config = config;
            _scimUsers = new List<SCIMUser>();
            _scimGroups = new List<SCIMGroup>();
        }



        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }


        [Route("Home/DisplayUsers")]
        public IActionResult DisplayUsers()
        {
            SCIMFilter myFilter = null;
            PaginationProperties pp = new PaginationProperties(200, 1);
            Okta.SCIM.Models.SCIMUserQueryResponse rGetUsers = _connector.getUsers(pp, myFilter);
            _scimUsers = rGetUsers.Resources;

            return View(_scimUsers);
        }


        [Route("Home/DisplayGroups")]
        public IActionResult DisplayGroups()
        {

            PaginationProperties pp = new PaginationProperties(200, 1);
            Okta.SCIM.Models.SCIMGroupQueryResponse rGetGroups = _connector.getGroups(pp);
            _scimGroups = rGetGroups.Resources;
            return View(_scimGroups);
        }

    }
}
