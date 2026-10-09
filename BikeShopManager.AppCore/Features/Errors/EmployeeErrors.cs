namespace BikeShopManager.AppCore.Features.Errors;

public static class EmployeeError
{
    public static Error NotFound => new Error("EmployeeError.NotFound", "Employee not found !");
    public static Error NotInserted =>
        new Error("EmployeeError.NotInserted", "Employee not inserted !");
    public static Error NotUpdated =>
        new Error("EmployeeError.NotUpdated", "Employee not updated !");
    public static Error NotDisabled =>
        new Error("EmployeeError.NotDisabled", "Employee not disabled !");
    public static Error NotEnabled =>
        new Error("EmployeeError.NotEnabled", "Employee not enabled !");
    public static Error Empty =>
        new Error("EmployeeError.TableIsEmpty", "Employee table is empty !");
}
