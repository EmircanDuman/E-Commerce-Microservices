
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name cannot be empty")
                .MaximumLength(100).WithMessage("Name length cannot exceed 100 characters");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Categor name cannot be empty");
            RuleFor(x => x.Description).NotEmpty()
                .WithMessage("Description cannot be empty").MaximumLength(1000)
                .WithMessage("Description length cannot exceed 1000 characters");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Imagefile cannot be empty")
                .MaximumLength(400).WithMessage("ImageFile length cannot exceed 400 characters");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price needs to be higher than 0");
        }
    }

    internal class CreateProductCommandHandler
        (IDocumentSession session, ILogger<CreateProductCommandHandler> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateProductCommandHandler.Handle called with {@Command}", command);
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price

            };

            // save to database

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            // return createproductresult with new product id
            return new CreateProductResult(product.Id);
        }
    }
}
