using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        decimal GetBalance(int id);

        bool AddToBalance(Transfers transfers);

        bool SubtractFromBalance(Transfers transfers);
    }
}
