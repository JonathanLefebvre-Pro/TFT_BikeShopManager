using BikeShopManager.AppCore.Interfaces.Dispatcher;
using BikeShopManager.AppCore.Interfaces.Repositories.Stock;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockVatRateController(IDispatcher Dispatcher) : ControllerBase
    {
        //[Route("GetVatRatesAsync")]
        //[HttpGet]
        //[Authorize(Roles = ("Admin, Manager, Seller, Mechanic"))]
        /*public async Task<IActionResult> GetVatRatesAsync()
        {
            
        }*/
    }
}
