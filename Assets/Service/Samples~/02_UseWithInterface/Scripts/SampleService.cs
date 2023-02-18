using UnityEngine;

namespace GigaCreation.Tools.Service.Sample02
{
    public class SampleService : ISampleService
    {
        public void Bark()
        {
            Debug.Log("Meow!");
        }

        public void Dispose()
        {
            // ISampleService inherits IDisposable, so this method is called on unregister.
            Debug.Log("SampleService disposed.");
        }

        public void Scratch()
        {
            // This method is public, but you cannot call it via ISampleService.
            Debug.Log("Ouch!");
        }
    }
}
