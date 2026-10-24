using BikeShopManager.Domain.Entities;
using BikeShopManager.WebApi.Dtos.Auth;

namespace BikeShopManager.WebApi.Mappers;

public static class AuthDtoMapper
{
    public static TokenResponse ToTokenResponseEntity(this TokenResponseDto dto)
    {
        return new TokenResponse()
        {
            AccessToken = dto.AccessToken,
            RefreshToken = dto.RefreshToken,
            ExpireAt = dto.ExpireAt,
            Type = dto.Type,
        };
    }

    public static LoginRequest ToLoginRequestEntity(this LoginRequestDto dto)
    {
        return new LoginRequest() { Login = dto.Login, Password = dto.Password };
    }

    public static TokenResponseDto ToTokenResponseDto(this TokenResponse entity)
    {
        return new TokenResponseDto()
        {
            AccessToken = entity.AccessToken,
            RefreshToken = entity.RefreshToken,
            ExpireAt = entity.ExpireAt,
            Type = entity.Type,
        };
    }
}
