namespace CatalogAPI.Products.GetProductsById
{
    public record GetProductsByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) :
        IQueryHandler<GetProductsByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
        {

            logger.LogInformation($"GetProductsQueryHandler.Handle called with {request}");
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException();

            return new GetProductByIdResult(product);
        }
    }
}
