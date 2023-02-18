using System;

namespace GigaCreation.Tools.Service.Sample02
{
    public interface ISampleService : IService, IDisposable
    {
        void Bark();
    }
}
