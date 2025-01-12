namespace Ecommerce.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        ICategoryRepository CategoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        IReviewRepository ReviewRepository { get; }

        ICartRepository CartRepository { get; }
        ICartItemRepository CartItemRepository { get; }
        ICacheRepositroy CacheRepositroy { get; }
        IUserRefreshTokenRepository UserRefreshTokenRepository { get; }
        Task CompleteAsync();
    }
}
