using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserDAO usersDAO;

        public UsersController(IUserDAO _usersDAO)
        {
            usersDAO = _usersDAO;
        }

        [HttpGet]
        public List<User> GetUsers()
        {
            return usersDAO.GetUsers();
        }

    }
}
