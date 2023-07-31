

namespace DotnetCoding.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }

        void Save();
    }
}
