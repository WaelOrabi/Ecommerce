namespace Application.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
       IAccountRepository AccountRepository { get; }
       ICategoryRepository CategoryRepository { get; }
       IOrderRepository OrderRepository { get; }
       IProductRepository ProductRepository { get; }
       IReviewRepository ReviewRepository { get; }
       IRoleRepository RoleRepository { get; }
        Task CompleteAsync();
    }
}
