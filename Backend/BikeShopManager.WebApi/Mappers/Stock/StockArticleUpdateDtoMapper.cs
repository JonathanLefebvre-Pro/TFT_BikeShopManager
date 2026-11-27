using BikeShopManager.Domain.Entities.Stock;
using BikeShopManager.WebApi.Dtos.Stock;

namespace BikeShopManager.WebApi.Mappers.Stock;

public class StockArticleUpdateDtoMapper : StockEntityMapperBase<Article, StockArticleUpdateDto>
{
    public override Article ToEntity(StockArticleUpdateDto dto)
    {
        Article entity = new Article()
        {
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
