using System.Collections;
using UnityEngine;

namespace GigaceeTools.Service.Sample
{
    public class RegisterServiceSample : MonoBehaviour
    {
        [SerializeField] private bool _register;
        [SerializeField] private bool _unregister;

        private ISampleService _sampleService;

        private void Awake()
        {
            _sampleService = new SampleService();

            if (_register)
            {
                ServiceLocator.Register(_sampleService);
            }
        }

        private IEnumerator Start()
        {
            if (_unregister)
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
