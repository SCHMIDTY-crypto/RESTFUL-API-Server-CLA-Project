using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TenmoServer.DAO;
using TenmoServer.Models;
using TenmoServer.Security;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransfersDAO transfersDAO;
        private readonly IAccountDAO accountDAO;

        public TransferController(ITransfersDAO _transfersDAO, IAccountDAO _accountDAO)
        {
            transfersDAO = _transfersDAO;
            accountDAO = _accountDAO;
        }

        [HttpGet]
        public List<Transfers> GetTransfers()
        {
            var id = User.FindFirst("sub")?.Value;
            var accountId = int.Parse(id);
            if(accountId > 0)
            {
                return transfersDAO.GetTransfers(accountId);
            }
            return null; 
        }

        [HttpGet("{id}")]
        public Transfers GetTransferDetails(int id)
        {
            return transfersDAO.GetTransferDetails(id);
        }

        [HttpPost]
        public bool TransferRequest(Transfers transfer)
        {
            if(transfer.Amount <= accountDAO.GetBalance(transfer.AccountFrom))
            {
                return transfersDAO.TransferRequest(transfer);
            }
            else
            {
                return false;
            }
        }

        // httpGet Method that will get all transfers that have to deal with the current user

        // httpGet that will just get all of the information from a transfer, used for AFTER a user has grabbed the list of transfers


    }
}
