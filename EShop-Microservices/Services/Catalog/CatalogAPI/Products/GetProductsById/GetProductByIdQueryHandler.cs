namespace CatalogAPI.Products.GetProductsById
{
    public record GetProductsByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session) :
        IQueryHandler<GetProductsByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductsByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException(request.Id);

            return new GetProductByIdResult(product);
        }
    }
}
