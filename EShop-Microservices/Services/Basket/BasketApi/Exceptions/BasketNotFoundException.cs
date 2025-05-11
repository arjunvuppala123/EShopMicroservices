using BuildingBlocks.Exceptions;

namespace BasketApi.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string UserName) : base("UserName" , UserName) { }
    }
}


namespace CatalogAPI.Exceptions
{
    public class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid Id) : base("Product", Id) { }
    }
}
