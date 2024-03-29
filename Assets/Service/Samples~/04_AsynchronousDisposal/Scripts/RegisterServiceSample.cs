using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GigaCreation.Tools.Service.Sample04
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
                ValueTask _ = WaitAndUnregisterServiceAsync();
            }
        }

        private void OnDestroy()
        {
            ValueTask _ = UnregisterServiceAsync();
        }

        private async ValueTask WaitAndUnregisterServiceAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1d));
            await UnregisterServiceAsync();
        }

        private async ValueTask UnregisterServiceAsync()
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
