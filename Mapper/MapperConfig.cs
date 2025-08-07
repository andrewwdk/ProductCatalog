using Mapster;
using ProductCatalog.Models.Dto;
using ProductCatalog.Models.Responses;

namespace ProductCatalog.Mapper
{
    public static class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<List<ProductDto>, ProductsResponse>
                .NewConfig()
                .Map(dest => dest.Products, src => src);
        }
    }
}
