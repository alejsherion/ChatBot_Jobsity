using ExternalApi.Domain.Contract;
using ExternalApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExternalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockInfoController: ControllerBase
    {
        #region Members
        public IStockInfoDomain StockInfoDomain { get; } 
        #endregion

        #region Builder
        public StockInfoController(IStockInfoDomain stockInfoDomain)
        {
            StockInfoDomain = stockInfoDomain;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get Info about stock code
        /// </summary>
        /// <param name="stock_code">code for analist</param>
        /// <returns>stooq code data</returns>
        [HttpGet]
        [Route("GetStooq")]
        public ActionResult<StooqModel> GetStooq(string stock_code)
        {
            try
            {
                var result = StockInfoDomain.GetStooq(stock_code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
