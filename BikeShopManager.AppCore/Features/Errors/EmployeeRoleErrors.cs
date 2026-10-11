namespace BikeShopManager.AppCore.Features.Errors;

public static class EmployeeRoleError
{
    public static Error NotFound =>
        new Error("EmployeeRoleError.NotFound", "EmployeeRole not found !");
    public static Error NotInserted =>
        new Error("EmployeeRoleError.NotInserted", "EmployeeRole not inserted !");
    public static Error NotUpdated =>
        new Error("EmployeeRoleError.NotUpdated", "EmployeeRole not updated !");
    public static Error NotDisabled =>
        new Error("EmployeeRoleError.NotDisabled", "EmployeeRole not disabled !");
    public static Error NotEnabled =>
        new Error("EmployeeRoleError.NotEnabled", "EmployeeRole not enabled !");
    public static Error Empty =>
        new Error("EmployeeRoleError.TableIsEmpty", "EmployeeRole table is empty !");
}
