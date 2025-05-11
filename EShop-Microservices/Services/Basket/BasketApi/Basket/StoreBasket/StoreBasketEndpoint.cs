﻿namespace BasketApi.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) => {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResponse>();
                return Results.Created($"/basket/{response.UserName}", response);
            })
            .WithName("CreateBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .WithSummary("Create Basket")
            .WithDescription("Create Basket");
        }
    }
}
