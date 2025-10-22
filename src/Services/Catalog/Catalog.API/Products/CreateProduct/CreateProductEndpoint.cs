namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest(string Name, List<string> category, string Description, string ImageFile, decimal Price);

    public record CreateProductResponse(Guid ProductId);
    public class CreateProductEndpoint
    {
    }
}
