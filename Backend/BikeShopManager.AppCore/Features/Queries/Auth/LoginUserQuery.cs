using BikeShopManager.AppCore.Interfaces.Queries;
using BikeShopManager.Domain.Entities;

namespace BikeShopManager.AppCore.Features.Queries.Auth;

public record LoginUserQuery(LoginRequest Request) : IQueryDefinition<TokenResponse>;
