using Cysharp.Threading.Tasks;

namespace GigaCreation.Tools.Service.Sample05
{
    public interface ISampleService : IService, IUniTaskAsyncDisposable
    {
        void Bark();
    }
}
