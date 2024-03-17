using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GigaCreation.Tools.Service.Sample05
{
    public class SampleService : ISampleService
    {
        public void Bark()
        {
            Debug.Log("Meow!");
        }

        public async UniTask DisposeAsync()
        {
            // First, print a message immediately when this method is called.
            Debug.Log("Wait for 1 second...");

            // We wait for 1 second.
            await UniTask.Delay(TimeSpan.FromSeconds(1d));

            // ISampleService inherits IUniTaskAsyncDisposable, so this method is called on unregister asynchronously.
            Debug.Log("SampleService disposed asynchronously with UniTask.");
        }

        public void Scratch()
        {
            // This method is public, but you cannot call it via ISampleService.
            Debug.Log("Ouch!");
        }
    }
}
