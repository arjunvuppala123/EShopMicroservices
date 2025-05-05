using CatalogAPI.Products.CreateProduct;

namespace CatalogAPI.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductHandler(IDocumentSession session, ILogger<UpdateProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation($"UpdateProductHandler.Handle called with {command}");
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null) {
                throw new ProductNotFoundException();
            }

            product.Name = command.Name;
            product.Category = command.Category;
            product.Description = command.Description;
            product.ImageFile = command.ImageFile;
            product.Price = command.Price;

            //save to database
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);

            //return CreateProductResult result

            return new UpdateProductResult(true);
        }
    }
}
