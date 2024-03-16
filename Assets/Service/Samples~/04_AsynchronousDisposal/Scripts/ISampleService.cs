using System;

namespace GigaCreation.Tools.Service.Sample04
{
    public interface ISampleService : IService, IAsyncDisposable
    {
        void Bark();
    }
}
