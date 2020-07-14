using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;


namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDAO accountDAO;

        public AccountController(IAccountDAO _accountDAO)
        {
            accountDAO = _accountDAO;
        }

        [HttpGet]
        public decimal GetBalance()
        {
            var id = User.FindFirst("sub")?.Value;
            var accountId = int.Parse(id);
            if (accountId > 0)
            {
                return accountDAO.GetBalance(accountId);
            }
            return 0;
        }

        [HttpPut]
        public bool SubtractFromBalance(Transfers transfers)
        {
            return accountDAO.SubtractFromBalance(transfers);
        }

        [HttpPut("add")]
        public bool AddToBalance(Transfers transfers)
        {
            return accountDAO.AddToBalance(transfers);
        }
    }
}
