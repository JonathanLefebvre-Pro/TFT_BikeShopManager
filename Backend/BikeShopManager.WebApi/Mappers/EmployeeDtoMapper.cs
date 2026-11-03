using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos;

namespace BikeShopManager.WebApi.Mappers;

public static class EmployeeDtoMapper
{
    public static EmployeeResponseDto ToEmployeeResponseDto(this Employee entity)
    {
        return new EmployeeResponseDto()
        {
            #region Employee
            EmployeeId = entity.Id,
            PasswordHash = entity.PasswordHash,
            SecurityStamp = entity.SecurityStamp,
            HiredAt = entity.HiredAt,
            ContractEndAt = entity.ContractEndAt,
            IsActive = entity.IsActive,
            #endregion

            #region User
            FirstName = entity.User.FirstName,
            LastName = entity.User.LastName,
            Email = entity.User.Email,
            PhoneNumber = entity.User.PhoneNumber,
            Address = entity.User.Address,
            City = entity.User.City,
            ZipCode = entity.User.ZipCode,
            Country = entity.User.Country,
            BirthDate = entity.User.BirthDate,
            // AllowMarketing = entity.User.AllowMarketing,
            #endregion

            #region Role
            RoleId = entity.Role.Id,
            RoleName = entity.Role.Name,
            NormalizedRoleName = entity.Role.NormalizedName,
            #endregion
        };
    }

    public static IEnumerable<EmployeeResponseDto> ToIEnumerableEmployeeResponseDtos(
        this IEnumerable<Employee> entities
    )
    {
        List<EmployeeResponseDto> dtos = new List<EmployeeResponseDto>();
        foreach (Employee employee in entities)
            dtos.Add(employee.ToEmployeeResponseDto());

        return dtos;
    }

    public static Employee ToEmployeeEntity(this EmployeeUpdateDto dto)
    {
        return new Employee()
        {
            //PasswordHash = dto.PasswordHash,
            //SecurityStamp = dto.SecurityStamp,
            //Password = dto.Password,
            HiredAt = dto.HiredAt,
            ContractEndAt = dto.ContractEndAt,
        };
    }

    public static User ToUserEntity(this EmployeeUpdateDto dto)
    {
        return new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            City = dto.City,
            ZipCode = dto.ZipCode,
            Country = dto.Country,
            BirthDate = dto.BirthDate,
            AllowMarketing = false, //dto.AllowMarketing,
        };
    }

    public static Employee ToEmployeeEntity(this EmployeeAddDto dto)
    {
        return new Employee()
        {
            //PasswordHash = dto.PasswordHash,
            //SecurityStamp = dto.SecurityStamp,
            Password = dto.Password,
            HiredAt = dto.HiredAt,
            ContractEndAt = dto.ContractEndAt,
        };
    }

    public static User ToUserEntity(this EmployeeAddDto dto)
    {
        return new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Address = dto.Address,
            City = dto.City,
            ZipCode = dto.ZipCode,
            Country = dto.Country,
            BirthDate = dto.BirthDate,
            AllowMarketing = false, //dto.AllowMarketing,
        };
    }
}
