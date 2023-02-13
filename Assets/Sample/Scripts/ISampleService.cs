using System;

namespace GigaceeTools.Service.Sample
{
    public interface ISampleService : IService, IDisposable
    {
        void Bark();
    }
}
