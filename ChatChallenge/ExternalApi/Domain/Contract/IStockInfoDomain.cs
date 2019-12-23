using ExternalApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalApi.Domain.Contract
{
    public interface IStockInfoDomain
    {
        StooqModel GetStooq(string stock_code);
    }
}
