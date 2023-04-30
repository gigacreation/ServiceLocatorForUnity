using System.Collections;
using UnityEngine;

namespace GigaCreation.Tools.Service.Sample03
{
    public class RegisterServiceSample : MonoBehaviour
    {
        [SerializeField] private bool _registerOnAwake;
        [SerializeField] private bool _unregisterAfterOneSecond;

        private IDerivedSampleService _derivedSampleService;

        private void Awake()
        {
            // Remark that left hand is the derived type.
            _derivedSampleService = new SampleService();

            if (_registerOnAwake)
            {
                ServiceLocator.Register(_derivedSampleService);
            }
        }

        private IEnumerator Start()
        {
            if (_unregisterAfterOneSecond)
            {
                yield return new WaitForSeconds(1f);

                ServiceLocator.Unregister(_derivedSampleService);
            }
        }

        private void OnDestroy()
        {
            if (ServiceLocator.IsRegistered(_derivedSampleService))
            {
                ServiceLocator.Unregister(_derivedSampleService);
            }
        }
    }
}
