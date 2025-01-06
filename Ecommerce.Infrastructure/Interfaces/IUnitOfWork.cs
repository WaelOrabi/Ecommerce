namespace Ecommerce.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        IAddressRepository AddressRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IRoleRepository RoleRepository { get; }
        ICartRepository CartRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        ICacheRepositroy CacheRepositroy { get; }
        Task CompleteAsync();
    }
}
