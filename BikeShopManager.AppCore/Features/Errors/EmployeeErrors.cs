namespace BikeShopManager.AppCore.Features.Errors;

public static class EmployeeError
{
    public static Error NotFound => new Error("EmployeeError.NotFound", "No Employee found !");
    public static Error NotInserted =>
        new Error("EmployeeError.NotInserted", "Employee was not inserted !");
    public static Error NotUpdated =>
        new Error("EmployeeError.NotUpdated", "Employee was not updated !");
    public static Error NotDisabled =>
        new Error("EmployeeError.NotDisabled", "Employee was not disabled !");
    public static Error TableIsEmpty =>
        new Error("EmployeeError.TableIsEmpty", "Employee table is empty !");
}
