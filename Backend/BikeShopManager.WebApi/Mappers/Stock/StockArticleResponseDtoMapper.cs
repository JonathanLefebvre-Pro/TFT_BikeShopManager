using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Dtos.Stock;

namespace BikeShopManager.WebApi.Mappers.Stock;

public class StockArticleResponseDtoMapper : StockDtoMapperBase<Article, StockArticleResponseDto>
{
    public override StockArticleResponseDto ToDto(Article entity)
    {
        StockArticleResponseDto dto = new StockArticleResponseDto()
        {
            Id = entity.Id,
            Name = entity.Name,
            BrandId = entity.BrandId,
            Description = entity.Description,
            CategoryId = entity.CategoryId,
            PurchasePriceExclVat = entity.PurchasePriceExclVat,
            SalePriceInclVat = entity.SalePriceInclVat,
            VatRateId = entity.VatRateId,
            AvailableQuantity = entity.AvailableQuantity,
            MinQuantity = entity.MinQuantity,
            MaxQuantity = entity.MaxQuantity,
            EAN = entity.EAN,
            IsActive = entity.IsActive,
        };

        return dto;
    }
}
