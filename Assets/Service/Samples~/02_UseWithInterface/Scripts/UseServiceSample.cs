using UnityEngine;

namespace GigaCreation.Tools.Service.Sample02
{
    public class UseServiceSample : MonoBehaviour
    {
        [SerializeField] private bool _tryGetServiceOnStart;
        [SerializeField] private bool _logIfServiceIsUnregistered;

        private void Start()
        {
            if (_tryGetServiceOnStart)
            {
                // Get a service via ISampleService.
                if (ServiceLocator.TryGet(out ISampleService sampleService))
                {
                    sampleService.Bark();

                    // You cannot call this method.
                    // sampleService.Scratch();
                }
                else if (_logIfServiceIsUnregistered)
                {
                    Debug.Log("Fail to get SampleService.", gameObject);
                }
            }
        }
    }
}
