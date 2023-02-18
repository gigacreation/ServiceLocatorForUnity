using System.Collections;
using UnityEngine;

namespace GigaCreation.Tools.Service.Sample02
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

        private IEnumerator Start()
        {
            if (_unregisterAfterOneSecond)
            {
                yield return new WaitForSeconds(1f);

                ServiceLocator.Unregister(_sampleService);
            }
        }

        private void OnDestroy()
        {
            if (ServiceLocator.IsRegistered(_sampleService))
            {
                ServiceLocator.Unregister(_sampleService);
            }
        }
    }
}
