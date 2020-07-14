using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransfersDAO
    {
        List<Transfers> GetTransfers(int id);
        Transfers GetTransferDetails(int id);
        bool TransferRequest(Transfers transfer);
    }
}
