using Microsoft.AspNetCore.Mvc;

namespace BikeShopManager.AppCore.Interfaces.Controllers;

public interface IControllerUpdateAsync<TUpdateDto>
{
    Task<IActionResult> UpdateAsync(int id, [FromBody] TUpdateDto dto);
}
