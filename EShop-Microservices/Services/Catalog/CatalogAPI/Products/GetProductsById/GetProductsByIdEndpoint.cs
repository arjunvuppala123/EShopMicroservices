using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.GetProductsById
{
    public record GetProductByIdResponse(Product Product);
    public class GetProductsByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app) {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProductsById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Id")
            .WithDescription("Get Products By Id");
        }
    }
}
