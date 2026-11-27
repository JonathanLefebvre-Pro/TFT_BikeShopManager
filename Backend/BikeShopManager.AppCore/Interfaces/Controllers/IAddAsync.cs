using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.AppCore.Interfaces.Controllers;

public interface IControllerAddAsync<TAddDto>
{
    Task<IActionResult> AddAsync([FromBody] TAddDto dto);
}
