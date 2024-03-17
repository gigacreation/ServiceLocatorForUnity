using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GigaCreation.Tools.Service.Sample05
{
    public class RegisterServiceSample : MonoBehaviour
    {
        [SerializeField] private bool _registerOnAwake;
        [SerializeField] private bool _unregisterAfterOneSecond;

        private ISampleService _sampleService;

        private void Awake()
        {
            _sampleService = new SampleService();

            if (_registerOnAwake)
            {
                ServiceLocator.Register(_sampleService);
            }

            if (_unregisterAfterOneSecond)
            {
                WaitAndUnregisterServiceAsync(this.GetCancellationTokenOnDestroy()).Forget();
            }
        }

        private void OnDestroy()
        {
            UnregisterServiceAsync().Forget();
        }

        private async UniTask WaitAndUnregisterServiceAsync(CancellationToken ct = default)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1d), cancellationToken: ct);
            await UnregisterServiceAsync();
        }

        private async UniTask UnregisterServiceAsync()
        {
            if (!ServiceLocator.IsRegistered(_sampleService))
            {
                return;
            }

            await ServiceLocator.UnregisterAsync(_sampleService);

            Debug.Log("Completed to unregister the service.");
        }
    }
}
