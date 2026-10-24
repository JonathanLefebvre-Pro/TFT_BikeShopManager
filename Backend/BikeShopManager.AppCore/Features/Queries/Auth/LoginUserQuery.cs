using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries;

public record LoginUserQuery(LoginRequest Request) : IQueryDefinition<TokenResponse>;
