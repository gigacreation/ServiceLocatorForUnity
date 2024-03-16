using System;
using System.Threading;
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
            // Remark that left hand is ISampleService.
            _sampleService = new SampleService();

            if (_registerOnAwake)
            {
                ServiceLocator.Register(_sampleService);
            }
        }

        private void Start()
        {
            if (_unregisterAfterOneSecond)
            {
                Task _ = WaitAndUnregisterServiceAsync();
            }
        }

        private void OnDestroy()
        {
            if (ServiceLocator.IsRegistered(_sampleService))
            {
                ServiceLocator.Unregister(_sampleService);
            }
        }

        private async Task WaitAndUnregisterServiceAsync(CancellationToken ct = default)
        {
            await Task.Delay(TimeSpan.FromSeconds(1d), ct);
            await ServiceLocator.UnregisterAsync(_sampleService);

            Debug.Log("Completed to unregister the service.");
        }
    }
}
