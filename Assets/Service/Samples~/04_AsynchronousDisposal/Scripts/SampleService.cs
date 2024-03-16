using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GigaCreation.Tools.Service.Sample04
{
    public class SampleService : ISampleService
    {
        public void Bark()
        {
            Debug.Log("Meow!");
        }

        public async ValueTask DisposeAsync()
        {
            // First, print a message immediately when this method is called.
            Debug.Log("Wait for 1 second...");

            // We wait for 1 second.
            await Task.Delay(TimeSpan.FromSeconds(1d));

            // ISampleService inherits IAsyncDisposable, so this method is called on unregister asynchronously.
            Debug.Log("SampleService disposed asynchronously.");
        }

        public void Scratch()
        {
            // This method is public, but you cannot call it via ISampleService.
            Debug.Log("Ouch!");
        }
    }
}
