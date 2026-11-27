using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Dtos.Stock;

namespace BikeShopManager.WebApi.Mappers.Stock;

public class StockArticleResponseDtoMapper : StockEntityMapperBase<Article, StockArticleResponseDto>
{
    public override Article ToEntity(StockArticleResponseDto dto)
    {
        Article entity = new Article()
        {
            Id = dto.Id,
            Name = dto.Name,
            BrandId = dto.BrandId,
            Description = dto.Description,
            CategoryId = dto.CategoryId,
            PurchasePriceExclVat = dto.PurchasePriceExclVat,
            SalePriceInclVat = dto.SalePriceInclVat,
            VatRateId = dto.VatRateId,
            AvailableQuantity = dto.AvailableQuantity,
            MinQuantity = dto.MinQuantity,
            MaxQuantity = dto.MaxQuantity,
            EAN = dto.EAN,
        };

        return entity;
    }
}
